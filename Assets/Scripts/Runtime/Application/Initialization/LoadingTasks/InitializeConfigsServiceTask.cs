using Cysharp.Threading.Tasks;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.LoadingTasks;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Initialization.LoadingTasks
{
    public class InitializeConfigsServiceTask : ILoadingTask
    {
        private readonly IConfigsService _configsService;
        private readonly ISpritesConfigService _spritesConfigService;

        [Preserve]
        public InitializeConfigsServiceTask(IConfigsService configsService, ISpritesConfigService spritesConfigService)
        {
            _configsService = configsService;
            _spritesConfigService = spritesConfigService;
        }

        public UniTask LoadAsync()
        {
            return UniTask.WhenAll(_spritesConfigService.Initialize(), _configsService.Initialize());
        }
    }
}
