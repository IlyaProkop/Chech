using Cysharp.Threading.Tasks;
using UnityEngine.Scripting;

namespace Game.Runtime.Infrastructure.LoadingTasks
{
    public class LoadingTaskSequence : ILoadingTask
    {
        private readonly ILoadingTask[] _tasks;

        [Preserve]
        public LoadingTaskSequence(params ILoadingTask[] tasks)
        {
            _tasks = tasks;
        }

        public async UniTask LoadAsync()
        {
            foreach (var loadingTask in _tasks)
            {
                await loadingTask.LoadAsync();
            }
        }
    }
}
