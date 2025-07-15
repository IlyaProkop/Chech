using Unity.Entities;
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