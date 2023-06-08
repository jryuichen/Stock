using Script.BaseClass;
using Script.SceneState;
using Script.SceneState.Base;
using Script.SceneState.HomePage;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Controller
{
    public class SceneStateController
    {
        #region Const StatesName
        private const string InitSplashState = "InitSplashState";

        private const string HomeState = "HomeState";
        private const string NewsState = "NewsState";
        private const string ProfileState = "ProfileState";
        private const string PersonalState = "PersonalState";
        private const string RecommendState = "RecommendState";
        private const string SubscribedState = "SubscribedState";

        private const string SignInState = "SignInState";
        private const string SignUpState = "SignUpState";

        #endregion

        public bool IsHomeState => CurrentState.GetType()== typeof(HomeState)&& CanChangeState;
        public bool IsNewsState => CurrentState.GetType() == typeof(NewsState)&& CanChangeState;
        public bool IsPersonalState => CurrentState.GetType() == typeof(PersonalState)&& CanChangeState;
        public bool IsRecommendState => CurrentState.GetType() == typeof(RecommendState)&& CanChangeState;
        public bool IsSubscribedState => CurrentState.GetType() == typeof(SubscribedState)&& CanChangeState;
        public bool CanChangeState => NextState == null;


        // Constructor
        private SceneStateController()
        {
            ToInitSplashState();
        }

        // Data
        private ISceneState CurrentState { get; set; }
        private ISceneState NextState { get; set; }
        private bool IsRunBegin => NextState == null && CurrentState.IsRunBegin;
        public bool NextIsLoggedState => NextState is Logged|| NextState ==null && CurrentState is Logged;

        // Interface
        private async UniTask SetState(ISceneState state, string loadSceneName = null)
        {
            Debug.Log("SetState:" + state); ;
            NextState = state;
            CurrentState?.StateEnd();
            // 載入場景
            if (loadSceneName != null)
                await LoadScene(loadSceneName);

            // 設定
            CurrentState = NextState;
            CurrentState.StateBegin();
            NextState = null;
        }

        public void StateUpdate()
        {
            if (CurrentState == null || !IsRunBegin) return;
            CurrentState?.StateUpdate();
        }

        // Methods
        private async UniTask LoadScene(string loadSceneName)
        {
            if (string.IsNullOrEmpty(loadSceneName))
                return;
            await SceneManager.LoadSceneAsync(loadSceneName);
        }

        private string GetStateSceneName(string stateName) => stateName.Replace("State","");

        // Change State Interface
        private async UniTask ToInitSplashState() =>await SetState(new InitSplashState(this), string.Empty);//不能前往初始化畫面

        public async UniTask ToHomeState() => await SetState(new HomeState(this), GetStateSceneName(HomeState));
        public async UniTask ToNewsState() =>await SetState(new NewsState(this), GetStateSceneName(NewsState));
       public async UniTask ToPersonalState() =>await SetState(new PersonalState(this), GetStateSceneName(PersonalState));
        public async UniTask ToRecommendState() =>await SetState(new RecommendState(this), GetStateSceneName(RecommendState));
        public async UniTask ToSubscribedState() =>await SetState(new SubscribedState(this), GetStateSceneName(SubscribedState));


        // Home Page
        public async UniTask ToProfileState() => await SetState(new ProfileState(this), GetStateSceneName(ProfileState));
        public async UniTask ToSignInState() => await SetState(new SignInState(this), GetStateSceneName(SignInState));
        public async UniTask ToSignUpState() => await SetState(new SignUpState(this), GetStateSceneName(SignUpState));
        // Factory
        public static SceneStateController CreateInitState()
        {
            return new SceneStateController();
        }
    }
}