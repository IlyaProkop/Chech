using UniRx;

/// <summary>
/// Модель настроек качества графики
/// </summary>
public class GraphicsQuality
{
    /// <summary>
    /// Текущие настройки качества графики
    /// </summary>
    public ReactiveProperty<GraphicsQualityLevel> CurrentSettings { get; set; } = new ReactiveProperty<GraphicsQualityLevel>(GraphicsQualityLevel.Medium);
    
    /// <summary>
    /// Доступные уровни качества графики
    /// </summary>
    public enum GraphicsQualityLevel
    {
        Low,
        Medium,
        High,
        Ultra
    }
} 