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
            return View(new CountryViewModel());
        }

        [HttpGet]
        public JsonResult Search(string searchedCountry)
        {
            var countries = countryService.GetCountries(searchedCountry);
            return Json(countries);
        }

        [HttpGet]
        public IActionResult SearchedCountry(int countryId)
        {
            var country = countryService.GetCountryById(countryId);
            var model = new CountryViewModel
            {
                Name = country.Name,
                CountryCode = country.Code,
                Currency = country.Currency
            };
            return View("Index", model);
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
