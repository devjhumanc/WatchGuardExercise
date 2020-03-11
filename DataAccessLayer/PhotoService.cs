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
        public async Task<RoverPhotos> GetRoverPhotosByDate(CancellationToken ct, DateTime date)
        {
            RoverPhotos roverPhotos = new RoverPhotos();

            try
            {
                var stringDate = date.ToString("yyyy-MM-dd");
                var jsonString = await GetJsonData("https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=" + stringDate + "&api_key=yhxmCdrK4XW0M24avWI9KBqamjGgWC89dNB9jC6I");
                roverPhotos = (RoverPhotos)ConvertJsonToObject<RoverPhotos>(jsonString);
            }
            catch (Exception ex)
            {
                var exectption = ex.ToString();
            }

            return roverPhotos;
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
    }
}
