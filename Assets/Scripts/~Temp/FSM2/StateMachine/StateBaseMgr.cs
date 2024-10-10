using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class StateBaseMgr
    {
        private Dictionary<int, StateBase> _stateType2StateBaseDic;
        public StateCommonData CommonData;
        protected StateBase DefaultState;
        protected StateBase CurrentState;
        private bool _isRelease = false;
        private bool _isTranslationing = false;

        public virtual void Init(StateCommonData commonData)
        {
            CommonData = commonData;
            _isRelease = false;
        }

        /// <summary>
        /// 状态机开始
        /// </summary>
        public virtual void Start()
        {
            if (CurrentState == null)
            {
                Debug.LogError($"【状态机】当前状态机没有指定初始状态，请指定！");
                return;
            }

            CurrentState.OnEnter();
        }


        /// <summary>
        /// 状态机暂停
        /// </summary>
        public virtual void Pause()
        {
            if (CurrentState == null)
            {
                Debug.LogError($"【状态机】 当前状态机没有正在运行的状态，不需要暂停！");
                return;
            }

            CurrentState.OnPause();
        }

        /// <summary>
        /// 从暂停中恢复
        /// </summary>
        public virtual void Resume()
        {
            if (CurrentState == null)
            {
                Debug.LogError($"【状态机】 当前状态机没有正在运行的状态，恢复不了！");
                return;
            }

            CurrentState.OnResume();
        }

        public virtual void Stop()
        {
            if (CurrentState == null)
            {
                Debug.LogError($"【状态机】 当前状态机没有正在运行的状态，无法关闭！");
                return;
            }

            CurrentState.OnLeave();
            Release();
        }

        public virtual void Release()
        {
            _isRelease = true;
            CurrentState?.OnLeave();
            if (_stateType2StateBaseDic != null)
            {
                foreach (var _state in _stateType2StateBaseDic)
                {
                    _state.Value.Release();
                }

                _stateType2StateBaseDic.Clear();

                _stateType2StateBaseDic = null;
            }

            CurrentState = null;

            CommonData?.Release();
            CommonData = null;
        }

        public virtual void Update()
        {
            if (_isRelease) 
                return;
            if (_isTranslationing) 
                return;

            if (CurrentState != null)
                CurrentState.OnUpdate();
        }

        public virtual void LateUpdate()
        {
            if (_isRelease) 
                return;
            if (_isTranslationing) 
                return;

            if (CurrentState != null)
                CurrentState.OnLateUpdate();
        }

        public void AddState(int stateType, StateBase stateBase, bool isDefault = false)
        {
            _stateType2StateBaseDic ??= new Dictionary<int, StateBase>();
            if (_stateType2StateBaseDic.TryGetValue(stateType, out StateBase existStateBase))
            {
                Debug.Log($"【状态机】当前状态机已经添加过State = {stateType}，请检查原因！");
            }
            else
            {
                stateBase.Init();
                _stateType2StateBaseDic.Add(stateType, stateBase);
            }

            if (isDefault)
                SetDefaultState(stateBase);
        }

        public void SetDefaultState(StateBase defaultState)
        {
            DefaultState = defaultState;
            CurrentState = defaultState;
        }

        public void TranslationState(int fromStateType, int stateType)
        {
            if (_isRelease) return;
            _isTranslationing = true;

            if (_stateType2StateBaseDic == null)
            {
                Debug.Log($"【状态机】当前状态机没有状态，无法Translation！");
                return;
            }

            if (_stateType2StateBaseDic.TryGetValue(stateType, out StateBase existState))
            {
                CurrentState?.OnLeave();
                CurrentState = existState;
                CurrentState.FromState = fromStateType;
                CurrentState.OnEnter();
                Log.Info($"CurrentState.OnEnter stateType={stateType}");
            }
            else
            {
                Debug.Log($"【状态机】当前状态机没有State = {stateType}， 请检查原因！");
            }

            _isTranslationing = false;
        }
    }
}