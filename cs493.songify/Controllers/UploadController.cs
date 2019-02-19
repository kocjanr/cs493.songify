using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cs493.songify.Models;
using Microsoft.AspNetCore.Cors;

namespace cs493.songify.Controllers
{
     public class UploadController : Controller
     {
          public IActionResult Index()
          {
               return View();
          }

          [HttpPost]
          public async Task<IActionResult> UploadSong(String artist, String album, IFormFile fileupload, String genre, String song)
          {
               String localPath = @"C:\Users\Ryan\Desktop\mp3s\";
               String filePath = localPath + fileupload.FileName;
               String key = artist + "-" + fileupload.FileName;


               AWS aws = new AWS();
               aws.UploadToS3(filePath,key);

               String preSignedUrl = aws.LastFileUploadedUrl;

               aws.InsertDynamoRecord(preSignedUrl,artist,album,genre,song);


               return RedirectToAction("Index", "Upload");
          }
     }
}