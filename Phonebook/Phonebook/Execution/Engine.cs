namespace Phonebook.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;

    public class Engine : IEngine
    {
        private const string Code = "+359";

        private readonly IPhonebookRepository data;

        public Engine(IPhonebookRepository data)
        {
            this.data = data;
        }

        public void Run()
        {
            string consoleInput = Console.ReadLine();

            string result = string.Empty;
            while (consoleInput != "End" && consoleInput != null)
            {
                var indexOfOpeningBracket = consoleInput.IndexOf('(');
                if (indexOfOpeningBracket == -1)
                {
                    Console.WriteLine("error!");
                    Environment.Exit(0);
                }

                var command = consoleInput.Substring(0, indexOfOpeningBracket);

                var commandArgsString = consoleInput.Substring(indexOfOpeningBracket + 1, consoleInput.Length - indexOfOpeningBracket - 2);
                var commandArgs = commandArgsString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (var j = 0; j < commandArgs.Length; j++)
                {
                    commandArgs[j] = commandArgs[j].Trim();
                }

                try
                {
                    switch (command)
                    {
                        case "AddPhone":
                            result = this.ExecuteAddPhone(commandArgs);
                            break;
                        case "ChangePhone":
                            result = this.ExecuteChangePhoneCommand(commandArgs);
                            break;
                        case "List":
                            result = this.ExecuteListCommand(commandArgs);
                            break;
                        default:
                            throw new InvalidOperationException("This command is not supported.");
                    }
                }
                catch (ArgumentException ex)
                {
                    result = ex.Message;
                }
                finally
                {
                    Console.WriteLine(result);
                    consoleInput = Console.ReadLine();
                }  
            }
        }

        private string ExecuteAddPhone(string[] args)
        {
            var name = args[0];
            var phoneNumbers = args.Skip(1).ToList();
            for (var i = 0; i < phoneNumbers.Count; i++)
            {
                phoneNumbers[i] = this.GetPhoneNumInCorrectFormat(phoneNumbers[i]);
            }

            bool created = this.data.AddPhone(name, phoneNumbers);

            string commandResult = created ? "Phone entry created" : "Phone entry merged";

            return commandResult;
        }

        private string ExecuteChangePhoneCommand(string[] args)
        {
            var phoneNumbers = new List<string>();
            for (var i = 0; i < args.Length; i++)
            {
                phoneNumbers.Add(this.GetPhoneNumInCorrectFormat(args[i]));
            }

            int changedPhoneNumbers = this.data.ChangePhone(phoneNumbers[0], phoneNumbers[1]);
            string result = $"{changedPhoneNumbers} numbers changed";

            return result;
        }

        private string ExecuteListCommand(string[] args)
        {
            int startIndex = int.Parse(args[0]);
            int count = int.Parse(args[1]);
            var result = new StringBuilder();

            IEnumerable<IPhonebookEntry> listedEntries = this.data.ListEntries(startIndex, count);
            //if (listedEntries == null)
            //{
            //    result.Append("Invalid range");
            //}
            //else
            //{
                foreach (IPhonebookEntry entry in listedEntries)
                {
                    result.AppendLine(entry.ToString());
                }
            //}

            return result.ToString().Trim();
        }

        private string GetPhoneNumInCorrectFormat(string phoneNumber)
        {
            var sb = new StringBuilder();

            sb.Clear();
            foreach (var symbol in phoneNumber)
            {
                if (char.IsDigit(symbol) || (symbol == '+'))
                {
                    sb.Append(symbol);
                }
            }

            if (sb.Length >= 2 && sb[0] == '0' && sb[1] == '0')
            {
                sb.Remove(0, 1);
                sb[0] = '+';
            }

            while (sb.Length > 0 && sb[0] == '0')
            {
                sb.Remove(0, 1);
            }

            if (sb.Length > 0 && sb[0] != '+')
            {
                sb.Insert(0, Code);
            }

            return sb.ToString();
        }
    }
}