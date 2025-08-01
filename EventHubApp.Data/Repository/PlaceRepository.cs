﻿

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;

namespace EventHubApp.Data.Repository
{
    public class PlaceRepository : BaseRepository<Place, Guid>, IPlaceRepository
    {
        public PlaceRepository(EventHubAppDbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
