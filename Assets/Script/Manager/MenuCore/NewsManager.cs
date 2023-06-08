using Script.BaseClass;
using Script.View.Core;
using Script.ViewModel.CoreCanvas;

namespace Script.Manager.MenuCore
{
    public class NewsManager : GameSystem
    {
        public NewsView NewsView => NewsView.Instance;
        private NewsViewModel NewsViewModel { get; }

        private NewsManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            NewsViewModel = NewsViewModel.Create(stockMainManager);
        }
        // Factories
        public static NewsManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}