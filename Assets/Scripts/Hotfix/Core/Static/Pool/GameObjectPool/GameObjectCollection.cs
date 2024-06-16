using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏物体对象池
/// </summary>
public class GameObjectCollection
{
    private EGameObjectPoolType gameObjectPoolType;
    private string poolKey;
    private GameObject prefab;
    private Queue<GameObject> gameObjects;
    private int capacity;
    public int ReuseGameObjectCount => gameObjects.Count;
    private Transform parentTrans;

    public GameObjectCollection(string poolKey, EGameObjectPoolType gameObjectPoolType, int capacity, Transform typeRootTrans)
    {
        gameObjects = new Queue<GameObject>();
        this.gameObjectPoolType = gameObjectPoolType;
        this.poolKey = poolKey;
        this.capacity = capacity;
        prefab = Resources.Load<GameObject>(poolKey); //TODO
        parentTrans = new GameObject().transform;
        parentTrans.name = poolKey;
        parentTrans.transform.SetParent(typeRootTrans, false);
    }

    public GameObject Get(bool active = true)
    {
        if (gameObjects.Count > 0)
        {
            var tempGo = gameObjects.Dequeue();
            tempGo.SetActive(active);
            return tempGo;
        }

        var newGo = Instantiate(active);
        return newGo;
    }

    public bool Put(GameObject go)
    {
        if (go == null)
        {
            Log.Error($"GameObject不能为null");
            return false;
        }
        if (go.name != poolKey)
        {
            Log.Error($"不属于此池子，对象名：{go.name}，池子key：{poolKey}");
            Destory(go);
            return false;
        }

        if (capacity >= 0 && gameObjects.Count >= capacity)
        {
            Destory(go);
            Log.Error($"池子容量已满，无法回收，直接销毁，池子key：{poolKey}");
            return false;
        }

        go.transform.SetParent(parentTrans, false);
        gameObjects.Enqueue(go);
        go.SetActive(false);
        return true;
    }

    public void SetCapacity(int capacity)
    {
        this.capacity = capacity;
    }

    public bool Add(int count)
    {
        bool addComplete = true;
        while (count-- > 0)
        {
            var newGo = Instantiate(false);
            gameObjects.Enqueue(newGo);
            if (capacity >= 0 && gameObjects.Count >= capacity)
            {
                addComplete = false;
                break;
            }
        }
        return addComplete;
    }

    public void Remove(int count)
    {
        if (count > gameObjects.Count)
        {
            count = gameObjects.Count;
        }
        while (count-- > 0)
        {
            GameObject tempGo = gameObjects.Dequeue();
            Destory(tempGo);
        }

        if (gameObjects.Count == 0 && parentTrans != null)
        {
            Destory(parentTrans.gameObject);
            parentTrans = null;
        }
    }

    public void Dispose()
    {
        Remove(gameObjects.Count);
        gameObjects.Clear();
    }

    private void Destory(GameObject go)
    {
        Object.Destroy(go);
    }

    private GameObject Instantiate(bool active)
    {
        if (prefab == null)
        {
            Log.Error($"prefab路径有误，池子key：{poolKey}");
            return null;
        }
        GameObject newGo = Object.Instantiate(prefab);
        newGo.name = poolKey;
        newGo.transform.SetParent(parentTrans, false);
        newGo.SetActive(active);
        return newGo;
    }
}