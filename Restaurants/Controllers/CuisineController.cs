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
        // [HttpGet("/Cuisine/{id}")]
        // public ActionResult Show(int id)
        // {
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine selectedCuisine = Cuisine.Find(id);
        //     List<Restaurant> CuisineRestaurants = selectedCuisine.GetAllRestaurants();
        //     model.Add("Cuisine", selectedCuisine);
        //     model.Add("items", CuisineItems);
        //     return View(model);
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