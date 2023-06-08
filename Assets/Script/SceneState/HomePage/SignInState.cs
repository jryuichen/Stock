using Script.Controller;
using Script.Manager;
using Script.Manager.SignIn;
using Script.SceneState.Base;

namespace Script.SceneState
{
    public class SignInState : ISceneState
    {
        // Data
        private SignInManager SignInManager { get; }

        // Constructor
        public SignInState(SceneStateController controller) : base(controller)
        {
            SignInManager = SignInManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            SignInManager.Initialize();
        }

        public override void StateUpdate()
        {
            SignInManager.Update();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            SignInManager.Release();
        }

        public override string ToString() => $"StateName={nameof(SignInState)}";
    }
}