using PanelsNavigationModule;

namespace Infrastructure.States
{
    public class LoadIntroState : IPayloadedState<string>
    {
        private const string GameScene = "GameScene";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUiModuleService _uiModuleService;

        public LoadIntroState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
            IUiModuleService uiModuleService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiModuleService = uiModuleService;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _uiModuleService.Show(PanelType.Intro);
            _uiModuleService.OnPanelDisposedEvent += panelType =>
            {
                if (panelType == PanelType.Intro)
                    _gameStateMachine.Enter<LoadAppState, string>(GameScene);
            };
        }

        public void Exit()
        {
            
        }
    }
}