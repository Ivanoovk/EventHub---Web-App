

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Services.Core
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        public async Task<Guid?> GetIdByUserIdAsync(string? userId)
        {
            Guid? managerId = null;
            if (!String.IsNullOrWhiteSpace(userId))
            {
                Manager? manager = await this.managerRepository
                    .FirstOrDefaultAsync(m => m.UserId.ToLower() == userId.ToLower());
                if (manager != null)
                {
                    managerId = manager.Id;
                }
            }

            return managerId;
        }

        public async Task<bool> ExistsByIdAsync(string? id)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(id))
            {
                result = await this.managerRepository
                    .GetAllAttached()
                    .AnyAsync(m => m.Id.ToString().ToLower() == id.ToLower());
            }

            return result;
        }

        public async Task<bool> ExistsByUserIdAsync(string? userId)
        {
            bool result = false;
            if (!String.IsNullOrWhiteSpace(userId))
            {
                result = await this.managerRepository
                    .GetAllAttached()
                    .AnyAsync(m => m.UserId.ToLower() == userId.ToLower());
            }

            return result;
        }
    }
}
