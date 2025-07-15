using Game.Runtime.Application.Game;
using Game.Runtime.Application.Resources;
using Game.Runtime.Application.SaveGame;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;
using System;
using Unity.Entities;



namespace Game.Runtime.Application.DI
{
    public class GameSceneLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PanelsService panelsService;

        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("GameSceneLifetimeScope Configure");
            builder.Register<IIocFactory, VContainerFactory>(Lifetime.Scoped);

            builder.Register<HeroData>(Lifetime.Scoped);

            // Регистрируем репозитории
            builder.Register<IRepositoryService, HeroRepository>(Lifetime.Scoped);
            builder.Register<IHeroRepository, HeroRepository>(Lifetime.Scoped);

            // Регистрируем UseCases
            builder.Register<UpgradeHeroUseCase>(Lifetime.Scoped);

            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var heroEntity = entityManager.CreateEntity();
            entityManager.AddComponentData(heroEntity, new HeroComponent { Level = 1, Strength = 10, Health = 100 });
            builder.RegisterInstance(heroEntity);

            // Регистрируем View как интерфейс
            builder.RegisterComponentInHierarchy<HeroView>()
                   .AsImplementedInterfaces();

            builder.RegisterInstance(InstantiatePanelsService());

            // MessagePipe для команд (UpgradeHeroCommand)
            var upgradeHeroCommand = new SimpleMessageBroker<UpgradeHeroCommand>();
            builder.RegisterInstance<IPublisher<UpgradeHeroCommand>>(upgradeHeroCommand);
            builder.RegisterInstance<ISubscriber<UpgradeHeroCommand>>(upgradeHeroCommand);

            // MessagePipe для событий (HeroUpgradedEvent)
            var heroUpgradedEvent = new SimpleMessageBroker<HeroUpgradedEvent>();
            builder.RegisterInstance<IPublisher<HeroUpgradedEvent>>(heroUpgradedEvent);
            builder.RegisterInstance<ISubscriber<HeroUpgradedEvent>>(heroUpgradedEvent);

            // ECS-система получает доступ к IPublisher<HeroUpgradedEvent> через сервис-локатор
            MessagePipeServiceLocator.HeroStatsChangedPublisher = heroUpgradedEvent;

            // Регистрируем Presenters
            builder.Register<HeroPresenter>(Lifetime.Scoped);

            builder.RegisterInstance(new UpgradeHeroMessageHandler(entityManager));

            ConfigureDomainControllers(builder);

            builder.RegisterEntryPoint<GameSaveController>(Lifetime.Scoped);
            builder.RegisterEntryPoint<GameController>(Lifetime.Scoped).AsSelf();
        }

        private void ConfigureDomainControllers(IContainerBuilder builder)
        {
            builder.Register<PlayerResourcesController>(Lifetime.Scoped).AsSelf().As<ISaveable>();
        }

        private IPanelsService InstantiatePanelsService()
        {
            var service = Instantiate(panelsService);
            DontDestroyOnLoad(service.gameObject);

            return service;
        }
    }
}
public class SimpleMessageBroker<T> : IPublisher<T>, ISubscriber<T>
{
    private event System.Action<T> _event;

    public void Publish(T message) => _event?.Invoke(message);

    public IDisposable Subscribe(System.Action<T> action)
    {
        _event += action;
        return new Unsubscriber(() => _event -= action);
    }

    // Реализация для ISubscriber<T>
    public IDisposable Subscribe(IMessageHandler<T> handler, params MessageHandlerFilter<T>[] filters)
    {
        System.Action<T> action = handler.Handle;
        _event += action;
        return new Unsubscriber(() => _event -= action);
    }

    private class Unsubscriber : System.IDisposable
    {
        private readonly System.Action _unsubscribe;
        public Unsubscriber(System.Action unsubscribe) => _unsubscribe = unsubscribe;
        public void Dispose() => _unsubscribe();
    }
}