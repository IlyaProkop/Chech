using Unity.Entities;
using System.Collections.Generic;

/// <summary>
/// Расширения для EntityManager для работы с однотиковыми компонентами
/// </summary>
public static class EntityManagerOneTickExtensions
{
    private static readonly Dictionary<System.Type, EntityArchetype> archetypeCache = new();

    /// <summary>
    /// Создаёт временную сущность с компонентом T и ссылкой на основную сущность
    /// </summary>
    /// <typeparam name="T">Тип компонента</typeparam>
    /// <param name="entityManager">Менеджер сущностей</param>
    /// <param name="target">Целевая сущность</param>
    /// <param name="mode">Режим однотикового компонента</param>
    /// <param name="component">Компонент для установки</param>
    /// <returns>Созданная или модифицированная сущность</returns>
    public static Entity SetOneTickComponent<T>(
        this EntityManager entityManager,
        Entity target,
        OneTickMode mode = OneTickMode.OnCurrentEntity,
        T component = default
        )
        where T : unmanaged, IOneTickComponent
    {
        if (mode == OneTickMode.OnCurrentEntity)
        {
            if (!entityManager.HasComponent<T>(target))
            {
                entityManager.AddComponentData(target, component);

                entityManager.AddComponentData(target, new OneTickEvent
                {
                    oneTickMode = mode,
                    typeId = TypeManager.GetTypeIndex<T>()
                });
            }
            return target;
        }
        else
        {
            var type = typeof(T);
            if (!archetypeCache.TryGetValue(type, out var archetype))
            {
                archetype = entityManager.CreateArchetype(typeof(T), typeof(OneTickEvent));
                archetypeCache[type] = archetype;
            }

            var oneTickEntity = entityManager.CreateEntity(archetype);
            entityManager.SetComponentData(oneTickEntity, component);
            entityManager.SetComponentData(oneTickEntity, new OneTickEvent { oneTickMode = mode });
            return oneTickEntity;
        }
    }
}

/// <summary>
/// Режим однотикового компонента
/// </summary>
public enum OneTickMode
{
    /// <summary>
    /// На текущей сущности
    /// </summary>
    OnCurrentEntity,
    
    /// <summary>
    /// На новой сущности
    /// </summary>
    OnNewEntity
}

/// <summary>
/// Событие однотикового компонента
/// </summary>
public struct OneTickEvent : IComponentData
{
    /// <summary>
    /// Режим однотикового компонента
    /// </summary>
    public OneTickMode oneTickMode;
    
    /// <summary>
    /// ID типа компонента
    /// </summary>
    public int typeId;
} 