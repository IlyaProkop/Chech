using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Resources
{
    public class PlayerResourcesController : ISaveable
    {
        private readonly IRepositoryService _repositoryService;

        public PlayerResources PlayerResources { get; private set; }

        [Preserve]
        public PlayerResourcesController(IRepositoryService repositoryService, IConfigsService configsService)
        {
            _repositoryService = repositoryService;
        }

        public void Save()
        {
            _repositoryService.Save(PlayerResources.GetSnapshot());
        }

        public void Initialize()
        {
            PlayerResources = new PlayerResources();
            if (_repositoryService.TryLoad<PlayerResourcesSnapshot>(out var snapshot))
            {
                PlayerResources!.RestoreFromSnapshot(snapshot);
            }
            else
            {
                PlayerResources.Add(new Resource(Constants.Resources.SoftCurrency, 3000));
            }
        }
    }
}
