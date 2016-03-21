namespace AirConditionerTesterSystem.Models.AirConditioners
{
    using System;
    using Utilities;
    using Type = Enums.Type;

    public abstract class AirConditioner
    {
        private string manufacturer;
        private string model;

        protected AirConditioner(Type type, string manufacturer, string model)
        {
            this.Type = type;
            this.Manufacturer = manufacturer;
            this.Model = model;
        }

        public Type Type { get; }

        public string Manufacturer
        {
            get
            {
                return this.manufacturer;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) 
                    || value.Length < Constants.ManufacturerMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyNameLength, "Manufacturer", Constants.ManufacturerMinLength));
                }

                this.manufacturer = value;
            }
        }

        public string Model
        {
            get
            {
                return this.model;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value) 
                    || value.Length < Constants.ModelMinLength)
                {
                    throw new ArgumentException(string.Format(Constants.IncorrectPropertyNameLength, "Model", Constants.ModelMinLength));
                }

                this.model = value;
            }
        }

        public abstract int Test();

        public override string ToString()
        {
            var print = "Air Conditioner" + "\r\n" + "====================" + "\r\n" + "Manufacturer: " +
                        this.Manufacturer + "\r\n" + "Model: " + this.Model + "\r\n";

            return print;
        }
    }
}