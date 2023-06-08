using Script.BaseClass;
using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore;
using Script.SceneState.Base;

namespace Script.SceneState
{
    public class NewsState : ISceneState, Logged
    {
        private NewsManager NewsManager { get; }
        public NewsState(SceneStateController controller) : base(controller)
        {
            NewsManager = NewsManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            NewsManager.Initialize();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            NewsManager.Release();
        }

        public override string ToString() => $"StateName={nameof(NewsState)}";

    }
}
