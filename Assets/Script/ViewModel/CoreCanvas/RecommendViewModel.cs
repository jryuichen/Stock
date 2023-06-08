using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.CoreCanvas
{
    public class RecommendViewModel :ViewModelBase
    {
        private RecommendViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
        }

        // Factories
        public static RecommendViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}