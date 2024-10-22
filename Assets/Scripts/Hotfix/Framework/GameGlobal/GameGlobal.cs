using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Hotfix
{
    public class GameGlobal
    {
        private static Dictionary<Type, ManagerBase> managerDict = new Dictionary<Type, ManagerBase>(); //所有管理器
        private static Dictionary<Type, ModuleBase> moduleDict = new Dictionary<Type, ModuleBase>(); //所有模块

        public static EventSystem EventSystem { get; private set; }
        public static GameObject DontDestroyOnLoadRoot { get; private set; }

        private static void Start()
        {
            Log.Info("Hotfix.GameInit Start");

            // 创建游戏基础游戏物体
            CreateDontDestroyOnLoadRoot();
            CreateEventSystem();

            // 手动注册并初始化Manager和Module（手动注册是为了更直观的管理顺序）
            InitManager();
            InitModule();

            // 注册MonoBehaviour生命周期
            RegisterMonoBehaviourEvent();

            // 游戏启动逻辑执行完成

            // test
            GetMgr<UIMgr>().OpenSync(1, 1231);
        }

        #region Manager

        /// <summary>
        /// 获取Manager
        /// </summary>
        public static T GetMgr<T>()
            where T : ManagerBase
        {
            var type = typeof(T);
            if (!managerDict.TryGetValue(type, out var manager))
            {
                Debug.LogError($"请先注册Manager [{type}]");
                return null;
            }
            return manager as T;
        }

        /// <summary>
        /// 手动注册Manager
        /// </summary>
        private static void InitManager()
        {
            RegisterManager<UIMgr>();
            RegisterManager<ConfigMgr>();
        }

        /// <summary>
        /// 注册Manager
        /// </summary>
        private static void RegisterManager<T>()
            where T : ManagerBase
        {
            var type = typeof(T);
            if (managerDict.ContainsKey(type))
            {
                Debug.LogError($"不能重复注册Manager [{type}]");
                return;
            }
            var manager = Activator.CreateInstance(typeof(T), true) as T;
            manager.Init();
            managerDict.Add(type, manager);
        }

        #endregion Manager

        #region Module

        /// <summary>
        /// 获取Module
        /// </summary>
        public static T GetModule<T>()
            where T : ModuleBase
        {
            var type = typeof(T);
            if (!moduleDict.TryGetValue(type, out var module))
            {
                Debug.LogError($"请先注册Module [{type}]");
                return null;
            }
            return module as T;
        }

        /// <summary>
        /// 手动注册Module
        /// </summary>
        private static void InitModule()
        {
            RegisterModule<ModEvent>();
            RegisterModule<ModTimer>();
        }

        /// <summary>
        /// 注册Module
        /// </summary>
        private static void RegisterModule<T>()
            where T : ModuleBase
        {
            var type = typeof(T);
            if (moduleDict.ContainsKey(type))
            {
                Debug.LogError($"不能重复注册Module [{type}]");
                return;
            }
            var module = Activator.CreateInstance(typeof(T), true) as T;
            module.Init();
            moduleDict.Add(type, module);
        }

        #endregion Module

        #region MonoBehaviour生命周期

        private static void RegisterMonoBehaviourEvent()
        {
            Loader.FixedUpdate += FixedUpdate;
            Loader.Update += Update;
            Loader.LateUpdate += LateUpdate;
            Loader.OnApplicationPause += OnApplicationPause;
            Loader.OnApplicationFocus += OnApplicationFocus;
            Loader.OnApplicationQuit += OnApplicationQuit;
        }

        public static void FixedUpdate()
        {
            foreach (var manager in managerDict.Values)
            {
                manager?.FixedUpdate();
            }
            foreach (var module in moduleDict.Values)
            {
                module?.FixedUpdate();
            }
        }

        public static void Update(float deltaTime)
        {
            foreach (var manager in managerDict.Values)
            {
                manager?.Update(deltaTime);
            }
            foreach (var module in moduleDict.Values)
            {
                module?.Update(deltaTime);
            }
        }

        public static void LateUpdate()
        {
            foreach (var manager in managerDict.Values)
            {
                manager?.LateUpdate();
            }
            foreach (var module in moduleDict.Values)
            {
                module?.LateUpdate();
            }
        }

        public static void OnApplicationPause(bool pauseStatus)
        {
            foreach (var manager in managerDict.Values)
            {
                manager?.OnApplicationPause(pauseStatus);
            }
            foreach (var module in moduleDict.Values)
            {
                module?.OnApplicationPause(pauseStatus);
            }
        }

        private static void OnApplicationFocus(bool hasFocus)
        {
            foreach (var manager in managerDict.Values)
            {
                manager?.OnApplicationFocus(hasFocus);
            }
            foreach (var module in moduleDict.Values)
            {
                module?.OnApplicationFocus(hasFocus);
            }
        }

        public static void OnApplicationQuit()
        {
            foreach (var manager in managerDict.Values)
            {
                manager?.Dispose();
            }
            foreach (var module in moduleDict.Values)
            {
                module?.Dispose();
            }
            Loader.FixedUpdate -= FixedUpdate;
            Loader.Update -= Update;
            Loader.LateUpdate -= LateUpdate;
            Loader.OnApplicationQuit -= OnApplicationQuit;
        }

        #endregion MonoBehaviour生命周期

        #region 游戏基础游戏物体

        private static void CreateEventSystem()
        {
            GameObject eventSystemGo = GameUtils.CreateGameObject("EventSystem", null, false,
                typeof(EventSystem), typeof(StandaloneInputModule));
            EventSystem = eventSystemGo.GetComponent<EventSystem>();
            Object.DontDestroyOnLoad(eventSystemGo);
        }

        private static void CreateDontDestroyOnLoadRoot()
        {
            DontDestroyOnLoadRoot = GameUtils.CreateGameObject("DontDestroyOnLoadRoot", null);
            Object.DontDestroyOnLoad(DontDestroyOnLoadRoot);
        }

        #endregion 游戏基础游戏物体
    }
}