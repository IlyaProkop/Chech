using System;
using System.Collections.Generic;
using Game.Runtime.Domain.Common;
using UnityEngine;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public class PlayerResources : ISnapshotable<PlayerResourcesSnapshot>
    {
        public event Action<string, ulong, ulong> ResourceCountAdded;

        public event Action<string, ulong, ulong> ResourceCountRemoved;

        private Dictionary<string, ulong> _resources = new Dictionary<string, ulong>();

        public IReadOnlyDictionary<string, ulong> Resources => _resources;

        public void Add(IEnumerable<Resource> resources)
        {
            foreach (var resource in resources)
            {
                Add(resource);
            }
        }

        public void Add(Resource resource)
        {
            if (_resources.ContainsKey(resource.Id))
            {
                _resources[resource.Id] += resource.Count;
            }
            else
            {
                _resources.Add(resource.Id, resource.Count);
            }

            ResourceCountAdded?.Invoke(resource.Id, resource.Count, _resources[resource.Id]);
        }

        public void Remove(Resource resource)
        {
            if (_resources.ContainsKey(resource.Id))
            {
                if (_resources[resource.Id] < resource.Count)
                {
                    Debug.LogError($"[PlayerResources] Can't remove {resource.Id} {resource.Count} because player has {_resources[resource.Id]}");
                    _resources[resource.Id] = 0;
                }
                else
                {
                    _resources[resource.Id] -= resource.Count;
                }

                var totalCount = _resources[resource.Id];
                if (_resources[resource.Id] <= 0)
                {
                    _resources.Remove(resource.Id);
                    totalCount = 0;
                }

                ResourceCountRemoved?.Invoke(resource.Id, resource.Count, totalCount);
            }
        }

        public bool HasEnough(Resource resource)
        {
            return _resources.ContainsKey(resource.Id) && _resources[resource.Id] >= resource.Count;
        }

        public ulong GetCount(string key)
        {
            return _resources.TryGetValue(key, out var count) ? count : 0u;
        }

        public PlayerResourcesSnapshot GetSnapshot()
        {
            return new PlayerResourcesSnapshot()
            {
                Resources = _resources,
            };
        }

        public void RestoreFromSnapshot(PlayerResourcesSnapshot snapshot)
        {
            _resources = snapshot.Resources;
        }
    }
}
