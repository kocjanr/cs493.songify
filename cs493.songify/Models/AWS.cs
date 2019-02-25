using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Internal;


namespace cs493.songify.Models
{
     public class AWS
     {
          private String _rootBucket = "cs493.songify.library";
          public String LastFileUploadedUrl;

          public async Task UploadToS3(String filePath, String key)
          {
               AWSCredentials credentials = new BasicAWSCredentials(
                    Environment.GetEnvironmentVariable("AWSAccessKey"),
                    Environment.GetEnvironmentVariable("AWSSecretKey"));

               IAmazonS3 client = new AmazonS3Client(
                    credentials,
                    Amazon.RegionEndpoint.USEast1
               );

               var request = new PutObjectRequest
               {
                    BucketName = _rootBucket,
                    Key = key,
                    FilePath = filePath
               };

               var putObjectResponse = client.PutObjectAsync(request).Result;
               if (putObjectResponse != null)
               {
                    GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                    {
                         BucketName = _rootBucket,
                         Key = key,
                         Expires = DateTime.Now.AddMinutes(1)
                    };

                    LastFileUploadedUrl = client.GetPreSignedURL(request1);
               }
          }

          public void InsertDynamoRecord(String url, String artist, String album, String genre, String songTitle)
          {
               AWSCredentials credentials = new BasicAWSCredentials(
                    Environment.GetEnvironmentVariable("AWSAccessKey"),
                    Environment.GetEnvironmentVariable("AWSSecretKey")
               );

               AmazonDynamoDBClient client = new AmazonDynamoDBClient(
                    credentials,
                    Amazon.RegionEndpoint.USEast1
               );


               Table musicTable = Table.LoadTable(client, "music");

               var song = new Document();
               song["Artist"] = artist;
               song["SongTitle"] = songTitle;
               song["Genre"] = genre;
               song["Album"] = album;
               song["S3Link"] = url;

              var request =  musicTable.PutItemAsync(song);
          }
    
     }
}

