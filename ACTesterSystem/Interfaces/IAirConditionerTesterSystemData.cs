namespace AirConditionerTesterSystem.Interfaces
{
    using System.Collections.Generic;
    using Models;
    using Models.AirConditioners;

    public interface IAirConditionerTesterSystemData
    {
        Dictionary<string, AirConditioner> AirConditioners { get; }

        Dictionary<string, IList<Report>> Reports { get; }

        void AddAirConditioner(CarAirConditioner airConditioner);

        void RemoveAirConditioner(CarAirConditioner airConditioner);

        AirConditioner GetAirConditioner(string manufacturer, string model);

        int GetAirConditionersCount();

        void AddReport(Report report);

        void RemoveReport(Report report);

        Report GetReport(string manufacturer, string model);

        int GetReportsCount();

        IList<Report> GetReportsByManufacturer(string manufacturer);
    }
}
