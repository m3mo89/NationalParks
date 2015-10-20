using System;
namespace NationalParks.Droid
{
    public class NationalPark
    {
        public NationalPark()
        {
            Id = Guid.NewGuid().ToString();
            Name = "New Park";
        }
        public string Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        public string Country
        {
            get;
            set;
        }
        public double? Latitude
        {
            get;
            set;
        }
        public double? Longitude
        {
            get;
            set;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
