using Infrastructure.Services;
using Infrastructure.States;
using Logic.UI.Curtain;
using PanelsNavigationModule;

namespace Infrastructure
{
    internal class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, IUiModuleService uiModuleService)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, uiModuleService);
        }
    }
}