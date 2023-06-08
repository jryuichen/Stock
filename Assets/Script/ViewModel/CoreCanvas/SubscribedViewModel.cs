using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.CoreCanvas
{
    public class SubscribedViewModel : ViewModelBase
    {
        private SubscribedViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
        }

        // Factories
        public static SubscribedViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}