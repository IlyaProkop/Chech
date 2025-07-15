using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Game.Runtime.Infrastructure.LoadingTasks
{
    public class DelegateTask : ILoadingTask
    {
        private readonly Func<UniTask> _delegate;

        [Preserve]
        public DelegateTask(Func<UniTask> @delegate)
        {
            _delegate = @delegate;
        }

        public UniTask LoadAsync()
        {
            return _delegate();
        }
    }
}
