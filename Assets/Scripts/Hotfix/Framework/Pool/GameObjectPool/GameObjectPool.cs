using System.Collections.Generic;
using UnityEngine;

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
                GameObject.DontDestroyOnLoad(rootGo);
                gameObjectPoolRoot = rootGo.transform;
            }
            return gameObjectPoolRoot;
        }
    }

    private static Dictionary<string, GameObjectCollection> gameObjectCollections = new();

    public static void PreLoad(string poolKey, int count, int capacity = -1)
    {
        var pool = GetGameObjectCollection(poolKey);
        pool.SetCapacity(capacity);
        pool.Add(count);
    }

    public static GameObject Get(string poolKey)
    {
        var pool = GetGameObjectCollection(poolKey);
        return pool.Get();
    }

    public static bool Put(GameObject go)
    {
        string poolKey = go.name;
        var pool = GetGameObjectCollection(poolKey);
        return pool.Put(go);
    }

    public static bool Add(string poolKey, int count)
    {
        var pool = GetGameObjectCollection(poolKey);
        return pool.Add(count);
    }

    public static void Remove(string poolKey, int count)
    {
        var pool = GetGameObjectCollection(poolKey);
        pool.Remove(count);
    }

    public static void Dispose(string poolKey)
    {
        var pool = GetGameObjectCollection(poolKey);
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

    public static GameObjectCollection GetGameObjectCollection(string poolKey)
    {
        if (string.IsNullOrEmpty(poolKey))
        {
            Debug.LogError($"poolKey不能为null");
            return null;
        }

        if (!gameObjectCollections.TryGetValue(poolKey, out GameObjectCollection gameObjectlCollection))
        {
            gameObjectlCollection = new GameObjectCollection(poolKey);
            gameObjectCollections.Add(poolKey, gameObjectlCollection);
        }
        return gameObjectlCollection;
    }
}