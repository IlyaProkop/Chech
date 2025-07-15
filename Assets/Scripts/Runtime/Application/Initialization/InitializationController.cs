using System;
using Cysharp.Threading.Tasks;
using Game.Runtime.Application.Initialization.LoadingTasks;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.LoadingTasks;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace Game.Runtime.Application.Initialization
{
    public class InitializationController : IInitializable
    {
        private const float LoaderDelaySeconds = 2f;

        private readonly IIocFactory _iocFactory;
        private readonly ILocalSavesService _localSavesService;

        [Preserve]
        public InitializationController(IIocFactory iocFactory, ILocalSavesService localSavesService)
        {
            _iocFactory = iocFactory;
            _localSavesService = localSavesService;
        }

        void IInitializable.Initialize()
        {
            UnityEngine.Application.targetFrameRate = 60;

            StartInitialization().Forget();
        }

        private async UniTaskVoid StartInitialization()
        {
            var initializationSequence = CreateInitializationSequence();
            var waitSequence = new LoadingTaskSequence(
                new DelayTask(TimeSpan.FromSeconds(LoaderDelaySeconds)));

            await UniTask.WhenAll(waitSequence.LoadAsync(), initializationSequence.LoadAsync());

            await _iocFactory.Create<LoadSceneTask, string>("Game").LoadAsync();
        }

        private ILoadingTask CreateInitializationSequence()
        {
            return new LoadingTaskSequence(
                _iocFactory.Create<InitializeConfigsServiceTask>(),
                new DelegateTask(_localSavesService.LoadAll),
                _iocFactory.Create<InitializeTimeServiceTask>());
        }
    }
}