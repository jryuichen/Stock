using Script.BaseClass;
using Script.View.InitSplash;
using Script.ViewModel.InitSplash;

namespace Script.Manager.InitSplash
{
    public class InitSplashManager : GameSystem
    {
        private InitSplashView InitSplashView => InitSplashView.Instance;
        private InitSplashViewModel InitSplashViewModel { get; }

        private InitSplashManager(StockMainManager stockMainManager) : base(stockMainManager)
        {
            InitSplashViewModel = InitSplashViewModel.Create(stockMainManager);
        }

        // Factories
        public static InitSplashManager Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}