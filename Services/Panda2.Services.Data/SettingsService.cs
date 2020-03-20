using Panda2.Services.Data.Contracts;

namespace Panda2.Services.Data
{
    using System.Linq;

    using Panda2.Data.Common.Repositories;
    using Panda2.Data.Models;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.All().Count();
        }
    }
}
