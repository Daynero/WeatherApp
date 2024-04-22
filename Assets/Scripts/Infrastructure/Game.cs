using Infrastructure.States;
using PanelsNavigationModule;

namespace Infrastructure
{
    internal class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, IUiModuleService uiModuleService)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), uiModuleService);
        }
    }
}