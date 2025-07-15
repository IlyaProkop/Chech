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

/// <summary>
/// Интерфейс для компонентов, которые должны быть удалены после одного тика
/// </summary>
public interface IOneTickComponent : IComponentData
{
    /// <summary>
    /// Целевая сущность
    /// </summary>
    public Entity target { get; set; }
} 