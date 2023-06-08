using Script.Controller;
using Script.View;
using Script.View.InitSplash;
using UnityEngine;

namespace Script.Manager
{
    public class StockMainManager : Singleton<StockMainManager>
    {
        [SerializeField]private CoreCanvasView coreCanvasView;
        [SerializeField]private BackView backView;

        public SceneStateController SceneStateController { get;private set; }
        public CoreCanvasController CoreCanvasController { get; private set; }
        public BackController BackController { get; private set; }
        public AccountProviderController AccountProviderController { get; private set; }

        // Life Cycle
        private void Start()
        {
            CoreCanvasController = CoreCanvasController.Create(this,coreCanvasView);
            SceneStateController = SceneStateController.CreateInitState();
            BackController = BackController.Create(this,backView);
            AccountProviderController = AccountProviderController.Create(this);
            Init();
        }

        private void Init()
        {
            CoreCanvasController.Init();
            BackController.Initialize();
        }

        // Update is called once per frame
        private void Update()
        {
            SceneStateController.StateUpdate();
            if (Input.GetKeyDown(KeyCode.A))
            {
                 back();
            }
        }

        public async void back()
        {
            await BackController.ExecuteBackCommandAsync();
            Debug.Log("Back");
        }
    }
}