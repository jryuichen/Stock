using Script.Manager;

namespace Script.BaseClass
{
	public abstract class GameSystem
	{
		public StockMainManager StockMainManager { get; }

		public GameSystem(StockMainManager stockMainManager )
		{
			StockMainManager = stockMainManager;
		}

		public virtual void Initialize(){}
		public virtual void Release(){}
		public virtual void Update(){}

	}
}
