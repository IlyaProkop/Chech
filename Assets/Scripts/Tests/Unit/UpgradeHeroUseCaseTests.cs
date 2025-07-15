using NUnit.Framework;
using MessagePipe;
using UniRx;
using Cysharp.Threading.Tasks;
using Unity.Entities;

/// <summary>
/// Unit тесты для UpgradeHeroUseCase
/// </summary>
[TestFixture]
public class UpgradeHeroUseCaseTests
{
    private HeroData heroData;
    private MockPublisher<UpgradeHeroMessage> mockPublisher;
    private UpgradeHeroUseCase useCase;
    private Entity heroEntity;

    /// <summary>
    /// Настройка перед каждым тестом
    /// </summary>
    [SetUp]
    public void Setup()
    {
        heroData = new HeroData();
        mockPublisher = new MockPublisher<UpgradeHeroMessage>();
        useCase = new UpgradeHeroUseCase(heroData, mockPublisher);
        heroEntity = new Entity();
    }

    /// <summary>
    /// Тест: ExecuteAsync должен увеличить уровень героя на 1
    /// </summary>
    [Test]
    public async UniTask ExecuteAsync_ShouldIncreaseLevelByOne()
    {
        // Arrange
        var initialLevel = heroData.Level.Value;

        // Act
        await useCase.ExecuteAsync(heroEntity);

        // Assert
        Assert.AreEqual(initialLevel + 1, heroData.Level.Value);
    }

    /// <summary>
    /// Тест: ExecuteAsync должен увеличить силу героя на 5
    /// </summary>
    [Test]
    public async UniTask ExecuteAsync_ShouldIncreaseStrengthByFive()
    {
        // Arrange
        var initialStrength = heroData.Strength.Value;

        // Act
        await useCase.ExecuteAsync(heroEntity);

        // Assert
        Assert.AreEqual(initialStrength + 5, heroData.Strength.Value);
    }

    /// <summary>
    /// Тест: ExecuteAsync должен увеличить здоровье героя на 10
    /// </summary>
    [Test]
    public async UniTask ExecuteAsync_ShouldIncreaseHealthByTen()
    {
        // Arrange
        var initialHealth = heroData.Health.Value;

        // Act
        await useCase.ExecuteAsync(heroEntity);

        // Assert
        Assert.AreEqual(initialHealth + 10, heroData.Health.Value);
    }

    /// <summary>
    /// Тест: ExecuteAsync должен установить флаг IsDirty в true
    /// </summary>
    [Test]
    public async UniTask ExecuteAsync_ShouldSetIsDirtyToTrue()
    {
        // Arrange
        heroData.IsDirty.Value = false;

        // Act
        await useCase.ExecuteAsync(heroEntity);

        // Assert
        Assert.IsTrue(heroData.IsDirty.Value);
    }

    /// <summary>
    /// Тест: ExecuteAsync должен опубликовать сообщение с правильным HeroId
    /// </summary>
    [Test]
    public async UniTask ExecuteAsync_ShouldPublishMessageWithCorrectHeroId()
    {
        // Arrange

        // Act
        await useCase.ExecuteAsync(heroEntity);

        // Assert
        Assert.AreEqual(1, mockPublisher.PublishedMessages.Count);
        Assert.AreEqual(heroEntity, mockPublisher.PublishedMessages[0].Entity);
    }
}

/// <summary>
/// Mock реализация IPublisher для тестирования
/// </summary>
/// <typeparam name="T">Тип сообщения</typeparam>
public class MockPublisher<T> : IPublisher<T>
{
    /// <summary>
    /// Список опубликованных сообщений
    /// </summary>
    public System.Collections.Generic.List<T> PublishedMessages { get; } = new();

    /// <summary>
    /// Публикует сообщение
    /// </summary>
    /// <param name="message">Сообщение для публикации</param>
    public void Publish(T message)
    {
        PublishedMessages.Add(message);
    }
} 