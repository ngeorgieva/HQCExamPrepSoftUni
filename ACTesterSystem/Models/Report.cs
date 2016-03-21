namespace AirConditionerTesterSystem.Models
{
    using System;
    using Interfaces;

    /// <summary>
    /// A class for objects of type Report.
    /// </summary>
    public class Report : IReport
    {
        public Report(string manufacturer, string model, int mark)
        {
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Mark = mark;
        }

        /// <summary>
        /// Gets and sets a specified manufacturer
        /// </summary>
        public string Manufacturer { get; }

        /// <summary>
        /// Gets and sets a specified Model
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets and sets the mark result of a test
        /// </summary>
        public int Mark { get; }

        public override string ToString()
        {
            var result = string.Empty;
            if (this.Mark == 0)
            {
                result = "Failed";
            }
            else if (this.Mark == 1)
            {
                result = "Passed";
            }

            var resultPrint = "Report"
                              + "\r\n"
                              + "===================="
                              + "\r\n" + "Manufacturer: "
                              + this.Manufacturer + "\r\n"
                              + "Model: "
                              + this.Model
                              + "\r\n"
                              + "Mark: "
                              + result
                              + "\r\n" + "====================";

            return resultPrint;
        }
    }
}