using Infrastructure;
using Infrastructure.States;
using Logic.UI.Curtain;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain curtain;
    
    private Game _game;

    private void Awake()
    {
        _game = new Game(this, curtain);
        _game.StateMachine.Enter<BootstrapState>();

        DontDestroyOnLoad(this);
    }
}