using Script.Controller;
using Script.Manager;

namespace Script.Modules.Infrastructure
{
    public class ViewModelBase
    {
        // Delegates
        public delegate void ViewModelUpdatedHandler();
        // Getters
        protected CoreCanvasController CoreCanvasController => StockMainManager.CoreCanvasController;
        protected SceneStateController SceneStateController => StockMainManager.SceneStateController;
        protected BackController BackController => StockMainManager.BackController;
        protected AccountProviderController AccountProviderController => StockMainManager.AccountProviderController;
        // Data
        private StockMainManager StockMainManager { get; }
        // Events
        public event ViewModelUpdatedHandler ViewModelUpdated;

        // Constructors
        protected ViewModelBase(StockMainManager stockMainManager)
        {
            StockMainManager = stockMainManager;
        }

        // Methods
        protected void NotifyViewModelUpdated()
        {
            ViewModelUpdated?.Invoke();
        }


    }
}