namespace Phonebook
{
    using Data;
    using Execution;

    public class PhonebookProgram
    {
        public static void Main()
        {
            var factory = new PhonebookFactory();
            var data = new PhonebookRepository(factory);
            var engine = new Engine(data);
            engine.Run();
        }
    }
}