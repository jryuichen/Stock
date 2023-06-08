using Script.BaseClass;
using Script.View.Core;
using Script.ViewModel.CoreCanvas;
using Script.ViewModel.SignIn;

namespace Script.Manager.MenuCore
{
    public class SubscribedManager : GameSystem
    {
        private SubscribedView SubscribedView => SubscribedView.Instance;
        private SubscribedViewModel SubscribedModel { get; }
        private SubscribedManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            SubscribedModel = SubscribedViewModel.Create(stockMainManager);
        }

        // Factories
        public static SubscribedManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}