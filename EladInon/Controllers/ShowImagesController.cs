using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> Index()
        {
            var pictures = context.Pictures.AsNoTracking();
            return View(await pictures.ToListAsync());
            
        }
    }
}