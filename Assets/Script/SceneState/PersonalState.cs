using Script.BaseClass;
using Script.Controller;
using Script.Manager;
using Script.Manager.MenuCore;
using Script.SceneState.Base;

namespace Script.SceneState
{
    public class PersonalState : ISceneState, Logged
    {
        private PersonalManager PersonalManager { get; }
        public PersonalState(SceneStateController controller) : base(controller)
        {
            PersonalManager = PersonalManager.Create(StockMainManager.Instance);
        }
        // Overrides
        public override void StateBegin()
        {
            base.StateBegin();
            PersonalManager.Initialize();
        }

        public override void StateEnd()
        {
            base.StateEnd();
            PersonalManager.Release();
        }

        public override string ToString() => $"StateName={nameof(PersonalState)}";
    }
}