namespace Ddd.Application
{
    public interface IFSMState
    {
        void Enter();
        void Exit();
    }
}