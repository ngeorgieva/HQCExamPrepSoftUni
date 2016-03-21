namespace AirConditionerTesterSystem.Models.AirConditioners
{
    using System;
    using Utilities;
    using Type = Enums.Type;

    public class CarAirConditioner : AirConditioner
    {
        private int volumeCovered;

        public CarAirConditioner(string manufacturer, string model, int volumeCoverage)
            : base(Type.Car, manufacturer, model)
        {
            this.VolumeCovered = volumeCoverage;
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

        public override int Test()
        {
            var sqrtVolume = Math.Sqrt(this.VolumeCovered);

            if (sqrtVolume >= Constants.MinCarVolume)
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
                   + "\r\n" + "====================";
        }
    }
}