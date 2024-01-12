//using UnityEngine.UI;
//using UnityEngine;
//using Hotfix;
//using PBConfig;
//using DG.Tweening;
//using System;
//using Random = UnityEngine.Random;
//using System.Collections.Generic;

///// <summary>
///// 每个飞奖励的数据
///// </summary>
//public class FlyRewardData
//{
//    public int itemId;
//    public Vector3 fromLocalPos;
//    public Vector3 toLocalPos;
//    public Transform targetTrans;
//    public int showCount;
//    public Action onComplete;
//    public int realCount;

//    public void Init(int itemId, Transform targetTrans, Vector3 fromLocalPos, Vector3 toLocalPos, int showCount, Action onComplete, int realCount)
//    {
//        this.itemId = itemId;
//        this.fromLocalPos = fromLocalPos;
//        this.toLocalPos = toLocalPos;
//        this.showCount = showCount;
//        this.onComplete = onComplete;
//        this.targetTrans = targetTrans;
//        this.realCount = realCount;
//    }
//}

///// <summary>
///// 飞奖励面板
///// </summary>
//public partial class UI_Win_FlyReward
//{
//    public override void InitializeParams()
//    {
//        base.InitializeParams();
//        layerId = UILayerId.Top;
//        NeedBackToLastUI = false;
//        NeedControl = false;
//    }

//    public override void onCreate()
//    {
//        base.onCreate();
//        GameUtil.RegEvent(EGameEventType.EnqueueFlyRewardData, HandleEnqueueFlyRewardData);
//        GameUtil.RegEvent(EGameEventType.FlyRewardGroupComplete, PlayFlyRewardGroupComplete);
//        GameUtil.RegEvent(EGameEventType.ChangeSceneComplete, HandleChangeSceneComplete);
//        FlyItem.gameObject.SetActive(false);
//    }

//    private void PlayFlyItem(int itemId, GameObject prefab, Transform targetTrans, Vector3 fromLocalPos, Vector3 toLocalPos, int showCount, Action onComplete, bool isLastInGroup, int realCount)
//    {
//        // 比如飞奖励还没飞完进入到了战斗场景，targetTrans是null
//        if (targetTrans == null)
//        {
//            onComplete?.Invoke();
//            // 单独飞奖励完成
//            var evtPlayFlyItemComplete = GameEventDataPool.Allocate<e>();
//            evtPlayFlyItemComplete.itemId = itemId;
//            evtPlayFlyItemComplete.perRealCount = realCount;
//            SendGameEvent(evtPlayFlyItemComplete);
//            // 本组最后一个飞奖励完成
//            if (isLastInGroup)
//            {
//                var evtPlayFlyItemGroupComplete = GameEventDataPool.CreateEvent<EvtFlyRewardGroupComplete>();
//                evtPlayFlyItemGroupComplete.itemId = itemId;
//                SendGameEvent(evtPlayFlyItemGroupComplete);
//            }
//            return;
//        }

//        // TODO：飞的效果
//    }

//    private GameObject CreateAndSetFlyItem(int itemId, GameObject prefab, Transform parentRoot, Vector3 fromLocalPos)
//    {
//        var itemCfg = ConfigManager.Instance.GetConfig<TItem>(itemId);
//        var flyItemGo = GameObject.Instantiate(prefab);
//        flyItemGo.gameObject.SetActive(true);
//        flyItemGo.transform.SetParent(parentRoot, false);
//        flyItemGo.transform.localPosition = fromLocalPos;
//        GameUtil.SetImage(flyItemGo.transform.Find("img_ItemIcon").GetComponent<Image>(), (int)itemCfg.CommonProp.FileIcon, flyItemGo.transform);
//        return flyItemGo;
//    }

//    private Vector3 GetRandomPos(Vector3 localPos)
//    {
//        int offset = 100;
//        float randomX = Random.Range(-offset, offset);
//        float randomY = Random.Range(-offset, offset);
//        float targetPosX = localPos.x + randomX;
//        float targetPosY = localPos.y + randomY;
//        Vector3 pos = new Vector3(targetPosX, targetPosY, 0);
//        return pos;
//    }

//    public override void OnDestroy()
//    {
//        base.OnDestroy();

//        GameUtil.UnRegEvent(EGameEventType.EnqueueFlyRewardData, HandleEnqueueFlyRewardData);
//        GameUtil.UnRegEvent(EGameEventType.FlyRewardGroupComplete, PlayFlyRewardGroupComplete);
//        GameUtil.UnRegEvent(EGameEventType.ChangeSceneComplete, HandleChangeSceneComplete);
//        flyRewardDataQueue.Clear();
//    }

//    #region 飞奖励数据相关

//    private Queue<List<FlyRewardData>> flyRewardDataQueue = new();
//    private bool flyRewardGroupIsInShow;//是否有飞奖励组在展示中

//    private void HandleEnqueueFlyRewardData(GameEventData data)
//    {
//        var evt = data as EvtEnqueueFlyRewardData;
//        if (evt == null)
//            return;
//        if (!GameUtil.AllSame(evt.itemIdList.Count, evt.showCountList.Count, evt.fromWorldPosList.Count, evt.realCountList.Count))
//        {
//            Log.Error("飞奖励数据不匹配！！！！！");
//            ExecuteOnComplete(evt.onCompleteList);
//            return;
//        }
//        var flyPanel = UIManager.Instance.Find<UI_Win_FlyReward>(UIPanelName.UIFlyPanel);
//        Camera worldCamera = GetWorldCamera();
//        if (flyPanel == null || worldCamera == null)
//        {
//            ExecuteOnComplete(evt.onCompleteList);
//            return;
//        }

//        // 将本次所有飞奖励数据添加到列表中
//        List<FlyRewardData> tempRewardDatas = new();
//        for (int i = 0; i < evt.itemIdList.Count; i++)
//        {
//            Vector3 fromLocalPos = CTUtils.World2UILocal(evt.fromWorldPosList[i], flyPanel.transform as RectTransform, worldCamera, UIManager.Instance.root.camera);
//            Transform targetTrans = GetTargetTrans(evt.itemIdList[i]);
//            // 找不到目标位置的没有飞奖励效果
//            if (targetTrans == null)
//            {
//                ExecuteOnComplete(evt.onCompleteList, i);
//                continue;
//            }
//            Vector3 toLocalPos = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(targetTrans.transform.position, UIManager.Instance.root.camera), flyPanel.transform as RectTransform, UIManager.Instance.root.camera);
//            FlyRewardData flyItemData = new FlyRewardData();
//            flyItemData.Init(evt.itemIdList[i], targetTrans, fromLocalPos, toLocalPos, evt.showCountList[i], evt.onCompleteList.Count <= i ? null : evt.onCompleteList[i], evt.realCountList[i]);
//            tempRewardDatas.Add(flyItemData);
//        }
//        // 根据优先级添加到飞奖励的队列中
//        var dict = CalcCurDatas(tempRewardDatas);
//        foreach (var v in dict.Values)
//        {
//            flyRewardDataQueue.Enqueue(v);
//        }

//        TrySpawnFlyReward();
//    }

//    private void PlayFlyRewardGroupComplete(GameEventData data)
//    {
//        var evt = data as EvtFlyRewardGroupComplete;
//        if (evt == null)
//            return;
//        flyRewardGroupIsInShow = false;
//        TrySpawnFlyReward();
//    }

//    private void HandleChangeSceneComplete(GameEventData data)
//    {
//        var evt = data as EvtChangeSceneComplete;
//        if (evt == null)
//        {
//            foreach (var dataList in flyRewardDataQueue)
//            {
//                foreach (var flyItemData in dataList)
//                {
//                    flyItemData.onComplete?.Invoke();
//                }
//            }
//            flyRewardDataQueue.Clear();
//            flyRewardGroupIsInShow = false;
//        }
//    }

//    private void TrySpawnFlyReward()
//    {
//        if (flyRewardGroupIsInShow)
//            return;
//        if (flyRewardDataQueue.Count == 0)
//            return;
//        var flyItemDatas = flyRewardDataQueue.Dequeue();
//        flyRewardGroupIsInShow = true;
//        for (int i = 0; i < flyItemDatas.Count; i++)
//        {
//            PlayFlyItem(flyItemDatas[i].itemId, FlyItem, flyItemDatas[i].targetTrans, flyItemDatas[i].fromLocalPos, flyItemDatas[i].toLocalPos, flyItemDatas[i].showCount, flyItemDatas[i].onComplete,
//                i == flyItemDatas.Count - 1, flyItemDatas[i].realCount);
//        }
//    }

//    private Transform GetTargetTrans(int itemId)
//    {
//        Transform targetTrans = null;
//        return targetTrans;
//    }

//    private Camera GetWorldCamera()
//    {
//        Camera worldCamera = null;
//        return worldCamera;
//    }

//    /// <summary>
//    /// 计算本次飞奖励的数据
//    /// </summary>
//    private Dictionary<int, List<FlyRewardData>> CalcCurDatas(List<FlyRewardData> flyItemDatas)
//    {
//        flyItemDatas.Sort(Sort);
//        Dictionary<int, List<FlyRewardData>> flyItemDataDict = new();
//        foreach (var flyItemData in flyItemDatas)
//        {
//            var itemCfg = ConfigManager.Instance.GetConfig<TItem>(flyItemData.itemId);
//            if (itemCfg == null)
//                continue;
//            if (!flyItemDataDict.TryGetValue(itemCfg.CommonProp.FlyOrder, out var outList))
//            {
//                outList = new List<FlyRewardData>();
//                flyItemDataDict.Add(itemCfg.CommonProp.FlyOrder, outList);
//            }
//            outList.Add(flyItemData);
//        }
//        return flyItemDataDict;
//    }

//    private int Sort(FlyRewardData data1, FlyRewardData data2)
//    {
//        if (data1 == null || data2 == null)
//            return 0;
//        var itemCfg1 = ConfigManager.Instance.GetConfig<TItem>(data1.itemId);
//        var itemCfg2 = ConfigManager.Instance.GetConfig<TItem>(data2.itemId);
//        if (itemCfg1 == null || itemCfg2 == null)
//            return 0;
//        int ret = itemCfg1.CommonProp.FlyOrder.CompareTo(itemCfg2.CommonProp.FlyOrder);
//        return ret;
//    }

//    private void ExecuteOnComplete(List<Action> onCompleteList, int index = -1)
//    {
//        if (onCompleteList == null)
//            return;
//        for (int i = 0; i < onCompleteList.Count; i++)
//        {
//            if (index == -1)
//            {
//                onCompleteList[i]?.Invoke();
//            }
//            else
//            {
//                if (index == i)
//                {
//                    onCompleteList[i].Invoke();
//                }
//            }
//        }
//    }

//    #endregion 飞奖励数据相关
//}
