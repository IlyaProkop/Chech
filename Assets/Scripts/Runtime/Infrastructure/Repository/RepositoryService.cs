using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;

namespace Game.Runtime.Infrastructure.Repository
{
    public class RepositoryService : IRepositoryService, ILocalSavesService
    {
        private const string LocalSavesKey = "LocalSaves";

        private Dictionary<string, string> _cachedSaves = new();

        [Preserve]
        public RepositoryService()
        {
        }

        #region Cached saves

        public void Save<TData>(TData data, string saveKey = null)
        {
            saveKey ??= GetSaveKey<TData>();
            _cachedSaves[saveKey] = JsonConvert.SerializeObject(data);
        }

        public bool TryLoad<TData>(out TData loadedData, string saveKey = null)
        {
            saveKey ??= GetSaveKey<TData>();

            if (_cachedSaves.TryGetValue(saveKey, out var cashedSave))
            {
                loadedData = JsonConvert.DeserializeObject<TData>(cashedSave);
                return true;
            }

            loadedData = default;
            return false;
        }

        public bool HasData<TData>(string saveKey = null)
        {
            saveKey ??= GetSaveKey<TData>();
            return _cachedSaves.ContainsKey(saveKey);
        }

        public void Delete<TData>(string saveKey = null)
        {
            saveKey ??= GetSaveKey<TData>();

            if (_cachedSaves.ContainsKey(saveKey))
            {
                _cachedSaves.Remove(saveKey);
            }
        }

        private string GetSaveKey<TData>()
        {
            return typeof(TData).FullName;
        }

        #endregion

        #region Local saves

        UniTask ILocalSavesService.LoadAll()
        {
            try
            {
                if (PlayerPrefs.HasKey(LocalSavesKey))
                {
                    var serializedSaves = PlayerPrefs.GetString(LocalSavesKey);
                    _cachedSaves = JsonConvert.DeserializeObject<SavesData>(serializedSaves).CachedSaves;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"[Saves] Failed load local saves. Error: {e}");
            }

            return UniTask.CompletedTask;
        }

        UniTask ILocalSavesService.SaveAll()
        {
            try
            {
                var serializedData = JsonConvert.SerializeObject(new SavesData(_cachedSaves));
                PlayerPrefs.SetString(LocalSavesKey, serializedData);
                PlayerPrefs.Save();
            }
            catch (Exception e)
            {
                Debug.LogError($"[Saves] Failed save local saves. Error: {e}");
            }

            return UniTask.CompletedTask;
        }

        UniTask ILocalSavesService.DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            return UniTask.CompletedTask;
        }

        public string ExportLocalSavesToJson()
        {
            var json = JsonConvert.SerializeObject(new SavesData(_cachedSaves), Formatting.Indented);

            return json;
        }

        public void ImportLocalSavesFromJson(string json)
        {
            var saveData = JsonConvert.DeserializeObject<SavesData>(json);
            _cachedSaves = saveData.CachedSaves;
        }

        #if UNITY_EDITOR

        [MenuItem("Game/Saves/Clear saves")]
        public static void DeleteAllSaves()
        {
            PlayerPrefs.DeleteAll();
        }

        #endif

        #endregion

    }

    [Serializable]
    public struct SavesData
    {
        public readonly Dictionary<string, string> CachedSaves;

        [Preserve]
        public SavesData(Dictionary<string, string> cachedSaves)
        {
            CachedSaves = cachedSaves;
        }
    }
}
