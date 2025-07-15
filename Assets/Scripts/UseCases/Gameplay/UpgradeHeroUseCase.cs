using Cysharp.Threading.Tasks;
using MessagePipe;
using UniRx;
using Unity.Entities;
using UnityEngine;

/// <summary>
/// UseCase для улучшения героя
/// </summary>
public class UpgradeHeroUseCase
{
    private readonly HeroData heroData;
    private readonly IPublisher<UpgradeHeroCommand> publisher;

    /// <summary>
    /// Конструктор UseCase
    /// </summary>
    /// <param name="heroData">Данные героя</param>
    /// <param name="publisher">Публикатор сообщений</param>
    public UpgradeHeroUseCase(HeroData heroData, IPublisher<UpgradeHeroCommand> publisher)
    {
        this.heroData = heroData;
        this.publisher = publisher;
    }

    /// <summary>
    /// Выполняет улучшение героя
    /// </summary>
    /// <param name="heroId">Идентификатор героя</param>
    /// <returns>Задача выполнения</returns>
    public async UniTask ExecuteAsync(Entity entity)
    {
        var message = new UpgradeHeroCommand
        {
            Entity = entity
        };

        publisher.Publish(message);

        await UniTask.CompletedTask;
    }
}

public class UpgradeHeroMessageHandler : IMessageHandler<UpgradeHeroCommand>
{
    private readonly EntityManager entityManager;

    public UpgradeHeroMessageHandler(EntityManager entityManager)
    {
        this.entityManager = entityManager;
    }

    public void Handle(UpgradeHeroCommand message)
    {     
        if (entityManager.Exists(message.Entity))
        {   
            entityManager.SetOneTickComponent<UpgradeEvent>(message.Entity, OneTickMode.OnCurrentEntity);
        }
    }
}