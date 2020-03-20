using Microsoft.AspNetCore.Http;
using Panda2.Data.Models;
using Panda2.Data.Models.ViewModels;

namespace Panda2.Services.Data.Contracts
{
    public interface IUsersService
    {
        UserViewModel GetUserViewModelByIdWithPackages(string id);

        AdministratorViewModel GetAdministratorViewModel();

        PandaUserRole GetRoleByUserId(string id);

        CookieOptions ConfirmLogout();
    }
}
