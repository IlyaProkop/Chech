using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using TriInspector;
using Game.Runtime.Infrastructure.Configs;
using UnityEditor;
using UnityEngine;

namespace Game.Runtime.Application.Configs
{
    [CreateAssetMenu(fileName = nameof(SpritesConfigService), menuName = "Game/" + nameof(SpritesConfigService),
        order = 0)]
    public class SpritesConfigService : ScriptableObject, ISpritesConfigService
    {
        [SerializeField] private Sprite _mockSprite;

        [SerializeField] private List<SpriteItem> _sprites = new();

        [SerializeField] private List<string> _spriteFolders;

        public UniTask Initialize()
        {
            return UniTask.CompletedTask;
        }

        public Sprite GetSprite(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                foreach (var sprite in _sprites)
                {
                    if (sprite.Id == id)
                    {
                        return sprite.Sprite;
                    }
                }
            }

            Debug.LogWarning($"[SpritesConfigService] Can't find sprite for id '{id}'");
            return _mockSprite;
        }

        public Sprite GetMockSprite()
        {
            return _mockSprite;
        }

#if UNITY_EDITOR

        [Button]        
        private void FillAllSprites()
        {
            _sprites.Clear();

            foreach (var folder in _spriteFolders)
            {
                var sprites = GetSpritesInFolder(folder);

                for (int i = 0; i < sprites.Length; i++)
                {
                    _sprites.Add(new SpriteItem(sprites[i].name, sprites[i]));
                }
            }

            EditorUtility.SetDirty(this);
        }

        private Sprite[] GetSpritesInFolder(params string[] folders)
        {
            var guids = AssetDatabase.FindAssets("t: Sprite", folders.Select(path => path).ToArray());

            return guids.Select(GetSpriteByGuid).ToArray();
        }

        private Sprite GetSpriteByGuid(string guid)
        {
            return AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid));
        }

#endif

        [Serializable]
        public class SpriteItem
        {
            [SerializeField] private string _id;
            [SerializeField] private Sprite _sprite;

            public SpriteItem(string id, Sprite sprite)
            {
                _id = id;
                _sprite = sprite;
            }

            public string Id => _id;
            public Sprite Sprite => _sprite;
        }
    }
}