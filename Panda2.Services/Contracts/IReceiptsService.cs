using System;
using System.Collections.Generic;
using System.Text;
using Panda2.Models.ViewModels;

namespace Panda2.Services.Contracts
{
    public interface IReceiptsService
    {
        UserViewModel GetUserViewModelByIdWithReceipts(string id);

        ReceiptViewModel GetReceiptViewModel(string id);

        bool Create(string id);
    }
}
