using System;
using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.CoreCanvas.HomePage
{
    public class ProfileViewModel : ViewModelBase
    {
        private const string General = "成為推廣員";
        private const string Professional = "解除推廣員";
        private const string DuringUpgrade = "推廣員申請中";

        // Getters
        public string StrCurrentUserState => AccountProviderController.IsGeneralUser ? General : AccountProviderController.IsProfessionUser ? Professional : DuringUpgrade;
        // Checks
        private bool CanClickButtonInformation => true;
        private bool CanClickButtonSecurity => true;
        private bool CanClickButtonPreference => true;
        private bool CanClickButtonUser => true;

        // Commands
        public Command ShowInformationCommand { get; }
        public Command ShowSecurityCommand { get; }
        public Command ShowPreferenceCommand { get; }
        public Command ShowUserCommand { get; }
        // Constructors
        private ProfileViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
            ShowInformationCommand = new Command(()=>CanClickButtonInformation, HandleShowInformationCommand);
            ShowSecurityCommand = new Command(()=>CanClickButtonSecurity, HandleShowSecurityCommand);
            ShowPreferenceCommand = new Command(()=>CanClickButtonPreference, HandleShowPreferenceCommand);
            ShowUserCommand = new Command(()=>CanClickButtonUser, HandleShowUserCommand);
        }

        private void HandleShowUserCommand()
        {

        }

        private void HandleShowPreferenceCommand()
        {
        }

        private void HandleShowSecurityCommand()
        {
        }

        private void HandleShowInformationCommand()
        {

        }

        // Factories
        public static ProfileViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}