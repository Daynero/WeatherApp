using Logic.UI.Curtain;
using Panels.MainPanel;
using PanelsNavigationModule;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadAppState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IUiModuleService _uiModuleService;

        public LoadAppState(GameStateMachine gameStateMachine, SceneLoader sceneLoader,
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
            _uiModuleService.Show(PanelType.Main);
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
        }
    }
}
