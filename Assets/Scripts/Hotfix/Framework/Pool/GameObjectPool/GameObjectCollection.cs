using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏物体对象池
/// </summary>
public class GameObjectCollection
{
    private string poolKey;
    private GameObject prefab;
    private Queue<GameObject> gameObjects = new();
    private int capacity = -1;//-1表示无限容量

    public GameObjectCollection(string poolKey)
    {
        this.poolKey = poolKey;
        prefab = Resources.Load<GameObject>(poolKey);//TODO
    }

    public GameObject Get(bool active = true)
    {
        if (gameObjects.Count > 0)
        {
            var tempGo = gameObjects.Dequeue();
            tempGo.SetActive(active);
            return tempGo;
        }
        var newGo = Instantiate();
        newGo.SetActive(active);
        return newGo;
    }

    public bool Put(GameObject go)
    {
        if (go == null)
        {
            Debug.LogError($"go为null");
            return false;
        }
        if (go.name != poolKey)
        {
            Debug.LogError($"不属于此池子，对象名：{go.name}，池子key：{poolKey}");
            Destory(go);
            return false;
        }

        if (capacity >= 0 && gameObjects.Count >= capacity)
        {
            Destory(go);
            Debug.LogError($"池子容量已满，无法回收，直接销毁，池子key：{poolKey}");
            return false;
        }

        go.transform.SetParent(GameObjectPool.GameObjectRoot, false);
        IPoolObject poolObject = go.GetComponent<IPoolObject>();
        poolObject?.OnRecycle();
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
            var newGo = Instantiate();
            newGo.SetActive(false);
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
    }

    public void Dispose()
    {
        while (gameObjects.Count > 0)
        {
            GameObject tempGo = gameObjects.Dequeue();
            Destory(tempGo);
        }
        gameObjects.Clear();
    }

    private GameObject Instantiate()
    {
        GameObject newGo = Object.Instantiate(prefab);
        newGo.name = poolKey;
        newGo.transform.SetParent(GameObjectPool.GameObjectRoot, false);
        IPoolObject poolObject = newGo.GetComponent<IPoolObject>();
        poolObject?.OnInit();
        return newGo;
    }

    private void Destory(GameObject go)
    {
        Object.Destroy(go);
    }
}