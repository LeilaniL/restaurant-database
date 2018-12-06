using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Models;

namespace Restaurants.Controllers
{
    public class CuisineController : Controller
    {
        [HttpGet("Cuisine/new")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost("/addCuisine")]
        public ActionResult Create(string cuisineName)
        {
            Cuisine newCuisine = new Cuisine(cuisineName);
            newCuisine.Save();
            List<Cuisine> allCuisine = Cuisine.GetAll();
            return View("Index", allCuisine);

        }

        [HttpGet("/showCuisine")]
        public ActionResult Show()
        {
            List<Cuisine> allCuisine = Cuisine.GetAll();
            return View("Index", allCuisine);

        }
        [HttpGet("/Cuisine/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Cuisine selectedCuisine = Cuisine.Find(id);
            List<Restaurant> CuisineRestaurants = Cuisine.GetRestaurantsByCuisine(id);
            model.Add("Cuisine", selectedCuisine);
            model.Add("restaurant", CuisineRestaurants);
            return View(model);
        }

        // [HttpGet("/Cuisine/{cuisine_id}/Restaurant/{restaurant_id}")]
        // public ActionResult ShowRestaurant(int cuisine_id, int restaurant_id)
        // {

        //     Console.WriteLine("Controller worked");
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine selectedCuisine = Cuisine.Find(cuisine_id);
        //     Restaurant selectRestaurant = Restaurant.FindRestaurant(restaurant_id);
        //     model.Add("Cuisine", selectedCuisine);
        //     model.Add("restaurant", selectRestaurant);
        //     return View("Show", model);

        // }
        // [HttpGet("Cuisine/{id}/restaurant/new")]
        // public ActionResult Show(int cuisineId)
        // {
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine selectedCuisine = Cuisine.Find(cuisineId);
        //     List<Restaurant> selectedRestaurants = Cuisine.GetRestaurantsByCuisine(cuisineId);
        //     model.Add("cuisine", selectedCuisine);
        //     model.Add("restaurants", selectedRestaurants);
        //     return View("../restaurant/new", model);
        // }
        // }

        // [HttpPost("/Cuisine/{cuisineId}/Cuisine")]
        // public ActionResult Create(int cuisineId, string cuisineDescription)
        // {
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine foundCuisine = Cuisine.Find(cuisineId);
        //     Cuisine newCuisine = new Cuisine(cuisineDescription);
        //     foundCuisine.AddCuisine(newCuisine);
        //     List<Cuisine> categoryCuisines = foundCuisine.GetItems();
        //     model.Add("items", categoryItems);
        //     model.Add("category", foundCategory);
        //     return View("Show", model);
        // 
    }
}