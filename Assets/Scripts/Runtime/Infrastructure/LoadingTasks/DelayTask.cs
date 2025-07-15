using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Game.Runtime.Infrastructure.LoadingTasks
{
    public class DelayTask : ILoadingTask
    {
        private readonly TimeSpan _delay;

        [Preserve]
        public DelayTask(TimeSpan delay)
        {
            _delay = delay;
        }

        public UniTask LoadAsync()
        {
            return UniTask.Delay(_delay);
        }
    }
}
