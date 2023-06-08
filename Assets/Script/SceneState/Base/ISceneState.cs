

// 場景狀態

using Script.Controller;

namespace Script.SceneState.Base
{
	public abstract class ISceneState
	{


		protected SceneStateController Controller { get; }
		public bool IsRunBegin { get;private set; }

        // Constructor
		protected ISceneState(SceneStateController controller)
		{
			Controller = controller;
		}


        public virtual void StateBegin()
        {
            IsRunBegin = true;

        }
        public virtual void StateEnd() {}
        public virtual void StateUpdate() {}

		public override string ToString ()
		{
			return  $"{nameof(ISceneState)}";
		}


	}
}
