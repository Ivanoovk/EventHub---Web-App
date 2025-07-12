using EventHubApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubApp.Data.Repository.Interfaces
{
    public interface IWatchlistRepository
        : IRepository<ApplicationUserEvent, object>, IAsyncRepository<ApplicationUserEvent, object>
    {
        ApplicationUserEvent? GetByCompositeKey(string userId, string eventId);

        Task<ApplicationUserEvent?> GetByCompositeKeyAsync(string userId, string eventId);

        bool Exists(string userId, string eventId);

        Task<bool> ExistsAsync(string userId, string eventId);
    }
}
