using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Runtime.Infrastructure.Panels
{
    public class PanelsService : MonoBehaviour, IPanelsService
    {
        public List<PanelBase> _scenePanels;

        private void Awake()
        {
            foreach (var panel in _scenePanels)
            {
                panel.Hide();
            }
        }

        private TPanel GetPanel<TPanel>() where TPanel : PanelBase
        {
            return _scenePanels.Single(popup => popup.GetType() == typeof(TPanel)) as TPanel;
        }
    
        public TPanel Open<TPanel>() where TPanel : PanelBase
        {
            var panel = GetPanel<TPanel>();
            panel.Show();
            return panel;
        }

        public void Close<TPanel>() where TPanel : PanelBase
        {
            var panel = GetPanel<TPanel>();
            panel.Hide();
        }

        public bool IsOpened<TPanel>(out TPanel panel) where TPanel : PanelBase
        {
            panel = GetPanel<TPanel>();
            if (panel.IsActive)
            {
                return true;
            }

            return false;
        }
    }
}
