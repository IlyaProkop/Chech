using Game.Runtime.Application.Configs;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Infrastructure.Time;
using Game.Runtime.Infrastructure.Utils;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.Application.DI
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private LocalConfigsService _localConfigsService;

        [SerializeField]
        private SpritesConfigService _spritesConfigService;

        [SerializeField]
        private UnityCallbacksService _callbacksServicePrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IIocFactory, VContainerFactory>(Lifetime.Singleton);
            builder.Register<RepositoryService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(InstantiateUnityCallbacksService());
            builder.RegisterEntryPoint<LocalTimeService>().As<ITimeService>().AsSelf();
           
            ConfigureConfigs(builder);
        }

        private UnityCallbacksService InstantiateUnityCallbacksService()
        {
            var service = Instantiate(_callbacksServicePrefab);
            DontDestroyOnLoad(service.gameObject);
            DontDestroyOnLoad(this.gameObject);

            return service;
        }

        private void ConfigureConfigs(IContainerBuilder builder)
        {
            builder.RegisterInstance<IConfigsService>(_localConfigsService);
            builder.RegisterInstance<ISpritesConfigService>(_spritesConfigService);
        }
    }
}
