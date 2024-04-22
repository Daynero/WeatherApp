using Infrastructure;
using Infrastructure.States;
using Logic.UI.Curtain;
using PanelsNavigationModule;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private UiModuleMono uiModuleMono;
    
    private Game _game;

    private void Awake()
    {
        IUiModuleService uiModuleService = new UiModuleCore(uiModuleMono);
        _game = new Game(this, uiModuleService);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}