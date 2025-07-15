using System;
using UnityEngine.Scripting;

namespace Game.Runtime.Infrastructure.Time
{
    public class LocalTimeService : ITimeService
    {
        public DateTime CurrentTime => DateTime.UtcNow;

        public void Initialize()
        {
        }

        [Preserve]
        public LocalTimeService()
        {
        }
    }
}