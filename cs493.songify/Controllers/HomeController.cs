using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using cs493.songify.Models;
using cs493.songify.Controllers;

namespace cs493.songify.Controllers
{
     public class HomeController : Controller
     {
          public IActionResult Index()
          {
               SongsController sc = new SongsController();

               var xyz = sc.getSong();


               return View();
          }

          public IActionResult About()
          {
               ViewData["Message"] = "Your application description page.";

               return View();
          }

          public IActionResult Contact()
          {
               ViewData["Message"] = "Your contact page.";

               return View();
          }

          public IActionResult Error()
          {
               return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
          }
     }
}
