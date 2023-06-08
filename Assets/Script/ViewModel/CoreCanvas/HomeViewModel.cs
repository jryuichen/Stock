using Modules.Core.Infrastructure;
using Script.Controller;
using Script.Manager;
using Script.Modules.Infrastructure;
using UniRx.Async;
using UnityEngine;

namespace Script.ViewModel.CoreCanvas
{
    public class HomeViewModel : ViewModelBase
    {
        // Checks
        private bool CanHomePage => SceneStateController.CanChangeState;

        // Commands
        public Command HomePageCommand { get; }

        private HomeViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
            HomePageCommand = new Command(() => CanHomePage, HandleHomePageClicked);
        }

        private async void HandleHomePageClicked()
        {
            if (AccountProviderController.IsSignedOut)
            {
                await SceneStateController.ToSignInState();
            }
            else
            {
                await SceneStateController.ToProfileState();
            }

            BackController.OnBackReceiverAsync(Test);
        }

        private async UniTask Test()
        {
            await UniTask.Delay(1000);
            Debug.Log("waited 1 second");
            await SceneStateController.ToHomeState();
        }

        // Factories
        public static HomeViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}