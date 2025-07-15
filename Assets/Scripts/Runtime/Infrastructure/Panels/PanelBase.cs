using UnityEngine;

namespace Game.Runtime.Infrastructure.Panels
{
    public class PanelBase : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        
        public bool IsActive { get; private set; }

        public void Show()
        {
            _panel.gameObject.SetActive(true);
            IsActive = true;
        }

        public void Hide()
        {
            _panel.gameObject.SetActive(false);
            IsActive = false;
        }
    }
}