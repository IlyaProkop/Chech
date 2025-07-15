/// <summary>
/// Интерфейс для Hero View игрового процесса
/// </summary>
public interface IHeroView : IView
{
    /// <summary>
    /// Устанавливает статистику героя
    /// </summary>
    /// <param name="level">Уровень героя</param>
    /// <param name="strength">Сила героя</param>
    /// <param name="health">Здоровье героя</param>
    void SetHeroStats(int level, int strength, int health);
    
    /// <summary>
    /// Событие нажатия кнопки улучшения
    /// </summary>
    event System.Action OnUpgradeClicked;
} 