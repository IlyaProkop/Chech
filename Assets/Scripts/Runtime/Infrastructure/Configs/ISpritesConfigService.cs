using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Runtime.Infrastructure.Configs
{
    public interface ISpritesConfigService
    {
        UniTask Initialize();
        Sprite GetSprite(string id);
        Sprite GetMockSprite();
    }
}