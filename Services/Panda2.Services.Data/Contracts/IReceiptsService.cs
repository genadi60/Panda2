using System.Threading.Tasks;
using Panda2.Data.Models.ViewModels;

namespace Panda2.Services.Data.Contracts
{
    public interface IReceiptsService
    {
        UserViewModel GetUserViewModelByIdWithReceipts(string id);

        ReceiptViewModel GetReceiptViewModel(string id);

        Task<int> Create(string id, string userId);
    }
}
