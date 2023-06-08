using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore.HomePage;
using Script.SceneState.Base;

namespace Script.SceneState.HomePage
{
    public class SignUpState : ISceneState
    {
        // Data
        private SignUpManager SignUpManager { get; }

        public SignUpState(SceneStateController controller) : base(controller)
        {
            SignUpManager = SignUpManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            SignUpManager.Initialize();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            SignUpManager.Release();
        }

        public override string ToString() => $"StateName={nameof(SignUpState)}";
    }
}