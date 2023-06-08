using System;
using Script.BaseClass;
using Script.Manager;
using Script.View.InitSplash;
using Script.ViewModel.InitSplash;
using UniRx.Async;
using UnityEngine;

namespace Script.Controller
{
    public class BackController : GameSystem
    {
        private BackView BackView { get; }
        private BackViewModel BackViewModel { get; }

        private BackController(StockMainManager stockMainManager, BackView view) : base(stockMainManager)
        {
            BackView = view;
            BackViewModel = BackViewModel.Create(stockMainManager);
            BackViewModel.ViewModelUpdated+=OnViewModelUpdated;
            BackViewModel.HideBackCommand.CheckExecute();
        }

        public override void Initialize()
        {

            BackView.btnBack.onClick.AddListener(async ()=>await HandleBackButtonOnClick());
        }

        // Interface
        public void OnBackReceiver(Action action)
        {
            var back = new BackCommandBase(() =>
            {
                action.Invoke();
                BackViewModel.HideBackCommand.CheckExecute();
            });
            BackViewModel.BackCommandInvoke.AddBackCommand(back);
            BackViewModel.ShowBackCommand.CheckExecute();
        }

        public void OnBackReceiverAsync(Func<UniTask> asyncAction)
        {
            var back = new BackCommandBase(async ()=>
            {
                BackView.eventSystem.enabled = false;
                BackViewModel.HideBackAsyncCommand.CheckExecuteAsync();
                await asyncAction();
                BackView.eventSystem.enabled = true;
            });
            BackViewModel.BackCommandInvoke.AddBackCommand(back);
            BackViewModel.ShowBackCommand.CheckExecute();
        }

        public async UniTask ExecuteBackCommandAsync()
        {
            await BackViewModel.BackCommand.CheckExecuteAsync();
        }

        public  void ExecuteBackCommand()
        {
             BackViewModel.BackCommand.CheckExecute();
        }
        // Methods
        private async UniTask HandleBackButtonOnClick()
        {
           await BackViewModel.BackCommand.CheckExecuteAsync();
        }

        private void OnViewModelUpdated()
        {
            BackView.btnBack.gameObject.SetActive(BackViewModel.BackCommandInvoke.HasCommands);
        }

        // Factories
        public static BackController Create(StockMainManager stockMainManager,BackView view) => new(stockMainManager,view);
    }
}