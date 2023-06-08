using Script.BaseClass;
using Script.Manager;
using Script.View;
using Script.ViewModel.CoreCanvas;
using UnityEngine.SceneManagement;

namespace Script.Controller
{
    public class CoreCanvasController : GameSystem
    {
        // Getter
        private CoreCanvasView CoreCanvasView { get; }
        private CoreCanvasViewModel CoreCanvasViewModel { get; }
        private SceneStateController SceneStateController => StockMainManager.SceneStateController;

        // Constructor
        private CoreCanvasController(StockMainManager stockMainManager, CoreCanvasView canvasView) : base(
            stockMainManager)
        {
            CoreCanvasView = canvasView;
            CoreCanvasViewModel = CoreCanvasViewModel.Create(stockMainManager);
        }

        // Interface
        public void Init()
        {
            AddListener();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // Methods
        private void AddListener()
        {
            CoreCanvasView.btnHome.onClick.AddListener(HandleButtonHomeClick);
            CoreCanvasView.btnRecommend.onClick.AddListener(HandleButtonRecommendClick);
            CoreCanvasView.btnSubscribed.onClick.AddListener(HandleButtonSubscribedClick);
            CoreCanvasView.btnNews.onClick.AddListener(HandleButtonNewsClick);
            CoreCanvasView.btnPersonal.onClick.AddListener(HandleButtonPersonalClick);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            CoreCanvasView.BottomBar.SetActive(SceneStateController.NextIsLoggedState);
        }

        private void HandleButtonPersonalClick()
        {
            CoreCanvasViewModel.PersonalCommand.CheckExecute();
        }

        private void HandleButtonNewsClick()
        {
            CoreCanvasViewModel.NewsCommand.CheckExecute();
        }

        private void HandleButtonSubscribedClick()
        {
            CoreCanvasViewModel.SubscribedCommand.CheckExecute();
        }

        private void HandleButtonRecommendClick()
        {
            CoreCanvasViewModel.RecommendCommand.CheckExecute();
        }

        private void HandleButtonHomeClick()
        {
            CoreCanvasViewModel.HomeCommand.CheckExecute();
        }

        // Factories
        public static CoreCanvasController Create(StockMainManager stockMainManager, CoreCanvasView canvasView) =>
            new(stockMainManager, canvasView);
    }
}