using System;
using System.Collections.Generic;
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
        public async Task<List<RoverPhotosViewModel>> GetRoverPhotosByDate(CancellationToken ct, IPhotosRepository repository, DateTime date) 
        {
            List<RoverPhotosViewModel> roverPhotosViewModels = new List<RoverPhotosViewModel>();
            var photos = await repository.GetRoverPhotosByDate(ct, date);

            foreach (var photo in photos.photos)
            {
                RoverPhotosViewModel rvm = new RoverPhotosViewModel();
                rvm.PhotoId = photo.id;
                rvm.ImageSrc = photo.img_src;
                rvm.EarthDate = Convert.ToDateTime(photo.earth_date);

                roverPhotosViewModels.Add(rvm);
            }
            return roverPhotosViewModels;
        }
    }
}
