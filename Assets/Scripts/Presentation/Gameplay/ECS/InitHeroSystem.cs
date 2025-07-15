using Unity.Burst;
using Unity.Entities;

/// <summary>
/// ECS система инициализации героя
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct InitHeroSystem : ISystem
{
    /// <summary>
    /// Вызывается при создании системы
    /// </summary>
    /// <param name="state">Состояние системы</param>
    public void OnCreate(ref SystemState state)
    {
        var entityManager = state.EntityManager;
        var query = entityManager.CreateEntityQuery(typeof(HeroComponent));
        
        if (query.IsEmpty)
        {
            var heroEntity = entityManager.CreateEntity(typeof(HeroComponent));
            entityManager.SetComponentData(heroEntity, new HeroComponent 
            { 
                Level = 1, 
                Strength = 10, 
                Health = 100, 
                IsDirty = 1 
            });
        }
    }

    /// <summary>
    /// Вызывается при обновлении системы
    /// </summary>
    /// <param name="state">Состояние системы</param>
    public void OnUpdate(ref SystemState state) 
    {
        // Система инициализации выполняется только один раз
    }
} 