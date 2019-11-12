using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
// The below line is needed for configuring EF
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Crudelicious.Models;

namespace Crudelicious.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
             List<dish> food = dbContext.Dishes.ToList();
             ViewBag.All = food;
            return View();
        }

        [HttpGet("NewDish")]
        public IActionResult NewDish()
        {
            return View("NewDish");
        }

        [HttpPost("create")]
        public IActionResult Create(dish d)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(d);
                dbContext.SaveChanges();
                // do somethng!  maybe insert into db?  then we will redirect
                Console.WriteLine("************** Model is Valid **************");
                return Redirect("/");
            }
            else
            {
                // Oh no!  We need to return a ViewResponse to preserve the ModelState, and the errors it now contains!
                Console.WriteLine("************** Model is Invalid **************");
                return View("NewDish");
            }
        }

        [HttpGet("DishID/{DishID}")]
        public IActionResult ChefsPage(int DishID)
        {
            dish RetrievedDish = dbContext.Dishes.FirstOrDefault(selectedDish => selectedDish.DishID == DishID);
            ViewBag.selectedDish = RetrievedDish;
            return View("ChefsPage");
        }

        [HttpGet("Edit/{DishID}")]
        public IActionResult edit(int DishID)
        {
            dish RetrievedDish = dbContext.Dishes.FirstOrDefault(selectedDish => selectedDish.DishID == DishID);
            // ViewBag.selectedDish = RetrievedDish;        
            return View(RetrievedDish);
        }

        [HttpPost("updateDish/{DishID}")]
        public IActionResult updateDish(int DishID, dish updatedDish)
        // public IActionResult updateDish(dish dish, int DishID)
        {
            if(ModelState.IsValid)
            {
                dish RetrievedDish = dbContext.Dishes.FirstOrDefault(selectedDish => selectedDish.DishID == DishID);
                RetrievedDish.Name = updatedDish.Name;
                RetrievedDish.Chef = updatedDish.Chef;
                RetrievedDish.Tastiness = updatedDish.Tastiness;
                RetrievedDish.Calories = updatedDish.Calories;
                RetrievedDish.Description = updatedDish.Description;
                // RetrievedDish.Name = dish.Name;
                // RetrievedDish.Chef = dish.Chef;
                // RetrievedDish.Tastiness = dish.Tastiness;
                // RetrievedDish.Calories = dish.Calories;
                // RetrievedDish.Description = dish.Description;
                dbContext.SaveChanges();
                RetrievedDish.UpdatedAt = DateTime.Now;
                Console.WriteLine("************** Model is Valid **************");
                return Redirect($"/DishID/{DishID}");
            }
            else
            {
                Console.WriteLine("************** Model is Invalid **************");
                return View("Edit", updatedDish);
            }
        }

        [HttpGet("delete/{DishId}")]
        public IActionResult DeleteDish(int DishID)
        {
            dish RetrievedDish = dbContext.Dishes.SingleOrDefault(selectedDish => selectedDish.DishID == DishID);
            dbContext.Dishes.Remove(RetrievedDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        // here we can "inject" our context service into the constructor
        // Allowing us to use mySQL query links
        private MyContext dbContext;
     
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
    }
}
