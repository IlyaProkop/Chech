using Game.Runtime.Application.Configs;
using Game.Runtime.Application.Initialization;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Infrastructure.Time;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Repository;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Runtime.Application.DI
{
    public class InitSceneLifetimeScope : LifetimeScope
    {       
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IIocFactory, VContainerFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<InitializationController>();
        }
    }
}
