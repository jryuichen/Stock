using Modules.Core.Infrastructure;
using Script.Manager;
using Script.Modules.Infrastructure;
using UniRx.Async;

namespace Script.ViewModel.InitSplash
{
    public class BackViewModel : ViewModelBase
    {
        // Checkers
        private bool CanClickBack => !BackCommandInvoke.IsExecuting && !BackCommandInvoke.IsAnimLock;
        private bool CanHideBack => !BackCommandInvoke.HasCommands;
        private bool CanHideBackAsync => !BackCommandInvoke.HasCommands||BackCommandInvoke.CommandsCount==1;

        private bool CanShowBack => BackCommandInvoke.HasCommands;

        // Commands
        public Command BackCommand { get; }
        public Command ShowBackCommand { get; }
        public Command HideBackCommand { get; }
        public Command HideBackAsyncCommand { get; }

        // Data
        public CommandInvoke<ICommand> BackCommandInvoke { get; }

        //Constructor
        private BackViewModel(StockMainManager stockMainManager) : base(stockMainManager)
        {
            BackCommandInvoke = new CommandInvoke<ICommand>();
            BackCommand = new Command(() => CanClickBack, OnBackClicked);
            ShowBackCommand = new Command(() => CanShowBack, OnShowBackClicked);
            HideBackCommand = new Command(() => CanHideBack, OnHideBackClicked);
            HideBackAsyncCommand = new Command(() => CanHideBackAsync, OnHideBackClicked);
            BackCommandInvoke.FinalCommandExecuted.AddListener(OnFinalBackCommandExecuted);
        }
        // Methods
        private void OnHideBackClicked()
        {
            NotifyViewModelUpdated();
        }

        private void OnShowBackClicked()
        {
            NotifyViewModelUpdated();
        }

        private async UniTask OnBackClicked()
        {
            await BackCommandInvoke.ExecuteNextAsync();
        }

        private void OnFinalBackCommandExecuted()
        {
            HideBackCommand.CheckExecute();
        }

        // Factories
        public static BackViewModel Create(StockMainManager stockMainManager) => new(stockMainManager);
    }
}