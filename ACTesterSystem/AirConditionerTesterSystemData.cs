namespace AirConditionerTesterSystem
{
    using System.Collections.Generic;
    using System.Linq;
    using Exceptions;
    using Interfaces;
    using Models;
    using Models.AirConditioners;
    using Utilities;

    public class AirConditionerTesterSystemData : IAirConditionerTesterSystemData
    {
        public AirConditionerTesterSystemData()
        {
            this.AirConditioners = new Dictionary<string, AirConditioner>();
            this.Reports = new Dictionary<string, IList<Report>>();
        }

        public Dictionary<string, AirConditioner> AirConditioners { get; }

        public Dictionary<string, IList<Report>> Reports { get; }

        public void AddAirConditioner(CarAirConditioner airConditioner)
        {
            string key = airConditioner.Manufacturer + airConditioner.Model;
            this.AirConditioners.Add(key, airConditioner);
        }

        public void RemoveAirConditioner(CarAirConditioner airConditioner)
        {
            string key = airConditioner.Manufacturer + airConditioner.Model;
            this.AirConditioners.Remove(key);
        }

        public AirConditioner GetAirConditioner(string manufacturer, string model)
        {
            if (this.AirConditioners.ContainsKey(manufacturer + model))
            {
                return this.AirConditioners[manufacturer + model];
            }

            return null;
        }

        public int GetAirConditionersCount()
        {
            return this.AirConditioners.Count;
        }

        public void AddReport(Report report)
        {
            if (this.Reports.ContainsKey(report.Manufacturer))
            {
                this.Reports[report.Manufacturer].Add(report);
            }
            else
            {
                this.Reports.Add(report.Manufacturer, new List<Report>());
                this.Reports[report.Manufacturer].Add(report);
            }
        }

        public void RemoveReport(Report report)
        {
            this.Reports[report.Manufacturer].Remove(report);
        }

        public Report GetReport(string manufacturer, string model)
        {
            if (this.Reports.ContainsKey(manufacturer))
            {
                if (this.Reports[manufacturer].Count > 0)
                {
                    return this.Reports[manufacturer].FirstOrDefault(x => x.Manufacturer == manufacturer && x.Model == model);
                }
            }
            
            return null;
        }

        public int GetReportsCount()
        {
            int count = 0;

            foreach (KeyValuePair<string, IList<Report>> pair in this.Reports)
            {
                count += pair.Value.Count;
            }

            return count;
        }

        public IList<Report> GetReportsByManufacturer(string manufacturer)
        {
            if (this.Reports.ContainsKey(manufacturer))
            {
                return this.Reports[manufacturer];
            }

            else
            {
               return null;
            }
        }
    }
}