using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Collections.LowLevel.Unsafe;

/// <summary>
/// ECS система улучшения героя
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct UpgradeHeroSystem : ISystem
{
    /// <summary>
    /// Вызывается при обновлении системы
    /// </summary>
    /// <param name="state">Состояние системы</param>
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {        
        var entityManager = state.EntityManager;
        var query = SystemAPI.QueryBuilder().WithAll<UpgradeEvent>().Build();
        var events = query.ToComponentDataArray<UpgradeEvent>(Allocator.Temp);
        
        foreach (var upgrade in events)
        {
            var heroEntity = upgrade.target;
            
            // Проверяем валидность сущности
            if (heroEntity == Entity.Null)
            {                
                var ecb = new EntityCommandBuffer(Allocator.TempJob);
                var job = new UpgradeJob();
                state.Dependency = job.ScheduleParallel(state.Dependency);
                state.Dependency.Complete();
                ecb.Playback(state.EntityManager);
                ecb.Dispose();
                continue;
            }
            
            if (!entityManager.Exists(heroEntity))
                continue;
                
            if (!entityManager.HasComponent<HeroComponent>(heroEntity))
                continue;

            // Обновляем данные героя
            var hero = entityManager.GetComponentData<HeroComponent>(heroEntity);
            hero.Level += 1;
            hero.Strength += 5;
            hero.Health += 10;
            hero.IsDirty = 1;
            entityManager.SetComponentData(heroEntity, hero);            
        }
        
        events.Dispose();
    }

    /// <summary>
    /// Job для параллельного улучшения героев
    /// </summary>
    [BurstCompile]
    public partial struct UpgradeJob : IJobEntity
    {
        /// <summary>
        /// Выполняет улучшение героя
        /// </summary>
        /// <param name="entityInQueryIndex">Индекс сущности в запросе</param>
        /// <param name="entity">Сущность</param>
        /// <param name="hero">Компонент героя</param>
        /// <param name="upgrade">Событие улучшения</param>
        public void Execute([EntityIndexInQuery] int entityInQueryIndex, Entity entity, ref HeroComponent hero, in UpgradeEvent upgrade)
        {
            unsafe
            {
                HeroComponent* heroPtr = (HeroComponent*)UnsafeUtility.AddressOf(ref hero);
                heroPtr->Level += 1;
                heroPtr->Strength += 5;
                heroPtr->Health += 10;
                heroPtr->IsDirty = 1;
            }
        }
    }
} 