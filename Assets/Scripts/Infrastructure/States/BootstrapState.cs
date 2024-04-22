using PanelsNavigationModule;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial"; 
        private const string IntroScene = "IntroScene"; 
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUiModuleService _uiModuleService;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IUiModuleService uiModuleService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiModuleService = uiModuleService;
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadIntroState, string>(IntroScene);
    }
}