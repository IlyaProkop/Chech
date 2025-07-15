using Unity.Entities;

/// <summary>
/// ECS компонент события улучшения
/// </summary>
public struct UpgradeEvent : IOneTickComponent
{
    /// <summary>
    /// Целевая сущность для улучшения
    /// </summary>
    public Entity target { get; set; }
}
