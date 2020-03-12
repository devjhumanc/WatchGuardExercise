using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WatchGuardExercise.BusinessLogicLayer;
using WatchGuardExercise.DataAccessLayer;
using WatchGuardExercise.Repositories;
using WatchGuardExercise.ViewModel;

namespace WatchGuardExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        IPhotosRepository _propertyRepository;
        IWebHostEnvironment _env;
        IConfiguration _config;
        public PhotosController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _config = configuration;
        }

        [HttpGet]
        [Route("{date}")]
        public async Task<List<RoverPhotosViewModel>> GetPhotosByDate(CancellationToken ct, DateTime date)
        {
            List<RoverPhotosViewModel> roverPhotos = new List<RoverPhotosViewModel>();
            _propertyRepository = new PhotoService();
            
            Photos photos = new Photos();
            roverPhotos = await photos.GetRoverPhotosByDate(ct, _propertyRepository, date, _env, _config);

            return roverPhotos;
        }
    }
}