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
            Dictionary<string, object> model = new Dictionary<string, object>();
            Cuisine selectedCuisine = Cuisine.Find(cuisine_id);
            Restaurant selectRestaurant = Restaurant.FindRestaurant(restaurant_id);
            model.Add("Cuisine", selectedCuisine);
            model.Add("Restaurant", selectRestaurant);
            return View("Show", model);
        }

        // [HttpGet("/Cuisine/{cuisine_id}/Restaurant/{restaurant_id}")]
        // public ActionResult ShowRestaurant(int cuisine_id, int restaurant_id)
        // {
        //     Console.WriteLine("Controller worked");
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine selectedCuisine = Cuisine.Find(cuisine_id);
        //     Restaurant selectRestaurant = Restaurant.FindRestaurant(restaurant_id);
        //     model.Add("Cuisine", selectedCuisine);
        //     model.Add("Restaurant", selectRestaurant);
        //     return View("Show", model);
        // }
    }
}