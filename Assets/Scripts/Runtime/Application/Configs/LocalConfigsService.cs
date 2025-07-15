using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TriInspector;
using Game.Runtime.Application.Configs.LocalSOConfigs;
using Game.Runtime.Infrastructure.Configs;
using UnityEngine;

namespace Game.Runtime.Application.Configs
{
    [CreateAssetMenu(fileName = nameof(LocalConfigsService), menuName = "Game/" + nameof(LocalConfigsService),
        order = 0)]
    public class LocalConfigsService : ScriptableObject, IConfigsService
    {
        [SerializeField]
        private ResourcesConfigsSO _resourcesConfigsSO;

        private readonly Dictionary<Type, object> _cachedConfigs = new();

        public UniTask Initialize()
        {
            InitializeInternal();

            return UniTask.CompletedTask;
        }

        public T Get<T>()
        {
            return (T)_cachedConfigs[typeof(T)];
        }

        #if UNITY_EDITOR
        [Button]
        public void InitializeEditor()
        {
            InitializeInternal();
        }
        #endif

        private void InitializeInternal()
        {
            _cachedConfigs.Clear();           
            AddToCache(_resourcesConfigsSO.ResourcesConfigs);
        }

        private void AddToCache<T>(T config)
        {
            _cachedConfigs.Add(config.GetType(), config);
        }
    }
}
