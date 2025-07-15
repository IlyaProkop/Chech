using Game.Runtime.Infrastructure.Repository;
using UnityEngine;

/// <summary>
/// Универсальный репозиторий для хранения данных, а также специализированный для героя
/// </summary>
public class HeroRepository : IRepositoryService, IHeroRepository
{
    private const string HeroDataKey = "HeroData";

    /// <summary>
    /// Сохраняет любые данные по ключу
    /// </summary>
    public void Save<TData>(TData data, string key)
    {
        var json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Пытается загрузить данные по ключу
    /// </summary>
    public bool TryLoad<TData>(out TData data, string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var json = PlayerPrefs.GetString(key);
            data = JsonUtility.FromJson<TData>(json);
            return true;
        }
        data = default;
        return false;
    }

    /// <summary>
    /// Проверяет наличие данных по ключу
    /// </summary>
    public bool HasData<TData>(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    /// <summary>
    /// Удаляет данные по ключу
    /// </summary>
    public void Delete<TData>(string key)
    {
        if (PlayerPrefs.HasKey(key))
            PlayerPrefs.DeleteKey(key);
    }

    // --- IHeroRepository реализация через generic методы ---

    /// <summary>
    /// Получает данные героя
    /// </summary>
    public HeroData GetHeroData()
    {
        if (TryLoad<HeroData>(out var hero, HeroDataKey))
            return hero;
        return new HeroData();
    }

    /// <summary>
    /// Сохраняет данные героя
    /// </summary>
    public void SaveHeroData(HeroData heroData)
    {
        Save(heroData, HeroDataKey);
    }

    /// <summary>
    /// Проверяет существование данных героя
    /// </summary>
    public bool HasHeroData()
    {
        return HasData<HeroData>(HeroDataKey);
    }
} 