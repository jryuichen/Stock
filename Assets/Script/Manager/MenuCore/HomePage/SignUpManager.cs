using System;
using Script.BaseClass;
using Script.View.SignIn;
using Script.ViewModel.SignIn;

namespace Script.Manager.MenuCore.HomePage
{
    public class SignUpManager : GameSystem
    {
        private SignUpView SignUpView => SignUpView.Instance;
        private SignUpViewModel SignUpViewModel { get; }

        private SignUpManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            SignUpViewModel = SignUpViewModel.Create(stockMainManager);
        }

        // Overrides
        public override void Initialize()
        {
            SignUpViewModel.ViewModelUpdated += HandleViewModelUpdated;
            SignUpView.btnSignUp.onClick.AddListener(HandleSignUpButtonOnClick);
            SignUpView.btnSendPhoneNumber.onClick.AddListener(HandleSendPhoneNumberButtonOnClick);
            SignUpView.btnSendVerificationCode.onClick.AddListener(HandleSendVerificationCodeButtonOnClick);
            HandleViewModelUpdated();
        }

        public override void Release()
        {
            SignUpViewModel.ViewModelUpdated -= HandleViewModelUpdated;
            SignUpView.btnSignUp.onClick.RemoveAllListeners();
            SignUpView.btnSendPhoneNumber.onClick.RemoveAllListeners();
            SignUpView.btnSendVerificationCode.onClick.RemoveAllListeners();
        }

        // Methods
        private void HandleSendVerificationCodeButtonOnClick()
        {
            SignUpViewModel.SendVerificationCodeCommand.CheckExecute();
        }

        private void HandleSendPhoneNumberButtonOnClick()
        {
            SignUpViewModel.SendPhoneNumberCommand.CheckExecute();
        }

        private void HandleSignUpButtonOnClick()
        {
            SignUpViewModel.SignUpCommand.CheckExecute();
        }

        private void HandleViewModelUpdated()
        {
            SignUpView.objPanelVerify.SetActive(SignUpViewModel.IsShowPanelVerify);
        }

        // Factories
        public static SignUpManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}