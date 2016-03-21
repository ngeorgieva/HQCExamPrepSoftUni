namespace AirConditionerTesterSystem.Models.AirConditioners
{
    using System;
    using Utilities;
    using Type = Enums.Type;

    public class PlaneAirConditioner : AirConditioner
    {
        private int electricityUsed;
        private int volumeCovered;

        public PlaneAirConditioner(string manufacturer, string model, int volumeCovered, int electricityUsed)
            : base(Type.Plane, manufacturer, model)
        {
            this.VolumeCovered = volumeCovered;
            this.ElectricityUsed = electricityUsed;
        }

        public int VolumeCovered
        {
            get
            {
                return this.volumeCovered;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositive, "Volume Covered"));
                }

                this.volumeCovered = value;
            }
        }

        public int ElectricityUsed
        {
            get
            {
                return this.electricityUsed;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositive, "Electricity Used"));
                }

                this.electricityUsed = value;
            }
        }

        public override int Test()
        {
            var sqrtVolume = Math.Sqrt(this.volumeCovered);
            if (this.ElectricityUsed / sqrtVolume < Constants.MinPlaneElectricity)
            {
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            return base.ToString()
                   + "Volume Covered: "
                   + this.VolumeCovered
                   + "\r\n"
                   + "Electricity Used: "
                   + this.ElectricityUsed
                   + "\r\n"
                   + "====================";
        }
    }
}