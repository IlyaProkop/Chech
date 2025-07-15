using Unity.Entities;

/// <summary>
/// ECS компонент данных героя
/// </summary>
public struct HeroComponent : IComponentData
{
    /// <summary>
    /// Уровень героя
    /// </summary>
    public int Level;
    
    /// <summary>
    /// Сила героя
    /// </summary>
    public int Strength;
    
    /// <summary>
    /// Здоровье героя
    /// </summary>
    public int Health;
    
    /// <summary>
    /// Флаг необходимости обновления (1 - обновить, 0 - не обновлять)
    /// </summary>
    public byte IsDirty;
} 