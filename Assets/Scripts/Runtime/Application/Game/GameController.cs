using Game.Runtime.Application.Resources;
using Game.Runtime.Infrastructure.Factories;
using Game.Runtime.Infrastructure.Panels;
using Game.Runtime.Infrastructure.Repository;
using Game.Runtime.Presentation.TopBar;
using UnityEngine.Scripting;
using VContainer.Unity;

namespace Game.Runtime.Application.Game
{
    public class GameController : IInitializable
    {
        private readonly PlayerResourcesController playerResourcesController;
        private readonly ISavesController gameSaveController;
        private readonly IIocFactory iocFactory;
        private readonly IPanelsService panelsService;
        private readonly HeroPresenter heroPresenter;
        [Preserve]
        public GameController(PlayerResourcesController playerResourcesController, ISavesController gameSaveController,
            IIocFactory iocFactory, IPanelsService panelsService, HeroPresenter heroPresenter)
        {
            this.playerResourcesController = playerResourcesController;
            this.gameSaveController = gameSaveController;
            this.iocFactory = iocFactory;
            this.panelsService = panelsService;
            this.heroPresenter = heroPresenter;
        }

        void IInitializable.Initialize()
        {
            playerResourcesController.Initialize();

          //  var topBarPanel = panelsService.Open<TopPanel>();
         //   topBarPanel.SetPresenter(iocFactory.Create<TopPanelPresenter>());

            gameSaveController.SaveAllLocal();
        }
    }
}