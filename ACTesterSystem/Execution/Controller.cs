namespace AirConditionerTesterSystem.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.AirConditioners;
    using Utilities;

    public class Controller : IController
    {
        private IAirConditionerTesterSystemData data;

        public Controller(IAirConditionerTesterSystemData data)
        {
            this.data = data;
        }

        public string RegisterStationaryAirConditioner(
            string manufacturer, 
            string model, 
            char energyEfficiencyRating,
            int powerUsage)
        {
            var stationaryAC = new StationaryAirConditioner(
                manufacturer, 
                model, 
                energyEfficiencyRating, 
                powerUsage);
            this.CheckForDuplicateEntry(stationaryAC);
            this.data.AirConditioners.Add(manufacturer + model, stationaryAC);
            return string.Format(
                Constants.RegisterMessage, 
                stationaryAC.Model, 
                stationaryAC.Manufacturer);
        }

        public string RegisterCarAirConditioner(
            string manufacturer, 
            string model, 
            int volumeCoverage)
        {
            var carAirConditioner = new CarAirConditioner(manufacturer, model, volumeCoverage);
            this.CheckForDuplicateEntry(carAirConditioner);
            this.data.AirConditioners.Add(manufacturer + model, carAirConditioner);
            return string.Format(
                Constants.RegisterMessage, 
                carAirConditioner.Model, 
                carAirConditioner.Manufacturer);
        }

        /// <summary>
        /// A command for registering a new Air conditioner
        /// </summary>
        /// <param name="manufacturer">The manufacturer of the AC to be registered.</param>
        /// <param name="model">The model of the AC to be registered.</param>
        /// <param name="volumeCoverage">The volume coverage of the AC to be registered.</param>
        /// <param name="electricityUsed">The electricity usage of the AC to be registered.</param>
        /// <returns></returns>
        public string RegisterPlaneAirConditioner(
            string manufacturer, 
            string model, 
            int volumeCoverage,
            int electricityUsed)
        {
            var planeAirConditioner = new PlaneAirConditioner(manufacturer, model, volumeCoverage, electricityUsed);
            this.CheckForDuplicateEntry(planeAirConditioner);
            this.data.AirConditioners.Add(manufacturer + model, planeAirConditioner);
            return string.Format(
                Constants.RegisterMessage, 
                planeAirConditioner.Model, 
                planeAirConditioner.Manufacturer);
        }

        public string TestAirConditioner(string manufacturer, string model)
        {
            if (this.data.AirConditioners.ContainsKey(manufacturer + model))
            {
                var airConditioner = this.data.GetAirConditioner(manufacturer, model);
                var mark = airConditioner.Test();
                var report = new Report(airConditioner.Manufacturer, airConditioner.Model, mark);

                if (!this.data.Reports.ContainsKey(manufacturer))
                {
                    this.data.Reports.Add(manufacturer, new List<Report>());
                    this.data.Reports[manufacturer].Add(report);
                }
                else
                {
                    if (this.data.Reports[manufacturer].Any(r => r.Model == model))
                    {
                        return Constants.Duplicate;
                    }
                    else
                    {
                        this.data.Reports[manufacturer].Add(report);
                    }
                }
  
                return string.Format(Constants.TestMessage, model, manufacturer);
            }

            return Constants.NonExistant;
        }

        /// <summary>
        /// Gets an AC by specified manufacturer and model from the Database
        /// </summary>
        /// <param name="manufacturer">The manufacturer of the AC to be found</param>
        /// <param name="model">The model of the AC to be found</param>
        /// <returns>A string with the AC details.</returns>
        public string FindAirConditioner(string manufacturer, string model)
        {
            var airConditioner = this.data.GetAirConditioner(manufacturer, model);
            if (airConditioner == null)
            {
                return Constants.NonExistant;
            }

            return airConditioner.ToString();
        }

        public string FindReport(string manufacturer, string model)
        {
            var report = this.data.GetReport(manufacturer, model);
            if (report == null)
            {
                return Constants.NonExistant;
            }

            return report.ToString();
        }

        public string FindAllReportsByManufacturer(string manufacturer)
        {
            var reports = this.data.GetReportsByManufacturer(manufacturer);

            if (reports == null)
            {
                return Constants.NoReports;
            }

            reports = reports.OrderBy(x => x.Model).ToList();
            var reportsPrint = new StringBuilder();
            reportsPrint.AppendLine(string.Format("Reports from {0}:", manufacturer));
            reportsPrint.Append(string.Join(Environment.NewLine, reports));

            return reportsPrint.ToString();
        }

        /// <summary>
        /// Calculates the percentage of completed jobs.
        /// </summary>
        /// <returns></returns>
        public string Status()
        {
            var reports = this.data.GetReportsCount();
            double airConditioners = this.data.GetAirConditionersCount();
            var percent = reports / airConditioners;
            if (double.IsNaN(percent))
            {
                percent = 0d;
            }

            percent = percent * 100;
            return string.Format(Constants.Status, percent);
        }

        private void CheckForDuplicateEntry(AirConditioner airConditioner)
        {
            if (this.data.AirConditioners.ContainsKey(airConditioner.Manufacturer + airConditioner.Model))
            {
                throw new DuplicateEntryException(Constants.Duplicate);
            }
        }
    }
}
