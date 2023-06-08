using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.SignIn
{
    public class SignInViewModel : ViewModelBase
    {
        // Checkers
        private bool CanClickSignInButton => true;
        private bool CanClickSignUpButton => true;

        // Commands
        public Command SignInCommand { get; }
        public Command SignUpCommand { get; }

        private SignInViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
            SignInCommand = new Command(() => CanClickSignInButton, OnSignInClicked);
            SignUpCommand = new Command(() => CanClickSignUpButton, OnSignUpClicked);
        }

        private void OnSignUpClicked()
        {
            SceneStateController.ToSignUpState();
            BackController.OnBackReceiverAsync(SceneStateController.ToSignInState);
        }

        // Methods
        private void OnSignInClicked()
        {
            BackController.ExecuteBackCommand();
        }

        // Factories
        public static SignInViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}