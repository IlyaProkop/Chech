using Cysharp.Threading.Tasks;

namespace Game.Runtime.Infrastructure.Configs
{
    public interface IConfigsService
    {
        UniTask Initialize();
        T Get<T>();
    }
}