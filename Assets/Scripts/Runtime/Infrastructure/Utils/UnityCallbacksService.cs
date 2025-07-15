using System;
using UnityEngine;

namespace Game.Runtime.Infrastructure.Utils
{
    public class UnityCallbacksService : MonoBehaviour
    {
        public event Action<bool> OnApplicationPaused;
        public event Action<bool> OnApplicationFocusChanged;
        public event Action OnApplicationQuitted;

        private void OnApplicationPause(bool pauseStatus)
        {
            OnApplicationPaused?.Invoke(pauseStatus);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            OnApplicationFocusChanged?.Invoke(hasFocus);
        }

        private void OnApplicationQuit()
        {
            OnApplicationQuitted?.Invoke();
        }
    }
}
