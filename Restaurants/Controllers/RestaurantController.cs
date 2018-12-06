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
        // [HttpGet("Cuisine/{id}/restaurant/new")]
        // public ActionResult New()
        // {
        //     return View("../Cuisine/Detail");
        // }
        [HttpPost("/addRestaurant")]
        public ActionResult Create(string RestaurantName, string Location, int Cuisine)
        {
            Restaurant newRestaurant = new Restaurant(RestaurantName, Location, Cuisine);
            newRestaurant.SaveRestaurant();
            List<Restaurant> allRestaurants = Restaurant.GetAllRestaurants();
            return View("Index", allRestaurants);

        }
        [HttpGet("Cuisine/{id}/restaurant/new")]
        public ActionResult Show(int cuisineId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Cuisine selectedCuisine = Cuisine.Find(cuisineId);
            List<Restaurant> selectedRestaurants = Cuisine.GetRestaurantsByCuisine(cuisineId);
            model.Add("cuisine", selectedCuisine);
            model.Add("restaurants", selectedRestaurants);
            return View("../Cuisine/Detail", model);
        }
    }
}