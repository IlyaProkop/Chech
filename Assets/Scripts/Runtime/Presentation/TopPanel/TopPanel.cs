using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Presentation.TopPanel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Presentation.TopBar
{
    public class TopPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI _softCurrencyText;
        [SerializeField] private Image _softCurrencyImage;
        
        private ITopPanelPresenter _presenter;

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }

        public void SetPresenter(ITopPanelPresenter presenter)
        {
            _presenter = presenter;
            _presenter.OnSoftCurrencyChanged += OnUpdateSoftCurrency;

            _softCurrencyImage.sprite = _presenter.SoftCurrencySprite;

            UpdateSoftCurrency();
        }

        private void OnUpdateSoftCurrency()
        {
            UpdateSoftCurrency();
        }

        private void UpdateSoftCurrency()
        {
            _softCurrencyText.text = _presenter.SoftCurrencyCount.ToString();
        }
    }
}
