namespace Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
        void Exit();
    }

    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
        void Exit();
    }
}