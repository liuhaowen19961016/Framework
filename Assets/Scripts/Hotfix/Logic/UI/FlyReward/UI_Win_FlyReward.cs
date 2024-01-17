using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

/// <summary>
/// 每个飞奖励的数据
/// </summary>
public class FlyRewardData : IPoolObject
{
    public int itemId;
    public Vector3 fromLocalPos;
    public Vector3 toLocalPos;
    public Transform targetTrans;
    public int showCount;
    public int realCount;

    public void OnInit()
    {

    }

    public void SetData(int itemId, Transform targetTrans, Vector3 fromLocalPos, Vector3 toLocalPos, int showCount, int realCount)
    {
        this.itemId = itemId;
        this.fromLocalPos = fromLocalPos;
        this.toLocalPos = toLocalPos;
        this.showCount = showCount;
        this.targetTrans = targetTrans;
        this.realCount = realCount;
    }

    public void OnRecycle()
    {
        itemId = -1;
        fromLocalPos = Vector3.zero;
        toLocalPos = Vector3.zero;
        targetTrans = null;
        showCount = -1;
        realCount = -1;
    }
}

/// <summary>
/// 飞奖励面板
/// </summary>
public partial class UI_Win_FlyReward : MonoBehaviour
{
    public void Awake()
    {
        GameEvent.AddListener<EvtEnqueueFlyRewardData>(EGameEventType.EnqueueFlyRewardData, OnEnqueueFlyRewardData);
        GameEvent.AddListener<EvtPlayFlyRewardGroupComplete>(EGameEventType.PlayFlyRewardGroupComplete, OnPlayFlyRewardGroupComplete);

        GameObjectPool.PreLoad("FlyReward1", 5);//TODO
    }

    private void ShowFlyReward(int itemId, Transform targetTrans, Vector3 fromLocalPos, Vector3 toLocalPos, int showCount, bool isLastInGroup, int realCount)
    {
        // 比如飞奖励还没播放完就进入到了战斗场景，targetTrans为null
        if (targetTrans == null)
        {
            // 单类型飞奖励完成
            var evtPlayFlyRewardSingleTypeComplete = GameEventDataPool.Allocate<EvtPlayFlyRewardSingleTypeComplete>();
            evtPlayFlyRewardSingleTypeComplete.itemId = itemId;
            evtPlayFlyRewardSingleTypeComplete.realCount = realCount;
            GameEvent.DispatchGameEvent(evtPlayFlyRewardSingleTypeComplete);
            // 本组飞奖励完成
            if (isLastInGroup)
            {
                var evtPlayFlyRewardGroupComplete = GameEventDataPool.Allocate<EvtPlayFlyRewardGroupComplete>();
                evtPlayFlyRewardGroupComplete.itemId = itemId;
                evtPlayFlyRewardGroupComplete.realCount = realCount;
                GameEvent.DispatchGameEvent(evtPlayFlyRewardGroupComplete);
            }
            return;
        }

        // TODO：飞的效果
        // 生成一堆然后依次飞向目标点
        //List<GameObject> itemList = new();
        //for (int i = 0; i < showCount; i++)
        //{
        //    GameObject flyItemGo = GameObjectPool.Get(GetFlyRewardPrefabName(itemId));
        //    flyItemGo.transform.SetParent(transform, false);
        //    flyItemGo.transform.localPosition = fromLocalPos;
        //    itemList.Add(flyItemGo);
        //    flyItemGo.transform.DOScale(Vector3.one * 1.8f, 0.2f);
        //    flyItemGo.transform.DOLocalMove(GetRandomPos(fromLocalPos), 0.2f);
        //}
        //TimerManager.Ins.Register(0.2f, onComplete: () =>
        //{
        //    float timer = 0;
        //    for (int i = 0; i < itemList.Count; i++)
        //    {
        //        int index = i;
        //        TimerManager.Ins.Register(timer, onComplete: () =>
        //        {
        //            Sequence sequence = DOTween.Sequence();
        //            sequence.Insert(0, itemList[index].transform.DOLocalMove(toLocalPos, 0.5f).SetEase(Ease.InSine));
        //            sequence.SetUpdate(true);
        //            sequence.OnComplete(() =>
        //            {
        //                GameObjectPool.Put(itemList[index]);
        //                // 单次飞奖励完成
        //                var evtPlayFlyRewardSingleOnceComplete = GameEventDataPool.Allocate<EvtPlayFlyRewardSingleOnceComplete>();
        //                evtPlayFlyRewardSingleOnceComplete.itemId = itemId;
        //                evtPlayFlyRewardSingleOnceComplete.perRealCount = realCount * 1 / itemList.Count;
        //                GameEvent.Dispatch(evtPlayFlyRewardSingleOnceComplete);
        //                if (index == itemList.Count - 1)
        //                {
        //                    // 单类型飞奖励完成
        //                    var evtPlayFlyRewardSingleTypeComplete = GameEventDataPool.Allocate<EvtPlayFlyRewardSingleTypeComplete>();
        //                    evtPlayFlyRewardSingleTypeComplete.itemId = itemId;
        //                    evtPlayFlyRewardSingleTypeComplete.realCount = realCount;
        //                    GameEvent.Dispatch(evtPlayFlyRewardSingleTypeComplete);
        //                }
        //                if (index == itemList.Count - 1 && isLastInGroup)
        //                {
        //                    // 本组飞奖励完成
        //                    var evtPlayFlyRewardGroupComplete = GameEventDataPool.Allocate<EvtPlayFlyRewardGroupComplete>();
        //                    evtPlayFlyRewardGroupComplete.itemId = itemId;
        //                    evtPlayFlyRewardGroupComplete.realCount = realCount;
        //                    GameEvent.Dispatch(evtPlayFlyRewardGroupComplete);
        //                }
        //            });
        //        });
        //        timer += 1f;
        //    }
        //});
    }

    // TODO
    private string GetFlyRewardPrefabName(int itemId)
    {
        string flyRewardPrefabName = null;
        switch (itemId)
        {
            case 1:
                flyRewardPrefabName = "FlyReward1";
                break;
            case 2:
                flyRewardPrefabName = "FlyReward2";
                break;
            default:
                break;
        }
        return flyRewardPrefabName;
    }

    private Vector2 GetRandomPos(Vector2 localPos)
    {
        int offset = 100;
        float randomX = Random.Range(-offset, offset);
        float randomY = Random.Range(-offset, offset);
        float targetPosX = localPos.x + randomX;
        float targetPosY = localPos.y + randomY;
        Vector2 pos = new Vector2(targetPosX, targetPosY);
        return pos;
    }

    public void OnDestroy()
    {
        GameEvent.RemoveListener<EvtEnqueueFlyRewardData>(EGameEventType.EnqueueFlyRewardData, OnEnqueueFlyRewardData);
        GameEvent.RemoveListener<EvtPlayFlyRewardGroupComplete>(EGameEventType.PlayFlyRewardGroupComplete, OnPlayFlyRewardGroupComplete);
        flyRewardDataQueue.Clear();
        tempFlyRewardDatas.Clear();
        tempFlyRewardDataDict.Clear();
    }

    #region 飞奖励数据相关

    private Queue<List<FlyRewardData>> flyRewardDataQueue = new();
    private bool flyRewardIsInShow;//是否有飞奖励组在展示中

    private List<FlyRewardData> tempFlyRewardDatas = new();
    Dictionary<int, List<FlyRewardData>> tempFlyRewardDataDict = new();

    private void OnEnqueueFlyRewardData(EvtEnqueueFlyRewardData data)
    {
        if (data == null)
            return;
        //if (!GameUtil.AllSame(data.itemIdList.Count, data.showCountList.Count, data.fromWorldPosList.Count, data.realCountList.Count))
        //{
        //    Log.Error("飞奖励数据不匹配！！！！！");
        //    return;
        //}
        Camera worldCamera = GetWorldCamera();
        if (worldCamera == null)
            return;

        // 将本次所有飞奖励数据添加到列表中
        tempFlyRewardDatas.Clear();
        Camera uiCamera = GameObject.Find("Canvas").GetComponent<Canvas>().worldCamera;//TODO
        for (int i = 0; i < data.itemIdList.Count; i++)
        {
            //Vector3 fromLocalPos = CTUtils.World2UILocal(data.fromWorldPosList[i], transform as RectTransform, worldCamera, uiCamera);
            Transform targetTrans = GetTargetTrans(data.itemIdList[i]);
            // 目标位置为null的则没有飞奖励效果，直接视为本类型的飞奖励效果完成
            if (targetTrans == null)
            {
                var evtPlayFlyRewardSingleTypeComplete = GameEventDataPool.Allocate<EvtPlayFlyRewardSingleTypeComplete>();
                evtPlayFlyRewardSingleTypeComplete.itemId = data.itemIdList[i];
                evtPlayFlyRewardSingleTypeComplete.realCount = data.realCountList[i];
                GameEvent.DispatchGameEvent(evtPlayFlyRewardSingleTypeComplete);
                continue;
            }
            //Vector3 toLocalPos = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(targetTrans.transform.position, uiCamera), transform as RectTransform, uiCamera);
            FlyRewardData flyRewardData = ReferencePool.Allocate<FlyRewardData>();
            //flyRewardData.SetData(data.itemIdList[i], targetTrans, fromLocalPos, toLocalPos, data.showCountList[i], data.realCountList[i]);
            tempFlyRewardDatas.Add(flyRewardData);
        }
        // 根据优先级添加到飞奖励数据的队列中
        tempFlyRewardDatas.Sort(Sort);
        tempFlyRewardDataDict.Clear();
        foreach (var flyRewardData in tempFlyRewardDatas)
        {
            if (!tempFlyRewardDataDict.TryGetValue(flyRewardData.itemId, out var flyRewardDatas))
            {
                flyRewardDatas = new List<FlyRewardData>();
                tempFlyRewardDataDict.Add(flyRewardData.itemId, flyRewardDatas);
            }
            flyRewardDatas.Add(flyRewardData);
        }
        foreach (var v in tempFlyRewardDataDict.Values)
        {
            flyRewardDataQueue.Enqueue(v);
        }

        TryPlayFlyReward();
    }

    private void OnPlayFlyRewardGroupComplete(EvtPlayFlyRewardGroupComplete data)
    {
        if (data == null)
            return;

        flyRewardIsInShow = false;
        TryPlayFlyReward();
    }

    private void TryPlayFlyReward()
    {
        if (flyRewardIsInShow)
            return;
        if (flyRewardDataQueue.Count == 0)
            return;

        var flyRewardDatas = flyRewardDataQueue.Dequeue();
        if (flyRewardDatas == null || flyRewardDatas.Count <= 0)
            return;
        flyRewardIsInShow = true;
        for (int i = 0; i < flyRewardDatas.Count; i++)
        {
            ShowFlyReward(flyRewardDatas[i].itemId, flyRewardDatas[i].targetTrans, flyRewardDatas[i].fromLocalPos, flyRewardDatas[i].toLocalPos, flyRewardDatas[i].showCount,
                 i == flyRewardDatas.Count - 1, flyRewardDatas[i].realCount);
            ReferencePool.Recycle(flyRewardDatas[i]);
        }
    }

    // TODO
    private Transform GetTargetTrans(int itemId)
    {
        Transform targetTrans = null;
        return targetTrans;
    }

    // TODO
    private Camera GetWorldCamera()
    {
        Camera worldCamera = null;
        return worldCamera;
    }

    // TODO
    private int Sort(FlyRewardData data1, FlyRewardData data2)
    {
        if (data1 == null || data2 == null)
            return 0;
        int ret = data1.itemId.CompareTo(data2.itemId);
        return ret;
    }

    #endregion 飞奖励数据相关
}