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
        public async Task<JsonResult> Search(string searchedCountry)
        {
            var countries = await countryService.GetCountries(searchedCountry);
            return Json(countries);
        }

        [HttpGet]
        public async Task<IActionResult> SearchedCountry(int countryId)
        {
            if (countryId == 0)
            {
                ModelState.AddModelError(string.Empty, "Please select a country from the list.");
                return View("Index", new CountryViewModel());
            }
            else
            {
                var country = await countryService.GetCountryById(countryId);
                var model = new CountryViewModel
                {
                    Name = country.Name,
                    CountryCode = country.Code,
                    Currency = country.Currency,
                    CapitalCity = country.CapitalCity
                };
                return View("Index", model);
            }
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
