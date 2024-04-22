using Infrastructure.Services;
using PanelsNavigationModule;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial"; 
        private const string IntroScene = "IntroScene"; 
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly IUiModuleService _uiModuleService;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, IUiModuleService uiModuleService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _uiModuleService = uiModuleService;
            
            RegisterServices();
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

        private void RegisterServices()
        {
            _services.RegisterSingle<IUiModuleService>(_uiModuleService);
        }
    }
}