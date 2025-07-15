using System;

namespace Game.Runtime.Infrastructure.Time
{
    public interface ITimeService
    {
        DateTime CurrentTime { get; }
        void Initialize();
    }
}