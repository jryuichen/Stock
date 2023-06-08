using System;
using Script.BaseClass;
using Script.View.SignIn;
using Script.ViewModel.SignIn;

namespace Script.Manager.SignIn
{
    public class SignInManager : GameSystem
    {
        private SignInView SignInView => SignInView.Instance;
        private SignInViewModel SignInViewModel { get; }

        private SignInManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            SignInViewModel = SignInViewModel.Create(stockMainManager);
        }

        public override void Initialize()
        {
            SignInView.SignInButton.onClick.AddListener(HandleSignInButtonOnClick);
            SignInView.SignUpButton.onClick.AddListener(HandleSignUpButtonOnClick);
        }

        public override void Release()
        {
            base.Release();
            SignInView.SignInButton.onClick.RemoveAllListeners();
            SignInView.SignUpButton.onClick.RemoveAllListeners();
        }

        // Methods
        private void HandleSignUpButtonOnClick()
        {
            SignInViewModel.SignUpCommand.CheckExecute();
        }

        private void HandleSignInButtonOnClick()
        {
            SignInViewModel.SignInCommand.CheckExecute();
        }
        // Factories
        public static SignInManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}