

using EventHubApp.Web.ViewModels.Admin.UserManagement;

namespace EventHubApp.Services.Core.Admin.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserManagementIndexViewModel>> GetUserManagementBoardDataAsync(string userId);

        Task<IEnumerable<string>> GetManagerEmailsAsync();

        Task<bool> AssignUserToRoleAsync(RoleSelectionInputModel inputModel);
    }
}
