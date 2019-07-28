using Microsoft.AspNetCore.Http;
using Panda2.Models;
using Panda2.Models.ViewModels;

namespace Panda2.Services.Contracts
{
    public interface IUsersService
    {
        UserViewModel GetUserViewModelByIdWithPackages(string id);

        AdministratorViewModel GetAdministratorViewModel();

        PandaUserRole GetRoleByUserId(string id);

        CookieOptions ConfirmLogout();
    }
}
