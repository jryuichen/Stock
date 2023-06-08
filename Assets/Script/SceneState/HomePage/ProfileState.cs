using Script.BaseClass;
using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore;
using Script.Manager.MenuCore.HomePage;
using Script.SceneState.Base;

namespace Script.SceneState.HomePage
{
    public class ProfileState : ISceneState
    {
        private ProfileManager ProfileManager { get; }
        public ProfileState(SceneStateController controller) : base(controller)
        {
            ProfileManager = ProfileManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            ProfileManager.Initialize();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            ProfileManager.Release();
        }

        public override string ToString() => $"StateName={nameof(ProfileState)}";

    }
}
