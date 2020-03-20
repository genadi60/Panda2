using System;
using Panda2.Data.Common.Repositories;
using Panda2.Data.Models;
using Panda2.Data.Models.ViewModels;
using Panda2.Services.Data.Contracts;
using Panda2.Services.Mapping;
using System.Linq;
using System.Threading.Tasks;

namespace Panda2.Services.Data
{
    public class ReceiptsService : IReceiptsService
    {
        private readonly IDeletableEntityRepository<PandaUser> _userRepository;
        private readonly IDeletableEntityRepository<Package> _packageRepository;
        private readonly IDeletableEntityRepository<Receipt> _receiptRepository;
        private readonly IDeletableEntityRepository<Status> _statusRepository;

        public ReceiptsService(IDeletableEntityRepository<PandaUser> userRepository, IDeletableEntityRepository<Package> packageRepository, IDeletableEntityRepository<Receipt> receiptRepository, IDeletableEntityRepository<Status> statusRepository)
        {
            _userRepository = userRepository;
            _packageRepository = packageRepository;
            _receiptRepository = receiptRepository;
            _statusRepository = statusRepository;
        }

        public UserViewModel GetUserViewModelByIdWithReceipts(string id)
        {
            var model = _userRepository.All()
                .Where(u => u.Id.Equals(id))
                .To<UserViewModel>()
                .SingleOrDefault();

            return model;
        }

        public async Task<int> Create(string id, string userId)
        {
            var package = _packageRepository.All()
                .SingleOrDefault(p => p.Id.Equals(id) && p.RecipientId.Equals(userId));

            if (package == null)
            {
                return 0;
            }

            var price = (decimal)package.Weight * 2.67M;

            var receipt = new Receipt
            {
                Fee = price,
                RecipientId = package.RecipientId,
                Recipient = package.Recipient,
                PackageId = package.Id,
                Package = package,
            };

            var status = _statusRepository.All().SingleOrDefault(s => s.Name.Equals("Acquired"));

            package.Status = status;

            _packageRepository.Update(package);
            await _packageRepository.SaveChangesAsync();

            await _receiptRepository.AddAsync(receipt);
            return await _receiptRepository.SaveChangesAsync();
        }

        public ReceiptViewModel GetReceiptViewModel(string id)
        {
            var model = _receiptRepository.All()
                .Where(r => r.Id.Equals(id))
                .To<ReceiptViewModel>()
                .SingleOrDefault();

            return model;
        }
    }
}
