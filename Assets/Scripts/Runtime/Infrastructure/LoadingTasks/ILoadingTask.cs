using Cysharp.Threading.Tasks;

namespace Game.Runtime.Infrastructure.LoadingTasks
{
    public interface ILoadingTask
    {
        UniTask LoadAsync();
    }
}
