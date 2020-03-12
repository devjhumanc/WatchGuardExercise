using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatchGuardExercise.Models;
using WatchGuardExercise.Repositories;

namespace WatchGuardExercise.DataAccessLayer
{
    public class PhotoService : IPhotosRepository
    {

        public async Task GetRoverPhotosByDate(CancellationToken ct, string stringDate, IWebHostEnvironment env, IConfiguration configuration)
        {
            try
            {
                RoverPhotos roverPhotos = new RoverPhotos();
                var nasaURL = configuration["NASA:URL"].ToString();
                var apikey = configuration["NASA:API_Key"].ToString();
                
                var jsonString = await GetJsonData(nasaURL + "?earth_date=" + stringDate + "&api_key=" + apikey);
                roverPhotos = (RoverPhotos)ConvertJsonToObject<RoverPhotos>(jsonString);

                Directory.CreateDirectory(env.WebRootPath + "/Images/" + stringDate);
                
                foreach (var photo in roverPhotos.photos)
                {
                    var url = photo.img_src;
                    var filename = photo.id + ".JPG";

                    var savePath = env.WebRootPath + "/Images/" + stringDate + "/" + filename;
                    DownloadAndSaveImage(url, savePath);
                }
            }
            catch (Exception ex)
            {
                //todo: log to db or files
                var exectption = ex.ToString();
            }
        }

        private async Task<JObject> GetJsonData(string url)
        {
            var message = "";

            var webReq = (HttpWebRequest)WebRequest.Create(url);
            using (WebResponse response = await webReq.GetResponseAsync())
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8);
                message = readStream.ReadToEnd();
            }

            JObject jsonData = JObject.Parse(message);

            return jsonData;
        }

        private object ConvertJsonToObject<T>(JObject jObject)
        {
            T tObject = JsonConvert.DeserializeObject<T>(jObject.ToString());
            return tObject;
        }

        private void DownloadAndSaveImage(string url, string savePath)
        {
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile(url, savePath);
        }
    }
}
