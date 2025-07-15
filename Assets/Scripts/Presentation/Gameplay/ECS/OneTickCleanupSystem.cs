using Unity.Burst;
using Unity.Entities;
using Unity.Collections;

/// <summary>
/// ECS система очистки однотиковых компонентов
/// </summary>
[BurstCompile]
[UpdateInGroup(typeof(PresentationSystemGroup))]
public partial struct OneTickCleanupSystem : ISystem
{
    /// <summary>
    /// Вызывается при обновлении системы
    /// </summary>
    /// <param name="state">Состояние системы</param>
    public void OnUpdate(ref SystemState state)
    {        
        var query = SystemAPI.QueryBuilder().WithAll<OneTickEvent>().Build();
        var entities = query.ToEntityArray(Allocator.Temp);
        
        foreach (var entity in entities)
        {        
            var oneTickEvent = state.EntityManager.GetComponentData<OneTickEvent>(entity);
            
            if (oneTickEvent.oneTickMode == OneTickMode.OnCurrentEntity)
            {                
                var typeId = oneTickEvent.typeId;
                var type = TypeManager.GetType(typeId);
                state.EntityManager.RemoveComponent(entity, type);
                state.EntityManager.RemoveComponent<OneTickEvent>(entity);
            }
            else
            {
                state.EntityManager.DestroyEntity(entity);
            }
        }
        
        entities.Dispose();
    }
} 