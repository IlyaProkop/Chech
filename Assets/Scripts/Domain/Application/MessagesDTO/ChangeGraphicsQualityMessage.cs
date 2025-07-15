/// <summary>
/// DTO сообщение для изменения настроек графики игры
/// </summary>
public class ChangeGraphicsQualityMessage
{
    /// <summary>
    /// Целевые настройки графики
    /// </summary>
    public GraphicsQuality.GraphicsQualityLevel Target { get; set; }
} 