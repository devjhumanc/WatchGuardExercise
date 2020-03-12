using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        Task GetRoverPhotosByDate(CancellationToken ct, string date, IWebHostEnvironment env, IConfiguration configuration);
    }
}
