using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Restaurants;

namespace Restaurants.Models
{
    public class Restaurant
    {
        private string _name;
        private int _cuisineId;
        private string _location;
        private int _id;

        public Restaurant(string name, string location, int cuisineId, int id = 0)
        {
            _name = name;
            _location = location;
            _cuisineId = cuisineId;
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public string GetLocation()
        {
            return _location;
        }
        public static List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> allRestaurants = new List<Restaurant> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string name = rdr.GetString(0);
                int cuisineId = rdr.GetInt32(1);
                string location = rdr.GetString(2);
                int id = rdr.GetInt32(3);
                Restaurant newRestaurant = new Restaurant(name, location, cuisineId, id);
                allRestaurants.Add(newRestaurant);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRestaurants;
        }
        public void SaveRestaurant()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO restaurants (name, location, cuisine_id) VALUES (@name, @location, @cuisine);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;

            MySqlParameter location = new MySqlParameter();
            location.ParameterName = "@location";
            location.Value = this._location;

            MySqlParameter cuisine = new MySqlParameter();
            cuisine.ParameterName = "@cuisine";
            cuisine.Value = this._cuisineId;
            cmd.Parameters.Add(name);
            cmd.Parameters.Add(location);
            cmd.Parameters.Add(cuisine);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Restaurant FindRestaurant(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int restaurantId = 1;
            string restaurantName = "";
            string location = "";
            int cuisineId = 1;
            while (rdr.Read())
            {
                restaurantName = rdr.GetString(0);
                cuisineId = rdr.GetInt32(1);
                location = rdr.GetString(2);
                restaurantId = id;
            }
            Restaurant newRestaurant = new Restaurant(restaurantName, location, cuisineId, id);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newRestaurant;
        }

    }
}
