using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;

namespace Script.ViewModel.InitSplash
{
    public class InitSplashViewModel :  ViewModelBase
    {
        private InitSplashViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
        }

        // Factories
        public static InitSplashViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}