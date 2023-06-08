using Script.Controller;
using Script.Manager;
using Script.Manager.InitSplash;
using Script.SceneState.Base;
using UnityEngine;

namespace Script.SceneState
{
    public class InitSplashState : ISceneState
    {
        private InitSplashManager InitSplashManager { get; }

        public InitSplashState(SceneStateController controller) : base(controller)
        {
            InitSplashManager = InitSplashManager.Create(StockMainManager.Instance);
        }

        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            InitSplashManager.Initialize();
        }
        public override void StateUpdate()
        {
            if(Input.GetKeyDown(KeyCode.K))
                Controller.ToHomeState();
        }

        public override string ToString() => $"StateName={nameof(InitSplashState)}";

    }
}
