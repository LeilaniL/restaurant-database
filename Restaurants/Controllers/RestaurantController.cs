using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Models;

namespace Restaurants.Controllers
{
    public class RestaurantController : Controller
    {
        [HttpGet("/restaurant")]
        public ActionResult Index()
        {
            List<Restaurant> allRestaurants = Restaurant.GetAllRestaurants();
            return View(allRestaurants);
        }
        [HttpGet("Cuisine/{id}/restaurant/new")]
        public ActionResult New(int id)
        {
            Cuisine selectCuisine = Cuisine.Find(id);
            return View("../restaurant/new", selectCuisine);
        }
        [HttpPost("/addRestaurant")]
        public ActionResult Create(string RestaurantName, string Location, int Cuisine)
        {
            Restaurant newRestaurant = new Restaurant(RestaurantName, Location, Cuisine);
            newRestaurant.SaveRestaurant();
            List<Restaurant> allRestaurants = Restaurant.GetAllRestaurants();
            return View("Index", allRestaurants);

        }
        [HttpGet("/Cuisine/{cuisine_id}/Restaurant/{restaurant_id}")]
        public ActionResult ShowRestaurant(int cuisine_id, int restaurant_id)
        {

            Console.WriteLine("Controller worked");
            Cuisine selectedCuisine = Cuisine.Find(cuisine_id);
            Dictionary<string, object> model = new Dictionary<string, object>();
            Restaurant selectRestaurant = Restaurant.FindRestaurant(restaurant_id);
            model.Add("Cuisine", selectedCuisine);
            model.Add("Restaurant", selectRestaurant);
            return View("Show", model);
        }


        [HttpGet("/Cuisine/{cuisine_id}/Restaurant/{restaurant_id}/edit")]
        public ActionResult Edit(int cuisine_id, int restaurant_id)
        {
            Console.WriteLine("Controller for edit worked");
            Dictionary<string, object> model = new Dictionary<string, object>();
            Restaurant selectRestaurant = Restaurant.FindRestaurant(restaurant_id);
            model.Add("Restaurant", selectRestaurant);
            Cuisine selectedCuisine = Cuisine.Find(cuisine_id);
            model.Add("Cuisine", selectedCuisine);
            return View("Edit", model);
        }

        [HttpPost("/Cuisine/{cuisine_id}/Restaurant/{restaurant_id}")]
        public ActionResult Update(int cuisine_id, int restaurant_id, string restaurantName, string location)
        {

            Console.WriteLine("Controller worked");
            Restaurant selectedRestaurant = Restaurant.FindRestaurant(restaurant_id);
            Cuisine selectedCuisine = Cuisine.Find(cuisine_id);
            selectedRestaurant.Edit(restaurantName, location);
            Dictionary<string, object> model = new Dictionary<string, object>();
            Restaurant selectRestaurant = Restaurant.FindRestaurant(restaurant_id);
            model.Add("Cuisine", selectedCuisine);
            model.Add("Restaurant", selectedRestaurant);
            return View("Show", model);
        }
    }
}