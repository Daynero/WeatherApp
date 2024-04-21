using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Infrastructure.Services;
using PanelsNavigationModule;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial"; 
        private const string GameScene = "GameScene";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
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
            _stateMachine.Enter<LoadLevelState, string>(GameScene);

        private void RegisterServices()
        {
            _services.RegisterSingle<IAssets>(new Assets());
            _services.RegisterSingle<IPanelController>(new UiModuleCore());
            _services.RegisterSingle<IGameFactory>(
                new GameFactory(_services.Single<IAssets>()));
        }
    }
}