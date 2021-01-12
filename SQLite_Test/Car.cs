using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Test
{
    class Car
    {
        public Car(int id, string manufacturer, string model, int year)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            this.year = year;
        }

        public Car()
        {
        }

        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int year { get; set; }

        public override bool Equals(object obj)
        {
            return this.Id == ((Car)obj).Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }
        public static bool operator ==(Car car1, Car car2)
        {
            return car1.Id == car2.Id;
        }
        public static bool operator !=(Car car1, Car car2)
        {
            return !(car1 == car2);
        }


        public override string ToString()
        {
            return $"{Newtonsoft.Json.JsonConvert.SerializeObject(this)}";
        }
    }
}
