using Unity.Entities;
/// <summary>
/// DTO сообщение для улучшения героя
/// </summary>
public struct UpgradeHeroCommand
{
    public Entity Entity { get; set; }
} 