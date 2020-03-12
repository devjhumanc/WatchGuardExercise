using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WatchGuardExercise.Models;
using WatchGuardExercise.Repositories;
using WatchGuardExercise.ViewModel;

namespace WatchGuardExercise.BusinessLogicLayer
{
    public class Photos
    {
        public async Task<List<RoverPhotosViewModel>> GetRoverPhotosByDate(CancellationToken ct, IPhotosRepository repository, DateTime date, IWebHostEnvironment env, IConfiguration config) 
        {
            List<RoverPhotosViewModel> roverPhotosViewModels = new List<RoverPhotosViewModel>();

            //1. check if the folder with that date exits
            var stringDate = date.ToString("yyyy-MM-dd");
            bool folderExists = Directory.Exists(env.WebRootPath + "/Images/" + stringDate);
            if (!folderExists)
            {
                await repository.GetRoverPhotosByDate(ct, stringDate, env, config);
            }

            //get photos from the folder
            var files = Directory.GetFiles(env.WebRootPath + "/Images/" + stringDate);
            
            foreach (var photo in files)
            {
                RoverPhotosViewModel rvm = new RoverPhotosViewModel();
                var fullFileName = Path.GetFileName(photo);
                var fileName = fullFileName.Split('.')[0];
                rvm.PhotoId = Convert.ToInt32(fileName);
                rvm.ImageSrc = config["ProdURL:URL"].ToString() + "Images/" + stringDate + "/" + fullFileName;
                rvm.EarthDate = Convert.ToDateTime(stringDate);
                roverPhotosViewModels.Add(rvm);
            }
            return roverPhotosViewModels;
        }

    }
}
