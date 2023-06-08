using System;
using Modules.Core.Infrastructure;
using Script.Controller;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.CoreCanvas
{
    public class CoreCanvasViewModel : ViewModelBase
    {

        // Checkers
        private bool CanClickHome => !SceneStateController.IsHomeState;
        private bool CanClickRecommend => !SceneStateController.IsRecommendState;
        private bool CanClickSubscribed => !SceneStateController.IsSubscribedState;
        private bool CanClickNews => !SceneStateController.IsNewsState;
        private bool CanClickPersonal => !SceneStateController.IsPersonalState;

        // Commands
        public Command HomeCommand { get; }
        public Command RecommendCommand { get; }
        public Command SubscribedCommand { get; }
        public Command NewsCommand { get; }
        public Command PersonalCommand { get; }

        private CoreCanvasViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
            HomeCommand = new Command(() => CanClickHome, OnHomeClicked);
            RecommendCommand = new Command(() => CanClickRecommend, OnRecommendClicked);
            SubscribedCommand = new Command(() => CanClickSubscribed, OnSubscribedClicked);
            NewsCommand = new Command(() => CanClickNews, OnNewsClicked);
            PersonalCommand = new Command(() => CanClickPersonal, OnPersonalClicked);
        }

        private void OnPersonalClicked()
        {
            SceneStateController.ToPersonalState();
        }

        private void OnNewsClicked()
        {
            SceneStateController.ToNewsState();
        }

        private void OnSubscribedClicked()
        {
            SceneStateController.ToSubscribedState();
        }

        private void OnRecommendClicked()
        {
            SceneStateController.ToRecommendState();
        }

        private void OnHomeClicked()
        {
            SceneStateController.ToHomeState();
        }

        // Factories
        public static CoreCanvasViewModel Create(StockMainManager stockMainManager)
        {
            return new CoreCanvasViewModel(stockMainManager);
        }
    }
}