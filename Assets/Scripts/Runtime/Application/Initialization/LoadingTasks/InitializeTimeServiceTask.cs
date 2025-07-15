using Cysharp.Threading.Tasks;
using Game.Runtime.Infrastructure.LoadingTasks;
using Game.Runtime.Infrastructure.Time;
using UnityEngine.Scripting;

namespace Game.Runtime.Application.Initialization.LoadingTasks
{
    public class InitializeTimeServiceTask : ILoadingTask
    {
        private readonly ITimeService _localTimeService;

        [Preserve]
        public InitializeTimeServiceTask(ITimeService localTimeService)
        {
            _localTimeService = localTimeService;
        }

        public UniTask LoadAsync()
        {
            _localTimeService.Initialize();
            return UniTask.CompletedTask;
        }
    }
}
