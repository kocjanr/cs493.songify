using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using cs493.songify.Models;

namespace cs493.songify.Controllers
{
     public class SongsController : Controller
     {
          public IActionResult Index()
          {
               return View();
          }

          public DataContractJsonSerializer getSong()
          {
               AWS aws = new AWS();

               Song song = new Song();
               song.Artist = "TheSkyCouldFly";
               song.Album = "Geodesic";


               MemoryStream stream = new MemoryStream();
               DataContractJsonSerializer JSON = new DataContractJsonSerializer(typeof(Song));
               JSON.WriteObject(stream,song);

               return JSON;

          }
     }
}