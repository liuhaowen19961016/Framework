namespace Framework
{
    public interface ICondition
    {
        bool Satisfied();

        void Init(int formState, int toState);
        void Release();
        void Reset();
    }
}