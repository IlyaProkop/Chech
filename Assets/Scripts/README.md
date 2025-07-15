# Архитектура проекта SoftTZ

## Обзор

Проект построен на основе **чистой архитектуры (Clean Architecture)** с разделением на ограниченные контексты **Application** и **Gameplay**.

## Структура слоев

### 1. ContractsInterfaces
Содержит интерфейсы для соблюдения принципов DI и разделения ответственности.

```
ContractsInterfaces/
├── Presentation/
│   ├── IView.cs                    # Базовый интерфейс для всех View
│   └── Gameplay/
│       └── IHeroView.cs            # Интерфейс для Hero View
├── Application/
│   └── IConfigsService.cs          # Интерфейс сервиса конфигураций
└── Gameplay/
    └── IHeroRepository.cs          # Интерфейс репозитория героя
```

### 2. Domain
Содержит модели данных и DTO сообщения без бизнес-логики.

```
Domain/
├── Application/
│   ├── Models/
│   │   └── GraphicsQuality.cs      # Модель настроек графики
│   └── MessagesDTO/
│       └── ChangeGraphicsQualityMessage.cs
└── Gameplay/
    ├── Models/
    │   └── HeroData.cs             # Модель данных героя
    └── MessagesDTO/
        └── UpgradeHeroMessage.cs   # DTO для улучшения героя
```

### 3. UseCases
Содержит высокоуровневую бизнес-логику и сценарии использования.

```
UseCases/
├── Application/
│   └── ChangeGraphicsQualityUseCase.cs
└── Gameplay/
    └── UpgradeHeroUseCase.cs
```

### 4. Presentation
Содержит Presenters и Views, а также ECS компоненты для высоконагруженных частей.

```
Presentation/
├── Application/                    # Presenters и Views уровня приложения
└── Gameplay/
    ├── HeroPresenter.cs            # Presenter для героя
    ├── HeroView.cs                 # View для героя
    └── ECS/                        # ECS компоненты
        ├── HeroComponent.cs
        ├── UpgradeEvent.cs
        ├── InitHeroSystem.cs
        ├── UpgradeHeroSystem.cs
        ├── OneTickCleanupSystem.cs
        └── EntityManagerOneTickExtensions.cs
```

### 5. Infrastructure
Содержит реализацию взаимодействия с внешними сервисами.

```
Infrastructure/
└── Repositories/
    └── HeroRepository.cs           # Реализация репозитория героя
```

### 6. Installers
Содержит конфигурацию DI контейнера.

```
Installers/
└── GameplayInstaller.cs            # Инсталлер для игрового процесса
```

### 7. Tests
Содержит unit и интеграционные тесты.

```
Tests/
└── Unit/
    └── UpgradeHeroUseCaseTests.cs  # Тесты для UseCase
```

## Принципы архитектуры

### 1. Разделение ответственности
- **Domain**: Только модели данных и DTO
- **UseCases**: Бизнес-логика и сценарии использования
- **Presentation**: Отображение и взаимодействие с пользователем
- **Infrastructure**: Внешние зависимости и хранение данных

### 2. Dependency Injection
- Использование VContainer для управления зависимостями
- Все зависимости инжектируются через конструктор
- Интерфейсы для абстракции

### 3. Реактивное программирование
- Использование UniRx для реактивных свойств
- Подписки на изменения данных
- Правильное освобождение ресурсов через Dispose

### 4. Асинхронность
- Использование UniTask для асинхронных операций
- Отсутствие блокирующих операций

### 5. Коммуникация между слоями
- MessagePipe для передачи сообщений между слоями
- DTO для передачи данных
- Слабая связанность между компонентами

## Требования к коду

### 1. Документация
- XML-документация для всех публичных членов
- Комментарии объясняют цель, а не реализацию

### 2. Тестирование
- Unit тесты для UseCases (покрытие >70%)
- Mock объекты для изоляции тестируемой логики
- NUnit для unit тестов

### 3. Стиль кода
- PascalCase для классов, интерфейсов, методов
- camelCase для переменных и параметров
- Префикс I для интерфейсов
- Следование принципам SOLID

### 4. Логирование
- Debug.Log только для отладки
- Serilog или PlayFab Events для релиза

## Примеры использования

### Создание нового UseCase
```csharp
/// <summary>
/// UseCase для выполнения действия
/// </summary>
public class ExampleUseCase
{
    private readonly IExampleRepository repository;
    private readonly IPublisher<ExampleMessage> publisher;

    [Inject]
    public ExampleUseCase(IExampleRepository repository, IPublisher<ExampleMessage> publisher)
    {
        this.repository = repository;
        this.publisher = publisher;
    }

    public async UniTask ExecuteAsync(ExampleData data)
    {
        // Бизнес-логика
        var result = await repository.ProcessAsync(data);
        
        // Публикация сообщения
        publisher.Publish(new ExampleMessage { Data = result });
    }
}
```

### Создание нового Presenter
```csharp
/// <summary>
/// Presenter для управления отображением
/// </summary>
public class ExamplePresenter : IDisposable
{
    private readonly IExampleView view;
    private readonly ExampleData data;
    private readonly CompositeDisposable disposables = new();

    [Inject]
    public ExamplePresenter(IExampleView view, ExampleData data)
    {
        this.view = view;
        this.data = data;
        Initialize();
    }

    private void Initialize()
    {
        data.SomeProperty
            .Subscribe(value => view.UpdateDisplay(value))
            .AddTo(disposables);
    }

    public void Dispose()
    {
        disposables?.Dispose();
    }
}
```

## Заключение

Данная архитектура обеспечивает:
- Высокую тестируемость кода
- Легкость поддержки и расширения
- Четкое разделение ответственности
- Слабая связанность между компонентами
- Соответствие принципам SOLID 