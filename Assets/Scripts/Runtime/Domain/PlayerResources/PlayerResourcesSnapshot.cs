using System;
using System.Collections.Generic;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public struct PlayerResourcesSnapshot
    {
        public Dictionary<string, ulong> Resources;
    }
}
