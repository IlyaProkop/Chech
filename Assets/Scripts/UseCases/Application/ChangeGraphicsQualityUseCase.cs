using Cysharp.Threading.Tasks;
using MessagePipe;
using UniRx;

/// <summary>
/// UseCase для изменения настроек графики
/// </summary>
public class ChangeGraphicsQualityUseCase
{
    private readonly GraphicsQuality graphicsQuality;
    private readonly IPublisher<ChangeGraphicsQualityMessage> publisher;

    /// <summary>
    /// Конструктор UseCase
    /// </summary>
    /// <param name="graphicsQuality">Модель настроек графики</param>
    /// <param name="publisher">Публикатор сообщений</param>
    public ChangeGraphicsQualityUseCase(GraphicsQuality graphicsQuality, IPublisher<ChangeGraphicsQualityMessage> publisher)
    {
        this.graphicsQuality = graphicsQuality;
        this.publisher = publisher;
    }

    /// <summary>
    /// Выполняет изменение настроек графики
    /// </summary>
    /// <param name="targetQuality">Целевое качество графики</param>
    /// <returns>Задача выполнения</returns>
    public async UniTask ExecuteAsync(GraphicsQuality.GraphicsQualityLevel targetQuality)
    {
        graphicsQuality.CurrentSettings.Value = targetQuality;
        
        var message = new ChangeGraphicsQualityMessage
        {
            Target = targetQuality
        };
        
        publisher.Publish(message);
        
        await UniTask.CompletedTask;
    }
} 