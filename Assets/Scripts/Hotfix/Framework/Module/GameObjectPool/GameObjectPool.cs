using System.Collections.Generic;
using Framework;
using UnityEngine;

/*****
 todo comment 目前只支持游戏物体路径与游戏物体名相同的情况
 ******/

/// <summary>
/// 游戏物体对象池类型
/// </summary>
public enum EGameObjectPoolType
{
    Global = 1, //全局
    InBattle, //战斗内
}

/// <summary>
/// 游戏物体对象池管理器
/// </summary>
public class GameObjectPool
{
    private static Transform gameObjectPoolRoot;
    public static Transform GameObjectPoolRoot
    {
        get
        {
            if (gameObjectPoolRoot == null)
            {
                GameObject rootGo = new GameObject("GameObjectPoolRoot");
                Object.DontDestroyOnLoad(rootGo);
                gameObjectPoolRoot = rootGo.transform;
            }
            return gameObjectPoolRoot;
        }
    }

    private const int DefaultCapacity = 50;

    private static Dictionary<EGameObjectPoolType, Dictionary<string, GameObjectCollection>> gameObjectCollections = new Dictionary<EGameObjectPoolType, Dictionary<string, GameObjectCollection>>();
    private static Dictionary<EGameObjectPoolType, Transform> gameObjectPoolType2Root = new Dictionary<EGameObjectPoolType, Transform>();

    public static void PreLoad(string poolKey, int count, int capacity = DefaultCapacity, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        if (count <= 0)
            return;

        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType, capacity, true);
        if (pool == null)
            return;
        pool.Add(count);
    }

    public static GameObject Get(string poolKey, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool active = true)
    {
        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType, forceGet: true);
        if (pool == null)
            return null;
        return pool.Get(active);
    }

    public static bool Put(GameObject go, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        string poolKey = go.name;
        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType);
        if (pool == null)
        {
            Object.Destroy(go);
            return false;
        }
        return pool.Put(go);
    }

    public static bool Add(string poolKey, int count, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType);
        if (pool == null)
            return false;
        return pool.Add(count);
    }

    public static void Remove(string poolKey, int count, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType);
        if (pool == null)
            return;
        pool.Remove(count);
    }

    public static void Dispose(EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pools = GetGameObjectCollections(gameObjectPoolType);
        if (pools == null)
            return;
        foreach (var pool in pools.Values)
        {
            pool.Dispose();
        }
        gameObjectCollections.Remove(gameObjectPoolType);
        Object.Destroy(gameObjectPoolType2Root[gameObjectPoolType].gameObject);
        gameObjectPoolType2Root.Remove(gameObjectPoolType);
    }

    public static void DisposeAll()
    {
        foreach (var kvp in gameObjectCollections)
        {
            Dispose(kvp.Key);
        }
        gameObjectCollections.Clear();
        gameObjectPoolType2Root.Clear();
    }

    public static void SetCapacity(string poolKey, int capacity, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType);
        if (pool == null)
            return;
        pool.SetCapacity(capacity);
    }

    public static int GetReuseGameObjectCount(string poolKey, EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global)
    {
        var pool = GetGameObjectCollection(poolKey, gameObjectPoolType);
        if (pool == null)
            return 0;
        int count = pool.ReuseGameObjectCount;
        return count;
    }

    #region Tools

    private static GameObjectCollection GetGameObjectCollection(string poolKey,
        EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, int capacity = DefaultCapacity, bool forceGet = false)
    {
        if (string.IsNullOrEmpty(poolKey))
        {
            Log.Error("poolKey不能为空");
            return null;
        }
        var gameObjectCollections = GetGameObjectCollections(gameObjectPoolType, forceGet);
        if (gameObjectCollections == null)
            return null;

        if (!gameObjectPoolType2Root.TryGetValue(gameObjectPoolType, out var rootTrans))
        {
            rootTrans = new GameObject().transform;
            rootTrans.name = gameObjectPoolType.ToString();
            rootTrans.transform.SetParent(GameObjectPoolRoot, false);
            gameObjectPoolType2Root[gameObjectPoolType] = rootTrans;
        }
        if (!gameObjectCollections.TryGetValue(poolKey, out var gameObjectCollection))
        {
            gameObjectCollection = new GameObjectCollection(poolKey, gameObjectPoolType, capacity, rootTrans);
            gameObjectCollections.Add(poolKey, gameObjectCollection);
        }
        return gameObjectCollection;
    }

    private static Dictionary<string, GameObjectCollection> GetGameObjectCollections(EGameObjectPoolType gameObjectPoolType = EGameObjectPoolType.Global, bool forceGet = false)
    {
        if (!gameObjectCollections.TryGetValue(gameObjectPoolType, out var existGameObjectCollections))
        {
            if (forceGet)
            {
                existGameObjectCollections = new Dictionary<string, GameObjectCollection>();
                gameObjectCollections.Add(gameObjectPoolType, existGameObjectCollections);
            }
        }
        return existGameObjectCollections;
    }

    #endregion Tools
}