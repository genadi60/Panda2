using System.Threading.Tasks;
using Panda2.Data.Models.InputModels;
using Panda2.Data.Models.ViewModels;

namespace Panda2.Services.Data.Contracts
{
    public interface IPackagesService
    {
        PackageViewModel GetPackage(string id);

        Task<int> Create(PackageInputModel model);

        Task<int> Ship(string id);

        Task<int> Deliver(string id);

        Task<int> Acquire(string id);
    }
}
