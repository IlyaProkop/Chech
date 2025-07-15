using System;
using Game.Runtime.Application.Resources;
using Game.Runtime.Domain.Common;
using Game.Runtime.Domain.PlayerResources;
using Game.Runtime.Infrastructure.Configs;
using Game.Runtime.Presentation.TopPanel;
using UnityEngine;
using VContainer;

namespace Game.Runtime.Application.Game
{
    public class TopPanelPresenter : ITopPanelPresenter
    {
        private readonly PlayerResourcesController _playerResourcesController;

        public ulong SoftCurrencyCount =>
            _playerResourcesController.PlayerResources.GetCount(Constants.Resources.SoftCurrency);

        public Sprite SoftCurrencySprite { get; }
        public event Action OnSoftCurrencyChanged;

        [Preserve]
        public TopPanelPresenter(PlayerResourcesController playerResourcesController, IConfigsService configsService,
            ISpritesConfigService spritesConfigService)
        {
            _playerResourcesController = playerResourcesController;

            SoftCurrencySprite = spritesConfigService.GetSprite(configsService.Get<ResourcesConfigs>()
                .GetResourceConfig(Constants.Resources.SoftCurrency).IconName);

            _playerResourcesController.PlayerResources.ResourceCountAdded += OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved += OnResourceCountChanged;
        }

        private void OnResourceCountChanged(string resourceId, ulong changedCount, ulong totalCount)
        {
            if (resourceId == Constants.Resources.SoftCurrency)
            {
                OnSoftCurrencyChanged?.Invoke();
            }
        }

        public void Dispose()
        {
            _playerResourcesController.PlayerResources.ResourceCountAdded -= OnResourceCountChanged;
            _playerResourcesController.PlayerResources.ResourceCountRemoved -= OnResourceCountChanged;
        }
    }
}