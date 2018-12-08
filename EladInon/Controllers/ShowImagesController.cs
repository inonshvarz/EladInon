using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EladInon.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EladInon.Controllers
{
    public class ShowImagesController : Controller
    {
        private PhotoContext context;

        public ShowImagesController(PhotoContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> Search(IEnumerable<string>  locations, IEnumerable<string> sessions)
        public async Task<IActionResult> Search()
        {
            var picture = context.Pictures.ToListAsync();
            return null;
        }
    }
}