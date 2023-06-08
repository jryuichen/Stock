using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.CoreCanvas
{
    public class PersonalViewModel : ViewModelBase
    {
        protected PersonalViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
        }
        // Factories
        public static PersonalViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}