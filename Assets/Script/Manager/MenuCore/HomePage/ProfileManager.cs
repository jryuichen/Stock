using System;
using Script.BaseClass;
using Script.View.Core.HomePage;
using Script.ViewModel.CoreCanvas.HomePage;

namespace Script.Manager.MenuCore.HomePage
{
    public class ProfileManager : GameSystem
    {
        private ProfileView ProfileView => ProfileView.Instance;
        private ProfileViewModel ProfileViewModel { get; }

        private ProfileManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            ProfileViewModel = ProfileViewModel.Create(stockMainManager);

        }

        // Overrides
        public override void Initialize()
        {
            base.Initialize();
            ProfileViewModel.ViewModelUpdated += HandleViewModelUpdated;
            ProfileView.ButtonInformation.onClick.AddListener(HandleButtonInformationOnClick);
            ProfileView.ButtonSecurity.onClick.AddListener(HandleButtonSecurityOnClick);
            ProfileView.ButtonPreference.onClick.AddListener(HandleButtonPreferenceOnClick);
            ProfileView.ButtonUser.onClick.AddListener(HandleButtonUserOnClick);
            HandleViewModelUpdated();
        }

        public override void Release()
        {
            base.Release();
            ProfileViewModel.ViewModelUpdated -= HandleViewModelUpdated;
            ProfileView.ButtonInformation.onClick.RemoveAllListeners();
            ProfileView.ButtonSecurity.onClick.RemoveAllListeners();
            ProfileView.ButtonPreference.onClick.RemoveAllListeners();
            ProfileView.ButtonUser.onClick.RemoveAllListeners();
        }

        // Methods
        private void HandleViewModelUpdated()
        {
            ProfileView.TxtUserState.text = ProfileViewModel.StrCurrentUserState;
        }

        private void HandleButtonUserOnClick()
        {
            ProfileViewModel.ShowUserCommand.CheckExecute();
        }

        private void HandleButtonPreferenceOnClick()
        {
            ProfileViewModel.ShowPreferenceCommand.CheckExecute();
        }


        private void HandleButtonSecurityOnClick()
        {
            ProfileViewModel.ShowSecurityCommand.CheckExecute();
        }

        private void HandleButtonInformationOnClick()
        {
            ProfileViewModel.ShowInformationCommand.CheckExecute();
        }

        // Factories
        public static ProfileManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}