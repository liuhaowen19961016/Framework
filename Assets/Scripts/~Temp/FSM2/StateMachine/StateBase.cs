using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class StateBase
    {
        private Dictionary<int, StateTranslationBase> _translationType2StateTranslation;

        protected Dictionary<int, StateTranslationBase> TranslationType2StateTranslation =>
            _translationType2StateTranslation;

        protected StateBaseMgr _stateMgr;

        protected float timer = 0f;

        protected bool _leaving = false;

        public int FromState;

        public StateBase(StateBaseMgr stateMgr)
        {
            _stateMgr = stateMgr;
            timer = 0f;
        }

        public virtual void Init()
        {
        }

        public void AddTranslation(int translationType, StateTranslationBase stateTranslation, int fromState, int toState)
        {
            _translationType2StateTranslation ??= new Dictionary<int, StateTranslationBase>();
            if (_translationType2StateTranslation.TryGetValue(translationType,
                    out StateTranslationBase existStateTranslation))
            {
                Debug.Log($"【状态机】当前状态已经添加过这个Translation = {translationType}了，检查是否还需要添加！");
            }
            else
            {
                stateTranslation.Init((int)fromState, (int)toState);
                _translationType2StateTranslation.Add(translationType, stateTranslation);
            }
        }

        public virtual void OnEnter()
        {
            _leaving = false;
            if (_translationType2StateTranslation == null) return;
            foreach (var translation in _translationType2StateTranslation)
            {
                translation.Value.Reset();
            }
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }

        public virtual void OnUpdate()
        {
            if (_leaving) return;

            if (_translationType2StateTranslation == null) return;
            foreach (var translation in _translationType2StateTranslation)
            {
                if (translation.Value.Satisfied())
                {
                    _stateMgr.TranslationState(translation.Value.FromState, translation.Value.ToState);
                    return;
                }
            }
        }
        
        public virtual void OnLateUpdate(){}

        public virtual void OnLeave()
        {
            _leaving = true;
            if (_translationType2StateTranslation == null) return;
            foreach (var translation in _translationType2StateTranslation)
            {
                translation.Value.Reset();
            }

            timer = 0;
        }

        public virtual void Release()
        {
            if (_translationType2StateTranslation == null) return;
            foreach (var translation in _translationType2StateTranslation)
            {
                translation.Value.Release();
            }

            _translationType2StateTranslation.Clear();
            _translationType2StateTranslation = null;
        }

        public void MarkAllTranslationPass()
        {
            if (_translationType2StateTranslation == null) return;
            foreach (var translation in _translationType2StateTranslation)
            {
                translation.Value.CanTranslation = true;
            }
        }
    }
}