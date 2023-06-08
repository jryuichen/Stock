using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.CoreCanvas
{
    public class NewsViewModel : ViewModelBase
    {
        private NewsViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
        }

        // Factories
        public static NewsViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}