using Script.BaseClass;
using Script.View.Core;
using Script.ViewModel.CoreCanvas;

namespace Script.Manager.MenuCore
{
    public class RecommendManager : GameSystem
    {
        private RecommendView RecommendView => RecommendView.Instance;
        private RecommendViewModel RecommendViewModel { get; }
        private RecommendManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            RecommendViewModel = RecommendViewModel.Create(stockMainManager);
        }

        // Factories
        public static RecommendManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}