using Script.BaseClass;
using Script.Manager;

namespace Script.Controller
{
    public enum LoginState
    {
        SignedOut = 0,
        SignedIn = 1
    }

    public enum UserState
    {
        General = 0,
        Profession = 1,
        DuringUpgrade = 2
    }
    public class AccountProviderController : GameSystem
    {

        private LoginState CurrentLoginState = LoginState.SignedIn;
        private UserState  CurrentUserState = UserState.General;
        // Getter
        public bool IsSignedIn => CurrentLoginState == LoginState.SignedIn;
        public bool IsSignedOut => CurrentLoginState == LoginState.SignedOut;
        public bool IsGeneralUser => CurrentUserState == UserState.General;
        public bool IsProfessionUser => CurrentUserState == UserState.Profession;
        public bool IsDuringUpgrade => CurrentUserState == UserState.DuringUpgrade;

        private AccountProviderController(StockMainManager stockMainManager) : base(stockMainManager)
        {
        }

        // Factories
        public static AccountProviderController Create(StockMainManager stockMainManager)
        {
            return new AccountProviderController(stockMainManager);
        }

    }
}
