using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NationalParks.Droid
{
    public class NationalParksData
    {
        public string DataDir { get; set; }

        protected string GetFilename()
        {
            return Path.Combine(DataDir, "NationalParks.json");
        }

        static NationalParksData _instance;
        public static NationalParksData Instance
        {
            get { return _instance ?? (_instance = new NationalParksData()); }
        }

        List<NationalPark> _parks;
        public List<NationalPark> Parks
        {
            get { return _parks; }
            protected set { _parks = value; }
        }

        private NationalParksData()
        {
        }

        public void Load()
        {
            if (File.Exists(GetFilename()))
            {
                string serializedParks = File.ReadAllText(GetFilename());
                _parks = JsonConvert.DeserializeObject<List<NationalPark>>(serializedParks);
            }
            else
            {
                _parks = new List<NationalPark>();
            }
        }

        public void Save(NationalPark park)
        {
            if (_parks != null)
            {
                if (!_parks.Contains(park))
                {
                    _parks.Add(park);
                    var data = JsonConvert.SerializeObject(_parks);
                    File.WriteAllText(GetFilename(), data);
                }
            }
        }

        public void Delete(NationalPark park)
        {
            if (_parks != null)
            {
                _parks.Remove(park);
                var data = JsonConvert.SerializeObject(_parks);
                File.WriteAllText(GetFilename(), data);
            }
        }

    }
}
