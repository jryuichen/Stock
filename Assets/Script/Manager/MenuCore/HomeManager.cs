using System;
using Script.BaseClass;
using Script.View.Core;
using Script.ViewModel.CoreCanvas;

namespace Script.Manager.MenuCore
{
    public class HomeManager : GameSystem
    {
        private HomeView HomeView => HomeView.Instance;
        private HomeViewModel HomeViewModel { get; }

        private HomeManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            HomeViewModel = HomeViewModel.Create(stockMainManager);
        }

        // Overrides
        public override void Initialize()
        {
            HomeView.btnHomePage.onClick.AddListener(HandleHomePageClicked);
        }

        public override void Release()
        {
            base.Release();
            HomeView.btnHomePage.onClick.RemoveAllListeners();
        }

        private void HandleHomePageClicked()
        {
            HomeViewModel.HomePageCommand.CheckExecute();
        }
        // Factories
        public static HomeManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}