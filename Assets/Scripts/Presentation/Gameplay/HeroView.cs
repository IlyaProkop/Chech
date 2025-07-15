using UnityEngine;
using UnityEngine.UI;
using VContainer;

/// <summary>
/// View компонент для отображения героя
/// </summary>
public class HeroView : MonoBehaviour, IHeroView
{
    [SerializeField] private Text levelText;
    [SerializeField] private Text strengthText;
    [SerializeField] private Text healthText;
    [SerializeField] private Button upgradeButton;

    /// <summary>
    /// Событие нажатия кнопки улучшения
    /// </summary>
    public event System.Action OnUpgradeClicked;

    private bool isVisible = true;

    /// <summary>
    /// Инициализирует View
    /// </summary>
    [Inject]
    public void Initialize()
    {
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
    }

    /// <summary>
    /// Устанавливает статистику героя
    /// </summary>
    /// <param name="level">Уровень героя</param>
    /// <param name="strength">Сила героя</param>
    /// <param name="health">Здоровье героя</param>
    public void SetHeroStats(int level, int strength, int health)
    {
        if (levelText != null)
            levelText.text = $"Level: {level}";
        
        if (strengthText != null)
            strengthText.text = $"Strength: {strength}";
        
        if (healthText != null)
            healthText.text = $"Health: {health}";
    }

    /// <summary>
    /// Открывает View
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        isVisible = true;
    }

    /// <summary>
    /// Скрывает View
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
        isVisible = false;
    }

    /// <summary>
    /// Обрабатывает нажатие кнопки улучшения
    /// </summary>
    private void OnUpgradeButtonClicked()
    {
        OnUpgradeClicked?.Invoke();
    }

    /// <summary>
    /// Освобождает ресурсы при уничтожении объекта
    /// </summary>
    private void OnDestroy()
    {
        if (upgradeButton != null)
            upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
    }
} 