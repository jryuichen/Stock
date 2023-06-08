using System;
using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.SignIn
{
    public class SignUpViewModel : ViewModelBase
    {
        // Data
        public bool IsShowPanelVerify { get;private set; }
        // Checkers
        private bool CanClickSignUpButton => !IsShowPanelVerify;
        private bool CanClickSendPhoneNumber => IsShowPanelVerify;
        private bool CanClickSendVerificationCodeButton => IsShowPanelVerify;

        // Commands
        public Command SignUpCommand { get; }
        public Command SendPhoneNumberCommand { get; }
        public Command SendVerificationCodeCommand { get; }

        // Constructor
        private SignUpViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
            SignUpCommand = new Command(() => CanClickSignUpButton, OnSignUpClicked);
            SendPhoneNumberCommand = new Command(() => CanClickSendPhoneNumber, OnSendPhoneNumberClicked);
            SendVerificationCodeCommand = new Command(() => CanClickSendVerificationCodeButton, OnSendVerificationCodeClicked);
        }

        // Methods
        private void OnSendVerificationCodeClicked()
        {

        }

        private void OnSendPhoneNumberClicked()
        {
        }

        private void OnSignUpClicked()
        {
            IsShowPanelVerify = true;
            BackController.OnBackReceiver(() =>
            {
                IsShowPanelVerify = false;
                NotifyViewModelUpdated();
            });
            NotifyViewModelUpdated();
        }

        // Factories
        public static SignUpViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}