﻿

using EventHubApp.Data.Models;
using EventHubApp.Data.Repository.Interfaces;
using EventHubApp.Services.Core.Admin.Interfaces;
using EventHubApp.Web.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventHubApp.Services.Core.Admin
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IManagerRepository managerRepository;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
            IManagerRepository managerRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.managerRepository = managerRepository;
        }

        public async Task<IEnumerable<UserManagementIndexViewModel>> GetUserManagementBoardDataAsync(string userId)
        {
            IEnumerable<UserManagementIndexViewModel> users = await this.userManager
                .Users
                .Where(u => u.Id.ToLower() != userId.ToLower())
                .Select(u => new UserManagementIndexViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Roles = userManager.GetRolesAsync(u)
                        .GetAwaiter()
                        .GetResult()
                })
                .ToArrayAsync();

            return users;
        }

        public async Task<IEnumerable<string>> GetManagerEmailsAsync()
        {
            IEnumerable<string> managerEmails = await this.managerRepository
                .GetAllAttached()
                .Where(m => m.User.UserName != null)
                .Select(m => (string)m.User.UserName!)
                .ToArrayAsync();

            return managerEmails;
        }

        public async Task<bool> AssignUserToRoleAsync(RoleSelectionInputModel inputModel)
        {
            ApplicationUser? user = await this.userManager
                .FindByIdAsync(inputModel.UserId);

            if (user == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            bool roleExists = await this.roleManager.RoleExistsAsync(inputModel.Role);
            if (!roleExists)
            {
                throw new ArgumentException("Selected role is not a valid role!");
            }

            try
            {
                await this.userManager.AddToRoleAsync(user, inputModel.Role);

                return true;
            }
            catch (Exception e)
            {
                throw new ArgumentException(
                    "Unexpected error occurred while adding the user to role! Please try again later!",
                    innerException: e);
            }
        }
    }
}
