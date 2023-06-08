using Script.Manager;
using UnityEngine;

// 遊戲使用者界面
namespace Script.BaseClass
{
	public abstract class UserInterface
	{
		private StockMainManager StockMainManager { get; }
		private readonly GameObject	_rootUi = null;
		private bool _isActive = true;

		protected UserInterface(StockMainManager stockMainManager)
		{
			StockMainManager = stockMainManager;
		}

		public bool IsVisible()
		{
			return _isActive;
		}

		public virtual void Show()
		{
			_rootUi.SetActive(true);
			_isActive = true;
		}

		public virtual void Hide()
		{
			_rootUi.SetActive(false);
			_isActive = false;
		}

		public virtual void Initialize(){}
		public virtual void Release(){}
		public virtual void Update(){}

	}
}
