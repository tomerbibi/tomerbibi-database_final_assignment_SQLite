using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLite_Test
{
    static class Functions
    {
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static readonly string ConnectionString;
        static SQLiteConnection con = new SQLiteConnection(ConnectionString);
        static Functions()
        {
            var reader = File.OpenText("SQLite_Test.config.json");
            string connection_string = reader.ReadToEnd();
        }
        static void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                    {
                        cmd.Connection.Open();
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = query;

                        cmd.ExecuteNonQuery();
                    }
                }
                catch(Exception)
                {
                    my_logger.Error("failed to open connection to the database");
                }
            }
        }

        static public List<Car> GetCars()
        {
            List<Car> cars = new List<Car>();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                con.Open();
                    using(SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM cars", con))
                {
                    using(SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Car c = new Car((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2), (int)reader.GetValue(3));
                            cars.Add(c);
                        }
                    }
                }
                con.Close();
            }
            return cars;
        }
        static public void AddCar(string manufacturer, string model, int year)
        {
            try
            {
                ExecuteNonQuery($"INSERT INTO cars (Manufacturer, Model, Year) VALUES ('{manufacturer}', '{model}', {year})");
                my_logger.Info("car was added to cars");
            }
            catch(Exception)
            {
                my_logger.Error("failed to add car");
            }
        }
        static public void UpdateCar(int id, string manufacturer, string model, int year)
        {
            ExecuteNonQuery($"UPDATE cars SET Manufacturer = '{manufacturer}', Model = '{model}', Year = {year} WHERE ID = {id}");
        }
        static public void RemoveCar(int id)
        {
            try
            {
                ExecuteNonQuery($"DELETE FROM cars WHERE ID = {id}");
                my_logger.Info("car was deleted from cars");
            }
            catch(Exception)
            {
                my_logger.Error("failed to delete a car");
            }
        }

        static public List<Test> GetTests()
        {
            List<Test> tests = new List<Test>();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM cars", con))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Test c = new Test((int)reader.GetValue(0), (int)reader.GetValue(1), (bool)reader.GetValue(2), (DateTime)reader.GetValue(3));
                            tests.Add(c);
                        }
                    }
                }
            }
            return tests;
        }
        static public void AddTest(int carId, bool isPassed, DateTime date)
        {
            try
            {
                ExecuteNonQuery($"INSERT INTO TEST (Car_ID, IsPassed, Date) VALUES({carId}, {isPassed}, {date})");
                my_logger.Info("test was added to tests");
            }
            catch(Exception)
            {
                my_logger.Error("failed to add test");
            }
        }
        static public void UpdateTest(int id, int carId, bool isPassed, DateTime date)
        {
            ExecuteNonQuery($"UPDATE TEST SET Car_ID = {carId}, IsPassed = {isPassed}, Date = {date} WHERE ID = {id}");
        }
        static public void RemoveTest(int id)
        {
            try
            {
                ExecuteNonQuery($"DELETE FROM TEST WHERE ID = {id}");
                my_logger.Info("test was deleted from tests");
            }
            catch(Exception)
            {
                my_logger.Error("failed to delete test");
            }
        }
        static List<Car> GetCarsByManufacturer(string manufacturer)
        {
            List<Car> cars = new List<Car>();
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM cars WHERE Manufacturer = '{manufacturer}'", con))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Car c = new Car((int)reader.GetValue(0), (string)reader.GetValue(1), (string)reader.GetValue(2), (int)reader.GetValue(3));
                            cars.Add(c);
                        }
                    }
                }
            }
            return cars;
        }
        static void ShowTestData()
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectionString))
            {
                con.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select TEST.ID ,TEST.Car_ID, TEST.Date, TEST.IsPassed, cars.Manufacturer, cars.Model, cars.Year from TEST join cars on TEST.Car_ID = cars.ID", con))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine((string)reader.GetValue(0) + (string)reader.GetValue(1) + (string)reader.GetValue(2) +
                                (string)reader.GetValue(3) + (string)reader.GetValue(4) + (string)reader.GetValue(5));
                        }
                    }
                }
            }
        }

    }
}
