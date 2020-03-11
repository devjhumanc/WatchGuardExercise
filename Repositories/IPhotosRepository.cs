using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WatchGuardExercise.Models;

namespace WatchGuardExercise.Repositories
{
    public interface IPhotosRepository
    {
        Task<RoverPhotos> GetRoverPhotosByDate(CancellationToken ct, DateTime date);
    }
}
