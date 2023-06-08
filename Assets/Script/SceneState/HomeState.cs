using Script.BaseClass;
using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore;
using Script.SceneState.Base;

namespace Script.SceneState
{
    public class HomeState : ISceneState, Logged
    {
        // Data
        private HomeManager HomeManager { get; }
        // Constructors
        public HomeState(SceneStateController controller) : base(controller)
        {
            HomeManager = HomeManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            HomeManager.Initialize();
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            HomeManager.Release();
        }

        // Override
        public override string ToString()=>$"StateName={nameof(HomeState)}";

    }
}
