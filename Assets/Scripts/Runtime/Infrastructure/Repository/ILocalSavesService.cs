using Cysharp.Threading.Tasks;

namespace Game.Runtime.Infrastructure.Repository
{
    public interface ILocalSavesService
    {
        UniTask LoadAll();
        UniTask SaveAll();
        UniTask DeleteAll();
    }
}
