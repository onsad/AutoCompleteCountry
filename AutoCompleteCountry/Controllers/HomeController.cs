using AutoCompleteCountry.Models;
using AutoCompleteCountry.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoCompleteCountry.Controllers
{
    public class HomeController(CountryService countryService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Search(string searchedCountry)
        {
            List<Country> country = new List<Country> { new Country { Id = 1, Name = "Czechia" } };
            return Json(country);
        }

        [HttpGet]
        public IActionResult SearchedCountry(int countryId)
        {
            List<Country> country = new List<Country> { new Country { Id = 1, Name = "Czechia" } };
            return Json(country);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
