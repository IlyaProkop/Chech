using Game.Runtime.Domain.PlayerResources;
using UnityEngine;

namespace Game.Runtime.Application.Configs.LocalSOConfigs
{
    [CreateAssetMenu(fileName = nameof(ResourcesConfigsSO), menuName = "Game/Configs/" + nameof(ResourcesConfigsSO),
        order = 0)]
    public class ResourcesConfigsSO : ScriptableObject
    {
        public ResourcesConfigs ResourcesConfigs;
    }
}
