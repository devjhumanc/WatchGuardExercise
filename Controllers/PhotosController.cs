using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WatchGuardExercise.BusinessLogicLayer;
using WatchGuardExercise.DataAccessLayer;
using WatchGuardExercise.Models;
using WatchGuardExercise.Repositories;
using WatchGuardExercise.ViewModel;

namespace WatchGuardExercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        IPhotosRepository _propertyRepository;

        [HttpGet]
        [Route("{date}")]
        public async Task<List<RoverPhotosViewModel>> GetPhotosByDate(CancellationToken ct, DateTime date) 
        {
            List<RoverPhotosViewModel> roverPhotos = new List<RoverPhotosViewModel>();
            _propertyRepository = new PhotoService();

            Photos photos = new Photos();
            roverPhotos = await photos.GetRoverPhotosByDate(ct, _propertyRepository, date);
            
            return roverPhotos;
        }
    }
}