using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cs493.songify.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cs493.songify.Controllers
{
     [Route("api/[controller]")]
     [EnableCors("CorsPolicy")]
     public class ValuesController : Controller
     {
          [HttpGet]
          [EnableCors("AllowSpecificOrigin")]
          public String Get()
          {
               AWS aws = new AWS();
               Song song = new Song();
               song.Artist = "TheSkyCouldFly";
               song.Album = "Geodesic";

               String json = JsonConvert.SerializeObject(song);

               return json;
          }
     }
}