using Script.BaseClass;
using Script.View.Core;
using Script.ViewModel.CoreCanvas;

namespace Script.Manager.MenuCore
{
    public class PersonalManager : GameSystem
    {
        private PersonalView PersonalView => PersonalView.Instance;
        private PersonalViewModel PersonalViewModel { get; }

        private PersonalManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            PersonalViewModel = PersonalViewModel.Create(stockMainManager);
        }

        // Factories
        public static PersonalManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}