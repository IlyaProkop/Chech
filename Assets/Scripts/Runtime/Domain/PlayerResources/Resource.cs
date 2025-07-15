using System;

namespace Game.Runtime.Domain.PlayerResources
{
    [Serializable]
    public struct Resource
    {
        public readonly string Id;
        public ulong Count;

        public Resource(string id, ulong count)
        {
            Id = id;
            Count = count;
        }
    }
}
