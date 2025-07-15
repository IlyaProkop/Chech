using System;
using MessagePipe;
using UniRx;
using VContainer;
using UnityEngine;
using Unity.Entities;

/// <summary>
/// Presenter для управления отображением героя
/// </summary>
public class HeroPresenter : IDisposable
{
    private readonly IHeroView view;
    private readonly HeroData heroData;
    private readonly IPublisher<UpgradeHeroMessage> publisher;
    private readonly Entity heroEntity;

    private readonly CompositeDisposable disposables = new CompositeDisposable();

    /// <summary>
    /// Конструктор HeroPresenter
    /// </summary>
    /// <param name="view">Интерфейс представления героя</param>
    /// <param name="heroData">Данные героя</param>
    /// <param name="publisher">Публикатор сообщений</param>
    [Inject]
    public HeroPresenter(IHeroView view, HeroData heroData, IPublisher<UpgradeHeroMessage> publisher)
    {
        Debug.Log("HeroPresenter constructor");
        this.view = view;
        this.heroData = heroData;
        this.publisher = publisher;
        
        Initialize();
    }

    /// <summary>
    /// Инициализирует presenter
    /// </summary>
    private void Initialize()
    {
        // Подписка на изменения данных героя
        heroData.Level
            .CombineLatest(heroData.Strength, heroData.Health, (level, strength, health) => new { level, strength, health })
            .Subscribe(data => view.SetHeroStats(data.level, data.strength, data.health))
            .AddTo(disposables);

        // Подписка на событие нажатия кнопки улучшения
        view.OnUpgradeClicked += HandleUpgradeClicked;
        
        // Начальное обновление UI
        UpdateView();
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки улучшения
    /// </summary>
    private void HandleUpgradeClicked()
    {
        Debug.Log("HandleUpgradeClicked");
        var message = new UpgradeHeroMessage
        {
            Entity = heroEntity
        };
        
        publisher.Publish(message);
    }

    /// <summary>
    /// Обновляет представление
    /// </summary>
    private void UpdateView()
    {
        view.SetHeroStats(heroData.Level.Value, heroData.Strength.Value, heroData.Health.Value);
    }

    /// <summary>
    /// Освобождает ресурсы
    /// </summary>
    public void Dispose()
    {
        view.OnUpgradeClicked -= HandleUpgradeClicked;
        disposables?.Dispose();
    }
} 