using Script.BaseClass;
using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore;
using Script.SceneState.Base;

namespace Script.SceneState
{
    public class SubscribedState : ISceneState, Logged
    {
        private SubscribedManager SubscribedManager { get; }

        public SubscribedState(SceneStateController controller) : base(controller)
        {
            SubscribedManager = SubscribedManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            SubscribedManager.Initialize();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            SubscribedManager.Release();
        }

        public override string ToString() => $"StateName={nameof(SubscribedState)}";
    }
}