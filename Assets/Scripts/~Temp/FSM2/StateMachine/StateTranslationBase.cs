using System;

namespace Framework
{
    public class StateTranslationBase : ICondition
    {
        public int FromState;
        public int ToState;
        public bool CanTranslation = false;
        public StateCommonData CommonData;

        public StateTranslationBase(StateCommonData commonData)
        {
            CommonData = commonData;
        }

        public virtual bool Satisfied()
        {
            return CanTranslation;
        }

        public virtual void Init(int fromState, int toState)
        {
            FromState = fromState;
            ToState = toState;
        }

        public virtual void Release()
        {
        }

        public virtual void Reset()
        {
            CanTranslation = false;
        }
    }
}