using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EladInon.Models;

namespace EladInon.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowImages()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "כאן צריך לכתוב אודות האתר שלנו";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "פרטי יצירת קשר";

            return View();
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




