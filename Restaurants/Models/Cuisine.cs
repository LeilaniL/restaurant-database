using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Restaurants;

namespace Restaurants.Models
{
    public class Cuisine
    {
        private string _name;
        private int _id;

        public Cuisine(string name, int id = 0)
        {
            _name = name;
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
        public static List<Cuisine> GetAll()
        {
            List<Cuisine> allcuisines = new List<Cuisine> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * from cuisine;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int cuisineId = rdr.GetInt32(1);
                string cuisineDescription = rdr.GetString(0);
                Cuisine newCuisine = new Cuisine(cuisineDescription, cuisineId);
                allcuisines.Add(newCuisine);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allcuisines;
        }

        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisine WHERE id = (@searchId);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int cuisineId = 1;
            string cuisineName = "";
            while (rdr.Read())
            {
                cuisineId = rdr.GetInt32(1);
                cuisineName = rdr.GetString(0);
            }
            Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCuisine;
        }
        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM cuisine;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public override bool Equals(System.Object otherCuisine)
        {
            if (!(otherCuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuisine newCuisine = (Cuisine)otherCuisine;
                bool idEquality = this.GetId() == newCuisine.GetId();
                bool nameEquality = this.GetName() == newCuisine.GetName();
                // bool categoryEquality = this.GetCategoryId() == newItem.GetCategoryId();
                return (idEquality && nameEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cuisine (name, id) VALUES (@CuisineDescription, @cuisine_id);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@CuisineDescription";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            MySqlParameter id = new MySqlParameter();
            id.ParameterName = "@cuisine_id";
            id.Value = this._id;
            cmd.Parameters.Add(id);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Restaurant> GetRestaurantsByCuisine(int cuisineId)
        {
            List<Restaurant> allRestaurants = new List<Restaurant> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants WHERE cuisine_id = @thisId;";
            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = cuisineId;
            cmd.Parameters.Add(thisId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                string name = rdr.GetString(0);
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
    }
}