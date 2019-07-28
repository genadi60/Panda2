using Panda2.Models.InputModels;
using Panda2.Models.ViewModels;

namespace Panda2.Services.Contracts
{
    public interface IPackagesService
    {
        PackageViewModel GetPackage(string id);

        int Create(PackageInputModel model);

        int Ship(string id);

        int Deliver(string id);

        int Acquire(string id);
    }
}
