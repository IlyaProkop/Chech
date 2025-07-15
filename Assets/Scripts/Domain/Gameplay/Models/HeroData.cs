using UniRx;

/// <summary>
/// Модель данных героя для игрового процесса
/// </summary>
public class HeroData
{
    /// <summary>
    /// Уровень героя
    /// </summary>
    public ReactiveProperty<int> Level { get; set; } = new ReactiveProperty<int>(1);
    
    /// <summary>
    /// Сила героя
    /// </summary>
    public ReactiveProperty<int> Strength { get; set; } = new ReactiveProperty<int>(10);
    
    /// <summary>
    /// Здоровье героя
    /// </summary>
    public ReactiveProperty<int> Health { get; set; } = new ReactiveProperty<int>(100);
    
    /// <summary>
    /// Флаг необходимости обновления UI
    /// </summary>
    public ReactiveProperty<bool> IsDirty { get; set; } = new ReactiveProperty<bool>(true);
} 