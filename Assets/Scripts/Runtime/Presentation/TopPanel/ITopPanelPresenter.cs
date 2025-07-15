using System;
using UnityEngine;

namespace Game.Runtime.Presentation.TopPanel
{
    public interface ITopPanelPresenter : IDisposable
    {
        public ulong SoftCurrencyCount { get; }
        Sprite SoftCurrencySprite { get; }
        public event Action OnSoftCurrencyChanged;
    }
}