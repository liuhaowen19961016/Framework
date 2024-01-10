using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏物体对象池管理器
/// </summary>
public class GameObjectPool
{
    private static Transform gameObjectRoot;
    public static Transform GameObjectRoot
    {
        get
        {
            if (gameObjectRoot == null)
            {
                GameObject rootGo = new GameObject("GameObjectPoolRoot");
                GameObject.DontDestroyOnLoad(rootGo);
                gameObjectRoot = rootGo.transform;
            }
            return gameObjectRoot;
        }
    }

    private static Dictionary<string, GameObjectCollection> gameObjectCollections = new();

    public static void PreLoad(GameObject prefab, int count, int capacity = -1)
    {
        var pool = GetGameObjectCollection(prefab);
        pool.SetCapacity(capacity);
        pool.Add(count);
    }

    public static GameObject Get(GameObject prefab)
    {
        var pool = GetGameObjectCollection(prefab);
        return pool.Get();
    }

    public static bool Put(GameObject prefab)
    {
        var pool = GetGameObjectCollection(prefab);
        return pool.Put(prefab);
    }

    public static bool Add(GameObject prefab, int count)
    {
        var pool = GetGameObjectCollection(prefab);
        return pool.Add(count);
    }

    public static void Remove(GameObject prefab, int count)
    {
        var pool = GetGameObjectCollection(prefab);
        pool.Remove(count);
    }

    public static void Dispose(GameObject prefab)
    {
        var pool = GetGameObjectCollection(prefab);
        pool.Dispose();
    }

    public static void DisposeAll()
    {
        foreach (var referencelCollection in gameObjectCollections.Values)
        {
            referencelCollection.Dispose();
        }
        gameObjectCollections.Clear();
    }

    private static GameObjectCollection GetGameObjectCollection(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError($"prefab不能为null");
            return null;
        }

        string poolKey = prefab.name;
        if (!gameObjectCollections.TryGetValue(poolKey, out GameObjectCollection gameObjectlCollection))
        {
            gameObjectlCollection = new GameObjectCollection(prefab);
            gameObjectCollections.Add(poolKey, gameObjectlCollection);
        }
        return gameObjectlCollection;
    }
}
