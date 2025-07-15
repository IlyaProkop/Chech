using Cysharp.Threading.Tasks;
using MessagePipe;
using UniRx;
using Unity.Entities;

/// <summary>
/// UseCase для улучшения героя
/// </summary>
public class UpgradeHeroUseCase
{
    private readonly HeroData heroData;
    private readonly IPublisher<UpgradeHeroMessage> publisher;

    /// <summary>
    /// Конструктор UseCase
    /// </summary>
    /// <param name="heroData">Данные героя</param>
    /// <param name="publisher">Публикатор сообщений</param>
    public UpgradeHeroUseCase(HeroData heroData, IPublisher<UpgradeHeroMessage> publisher)
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
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
      entityManager.SetOneTickComponent<UpgradeEvent>(entity);
    
        
        var message = new UpgradeHeroMessage
        {
            Entity = entity
        };
        
        publisher.Publish(message);
        
        await UniTask.CompletedTask;
    }
} 