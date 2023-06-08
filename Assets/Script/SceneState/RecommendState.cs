using Script.BaseClass;
using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore;
using Script.SceneState.Base;

namespace Script.SceneState
{
    public class RecommendState : ISceneState, Logged
    {
        private RecommendManager RecommendManager { get; }
        public RecommendState(SceneStateController controller) : base(controller)
        {
            RecommendManager = RecommendManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            RecommendManager.Initialize();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            RecommendManager.Release();
        }

        public override string ToString() => $"StateName={nameof(RecommendState)}";

    }
}
