namespace AirConditionerTesterSystem.Models.AirConditioners
{
    using System;
    using Utilities;
    using Type = Enums.Type;

    public class StationaryAirConditioner : AirConditioner
    {
        private char actualEnergyEfficiencyRating;
        private int powerUsage;
        private char requiredEnergyEfficiencyRating;

        public StationaryAirConditioner(
            string manufacturer, 
            string model, 
            char requiredEnergyEfficiencyRating,
            int powerUsage)
            : base(Type.Stationary, manufacturer, model)
        {
            this.PowerUsage = powerUsage;
            this.RequiredEnergyEfficiencyRating = requiredEnergyEfficiencyRating;
            if (this.PowerUsage > 2000)
            {
                this.ActualEnergyEfficiencyRating = 'E';
            }
            else if (this.PowerUsage <= 2000 && this.PowerUsage >= 1501)
            {
                this.ActualEnergyEfficiencyRating = 'D';
            }
            else if (this.PowerUsage <= 1500 && this.PowerUsage >= 1251)
            {
                this.ActualEnergyEfficiencyRating = 'C';
            }
            else if (this.PowerUsage <= 1250 && this.PowerUsage >= 1000)
            {
                this.ActualEnergyEfficiencyRating = 'B';
            }
            else if (this.PowerUsage < 1000)
            {
                this.ActualEnergyEfficiencyRating = 'A';
            }
        }

        public char RequiredEnergyEfficiencyRating
        {
            get
            {
                return this.requiredEnergyEfficiencyRating;
            }

            set
            {
                if (value != 'A' && value != 'B' && value != 'C' && value != 'D' && value != 'E')
                {
                    throw new ArgumentException(Constants.IncorrectRating);
                }

                this.requiredEnergyEfficiencyRating = value;
            }
        }

        public int PowerUsage
        {
            get
            {
                return this.powerUsage;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(Constants.NonPositive, "Power Usage"));
                }

                this.powerUsage = value;
            }
        }

        public char ActualEnergyEfficiencyRating
        {
            get
            {
                return this.actualEnergyEfficiencyRating;
            }

            set
            {
                this.actualEnergyEfficiencyRating = value;
            }
        }

        public override int Test()
        {
            if (this.ActualEnergyEfficiencyRating <= this.RequiredEnergyEfficiencyRating)
            {
                return 1;
            }

            return 0;
        }

        public override string ToString()
        {
            return base.ToString()
                   + "Required energy efficiency rating: "
                   + this.RequiredEnergyEfficiencyRating
                   + "\r\n"
                   + "Power Usage(KW / h): "
                   + this.PowerUsage + "\r\n"
                   + "====================";
        }
    }
}