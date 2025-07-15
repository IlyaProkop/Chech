/// <summary>
/// Интерфейс репозитория для работы с данными героя
/// </summary>
public interface IHeroRepository
{
    /// <summary>
    /// Получает данные героя
    /// </summary>
    /// <returns>Данные героя</returns>
    HeroData GetHeroData();
    
    /// <summary>
    /// Сохраняет данные героя
    /// </summary>
    /// <param name="heroData">Данные героя для сохранения</param>
    void SaveHeroData(HeroData heroData);
    
    /// <summary>
    /// Проверяет существование данных героя
    /// </summary>
    /// <returns>True если данные существуют, иначе False</returns>
    bool HasHeroData();
} 