namespace Framework
{
    public interface IStateNode
    {
        void OnCreate(StateMachine machine);
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}