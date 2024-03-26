using UnityEngine.UI;
using UnityEngine;
using Hotfix;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Object = UnityEngine.Object;

/// <summary>
/// 飞物体类型
/// </summary>
public enum EFlyObjType
{
    Invaild,
    Item,               //道具
    Icon,               //图标
}

/// <summary>
/// 飞物体标签
/// </summary>
public enum EFlyObjTag
{
    Normal,                 //通用
}

///// <summary>
///// 每次飞物体的数据
///// </summary>
//public class FlyObjData
//{
//    public EFlyObjType flyObjType;
//    public int logicId;
//    public Vector3 fromLocalPos;
//    public Vector3 toLocalPos;
//    public int showCount;
//    public int realCount;

//    public Action onSpawnFlyObjItemOver;
//    public Action onComplete;
//    public EFlyObjTag flyObjTag;

//    public int SortIndex
//    {
//        get
//        {
//            int ret = 0;
//            switch (flyObjType)
//            {
//                case EFlyObjType.Item:
//                    var itemCfg = GameGlobal.ConfigManager.GetConfig<TItem>(logicId);
//                    if (itemCfg != null)
//                    {
//                        ret = itemCfg.CommonProp.FlyOrder;
//                    }
//                    break;
//                case EFlyObjType.Icon:
//                    break;
//                default:
//                    break;
//            }
//            return ret;
//        }
//    }

//    public void Init(EFlyObjType flyObjType, int logicId, Vector3 fromLocalPos, Vector3 toLocalPos, int showCount, int realCount,
//        Action onSpawnOver = null, Action onComplete = null, EFlyObjTag flyObjTag = EFlyObjTag.Normal)
//    {
//        this.flyObjType = flyObjType;
//        this.logicId = logicId;
//        this.fromLocalPos = fromLocalPos;
//        this.toLocalPos = toLocalPos;
//        this.showCount = showCount;
//        this.realCount = realCount;
//        this.onComplete = onComplete;
//        this.onSpawnFlyObjItemOver = onSpawnOver;
//        this.flyObjTag = flyObjTag;
//    }
//}

///// <summary>
///// 飞物体面板
///// </summary>
//public partial class UI_Win_FlyObj : UIPanelBase
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
//        GameUtil.RegEvent(EGameEventType.AddFlyObjData, HandleAddFlyObjData);
//        GameUtil.RegEvent(EGameEventType.FlyObjGroupComplete, HandlePlayFlyObjGroupComplete);
//        FlyItem.gameObject.SetActive(false);
//    }

//    private void PlayFlyObj(FlyObjData flyObjData, bool isLastInGroup)
//    {
//        int iconId = -1;
//        switch (flyObjData.flyObjType)
//        {
//            case EFlyObjType.Item:
//                var itemCfg = GameGlobal.ConfigManager.GetConfig<TItem>(flyObjData.logicId);
//                iconId = (int)itemCfg.CommonProp.FileIcon;
//                break;
//            case EFlyObjType.Icon:
//                iconId = flyObjData.logicId;
//                break;
//        }
//        if (iconId == -1)
//        {
//            flyObjData.onComplete?.Invoke();

//            var evtFlyObjSingleComplete = GameEventPool.CreateEvent<EvtFlyObjSingleComplete>();
//            evtFlyObjSingleComplete.flyObjType = flyObjData.flyObjType;
//            evtFlyObjSingleComplete.logicId = flyObjData.logicId;
//            evtFlyObjSingleComplete.addRealCount = flyObjData.realCount * 1f / flyObjData.showCount;
//            SendGameEvent(evtFlyObjSingleComplete);

//            var evtFlyObjGroupComplete = GameEventPool.CreateEvent<EvtFlyObjGroupComplete>();
//            evtFlyObjGroupComplete.flyObjType = flyObjData.flyObjType;
//            evtFlyObjGroupComplete.logicId = flyObjData.logicId;
//            SendGameEvent(evtFlyObjGroupComplete);

//            return;
//        }

//        //TODO：可扩展
//        switch (flyObjData.flyObjTag)
//        {
//            case EFlyObjTag.Normal:
//                switch (flyObjData.flyObjType)
//                {
//                    case EFlyObjType.Item:
//                        var itemCfg = GameGlobal.ConfigManager.GetConfig<TItem>(flyObjData.logicId);
//                        iconId = (int)itemCfg.CommonProp.FileIcon;
//                        if (itemCfg.CommonProp.Type == TItemType.ItKey                                                                                  //钥匙
//                            || itemCfg.CommonProp.Type == TItemType.ItBattlepassActionPoint                                                             //通行证活跃度
//                            || (itemCfg.CommonProp.Type == TItemType.ItNormal && itemCfg.CommonProp.SmallType == TItemSmallType.IstTechmaterial)        //天赋材料
//                            || (itemCfg.CommonProp.Type == TItemType.ItNormal && itemCfg.CommonProp.SmallType == TItemSmallType.IstRevivalcoin))        //复活币
//                        {
//                            Performance_NormalAsyncBezier(iconId, flyObjData, isLastInGroup,
//                                0.2f, 0.1f, 2, 1, 0.5f);
//                        }
//                        else if (itemCfg.Base.Id == 1226)//局内心
//                        {
//                            Performance_InGameHeart(flyObjData.logicId, iconId, flyObjData.fromLocalPos, flyObjData.toLocalPos, flyObjData.onComplete, flyObjData.flyObjType, flyObjData.realCount, flyObjData.showCount);
//                        }
//                        else if (itemCfg.Base.Id == 1229)//局内金币
//                        {
//                            Performance_InGameCoin(iconId, flyObjData.flyObjType, flyObjData.logicId, flyObjData.fromLocalPos, flyObjData.toLocalPos, flyObjData.showCount, flyObjData.onComplete, isLastInGroup, flyObjData.realCount,
//                                0.5f, 0.1f, 1.5f, 1.5f, 0.5f, flyObjData.onSpawnFlyObjItemOver);
//                        }
//                        else//其他
//                        {
//                            Performance_NormalAsyncStraight(iconId, flyObjData, isLastInGroup,
//                                0.2f, 0.1f, 2, 1, 0.5f);
//                        }
//                        break;
//                    case EFlyObjType.Icon:
//                        Performance_NormalAsyncStraight(iconId, flyObjData, isLastInGroup,
//                                  0.2f, 0.1f, 2, 1, 0.5f);
//                        break;
//                    default:
//                        break;
//                }
//                break;
//            case EFlyObjTag.NpcAngelAndDemon:
//                Performance_NormalAsyncBezier(iconId, flyObjData, isLastInGroup,
//                             0, 0.5f, 2, 4.35f, 1f);
//                break;
//            default:
//                break;
//        }
//    }

//    private GameObject CreateAndSetFlyObjGo(int iconId, Vector3 fromLocalPos)
//    {
//        var flyObjGo = GameObject.Instantiate(FlyItem);
//        flyObjGo.gameObject.SetActive(true);
//        flyObjGo.transform.SetParent(transform, false);
//        flyObjGo.transform.localPosition = fromLocalPos;
//        GameUtil.SetImage(flyObjGo.transform.Find("img_ItemIcon").GetComponent<Image>(), iconId, flyObjGo.transform);
//        return flyObjGo;
//    }

//    public override void OnDestroy()
//    {
//        base.OnDestroy();

//        GameUtil.UnRegEvent(EGameEventType.AddFlyObjData, HandleAddFlyObjData);
//        GameUtil.UnRegEvent(EGameEventType.FlyObjGroupComplete, HandlePlayFlyObjGroupComplete);
//        flyObjDataQueue.Clear();
//    }

//    #region 添加飞物体数据

//    private Queue<List<FlyObjData>> flyObjDataQueue = new();
//    private bool flyObjShowing;//飞物体是否在展示中

//    private void HandleAddFlyObjData(GameEventBase data)
//    {
//        var evt = data as EvtAddFlyObjData;
//        if (evt == null)
//            return;
//        if (!GameUtil.AllSame(evt.flyObjTypes.Count, evt.logicIdList.Count, evt.fromScreenPosList.Count, evt.toScreenPosList.Count, evt.showCountList.Count, evt.realCountList.Count))
//        {
//            KaLog.LogError("飞物体数据不匹配！！！！！");
//            ForceOnComplete(evt.onCompleteList);
//            return;
//        }

//        //将本次所有飞物体数据添加进队列
//        List<FlyObjData> tempList = new();
//        for (int i = 0; i < evt.logicIdList.Count; i++)
//        {
//            Vector3 fromLocalPos = ClientTools.Screen2UILocal(evt.fromScreenPosList[i], transform as RectTransform, GameGlobal.UIManager.root.camera);
//            Vector3 toLocalPos = ClientTools.Screen2UILocal(evt.toScreenPosList[i], transform as RectTransform, GameGlobal.UIManager.root.camera);
//            FlyObjData flyItemData = new FlyObjData();
//            flyItemData.Init(evt.flyObjTypes[i], evt.logicIdList[i], fromLocalPos, toLocalPos, evt.showCountList[i], evt.realCountList[i],
//                evt.onSpawnFlyObjItemOverList.Count <= i ? null : evt.onSpawnFlyObjItemOverList[i],
//                evt.onCompleteList.Count <= i ? null : evt.onCompleteList[i],
//                evt.tagList.Count <= i ? EFlyObjTag.Normal : evt.tagList[i]);
//            tempList.Add(flyItemData);
//        }
//        //排序
//        tempList.Sort((x, y) => x.SortIndex.CompareTo(y.SortIndex));
//        Dictionary<int, List<FlyObjData>> tempDict = new();
//        foreach (var flyObjData in tempList)
//        {
//            if (!tempDict.TryGetValue(flyObjData.SortIndex, out var v))
//            {
//                v = new List<FlyObjData>();
//                tempDict.Add(flyObjData.SortIndex, v);
//            }
//            v.Add(flyObjData);
//        }
//        //最终添加
//        foreach (var v in tempDict.Values)
//        {
//            flyObjDataQueue.Enqueue(v);
//        }

//        TrySpawnFlyReward();
//    }

//    private void TrySpawnFlyReward()
//    {
//        if (flyObjDataQueue.Count == 0)
//            return;
//        if (flyObjShowing)
//            return;
//        var flyItemDatas = flyObjDataQueue.Dequeue();
//        flyObjShowing = true;
//        for (int i = 0; i < flyItemDatas.Count; i++)
//        {
//            PlayFlyObj(flyItemDatas[i], i == flyItemDatas.Count - 1);
//        }
//    }

//    #endregion 添加飞物体数据

//    #region private fun

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

//    private void ForceOnComplete(List<Action> onCompleteList, int index = -1)
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

//    #endregion private fun

//    #region Callback

//    private void HandlePlayFlyObjGroupComplete(GameEventBase data)
//    {
//        var evt = data as EvtFlyObjGroupComplete;
//        if (evt == null)
//            return;
//        flyObjShowing = false;
//        TrySpawnFlyReward();
//    }

//    #endregion Callback

//    #region 动画表现

//    /// <summary>
//    /// 通用依次飞 直线
//    /// </summary>
//    private void Performance_NormalAsyncStraight(int iconId, FlyObjData flyObjData, bool isLastInGroup,
//        float spawnTime, float moveTimeInterval, float initScaleRatio, float targetScaleRatio, float moveAniTime)
//    {
//        List<GameObject> itemList = new();
//        for (int i = 0; i < flyObjData.showCount; i++)
//        {
//            GameObject flyItemGo = CreateAndSetFlyObjGo(iconId, flyObjData.fromLocalPos);
//            itemList.Add(flyItemGo);
//            flyItemGo.transform.DOScale(Vector3.one * initScaleRatio, spawnTime);
//            flyItemGo.transform.DOLocalMove(GetRandomPos(flyObjData.fromLocalPos), spawnTime);
//        }
//        GameGlobal.Timer.AddTimer(spawnTime, true, callback: () =>
//        {
//            float tempTimer = 0;
//            for (int i = 0; i < itemList.Count; i++)
//            {
//                int tempI = i;
//                GameGlobal.Timer.AddTimer(tempTimer, true, () =>
//                {
//                    Sequence sequence = DOTween.Sequence();
//                    sequence.Insert(0, itemList[tempI].transform.DOLocalMove(flyObjData.toLocalPos, moveAniTime).SetEase(Ease.InSine));
//                    sequence.Insert(0, itemList[tempI].transform.DOScale(Vector3.one * targetScaleRatio, moveAniTime));
//                    sequence.SetUpdate(true);
//                    sequence.OnComplete(() =>
//                    {
//                        if (tempI == itemList.Count - 1)
//                        {
//                            flyObjData.onComplete?.Invoke();
//                        }

//                        var evtFlyObjSingleComplete = GameEventPool.CreateEvent<EvtFlyObjSingleComplete>();
//                        evtFlyObjSingleComplete.flyObjType = flyObjData.flyObjType;
//                        evtFlyObjSingleComplete.logicId = flyObjData.logicId;
//                        evtFlyObjSingleComplete.flyObjTag = flyObjData.flyObjTag;
//                        evtFlyObjSingleComplete.addRealCount = flyObjData.realCount / (float)flyObjData.showCount;
//                        SendGameEvent(evtFlyObjSingleComplete);

//                        if (tempI == itemList.Count - 1 && isLastInGroup)
//                        {
//                            var evtFlyObjGroupComplete = GameEventPool.CreateEvent<EvtFlyObjGroupComplete>();
//                            evtFlyObjGroupComplete.flyObjType = flyObjData.flyObjType;
//                            evtFlyObjGroupComplete.logicId = flyObjData.logicId;
//                            evtFlyObjGroupComplete.flyObjTag = flyObjData.flyObjTag;
//                            SendGameEvent(evtFlyObjGroupComplete);
//                        }

//                        Object.Destroy(itemList[tempI]);
//                    });
//                });
//                tempTimer += moveTimeInterval;
//            }
//        }, ignoreTimeScale: true);
//    }

//    /// <summary>
//    /// 通用依次飞 贝塞尔
//    /// </summary>
//    private void Performance_NormalAsyncBezier(int iconId, FlyObjData flyObjData, bool isLastInGroup,
//        float spawnTime, float moveTimeInterval, float initScaleRatio, float targetScaleRatio, float moveAniTime)
//    {
//        List<GameObject> itemList = new();
//        for (int i = 0; i < flyObjData.showCount; i++)
//        {
//            GameObject flyItemGo = CreateAndSetFlyObjGo(iconId, flyObjData.fromLocalPos);
//            itemList.Add(flyItemGo);
//            flyItemGo.transform.DOScale(Vector3.one * initScaleRatio, spawnTime);
//            flyItemGo.transform.DOLocalMove(GetRandomPos(flyObjData.fromLocalPos), spawnTime);
//        }
//        GameGlobal.Timer.AddTimer(spawnTime, true, () =>
//        {
//            float timer = 0;
//            for (int i = 0; i < itemList.Count; i++)
//            {
//                int tempI = i;
//                GameGlobal.Timer.AddTimer(timer, true, () =>
//                {
//                    List<Vector3> posList = new();
//                    int segement = 111;
//                    float offsetX = GameGlobal.UIManager.root.canvas.GetComponent<RectTransform>().rect.width / 4;
//                    Vector3 controlPos = new Vector3(flyObjData.fromLocalPos.x >= flyObjData.toLocalPos.x
//                        ? (flyObjData.fromLocalPos.x + flyObjData.toLocalPos.x) / 2 - offsetX
//                        : (flyObjData.fromLocalPos.x + flyObjData.toLocalPos.x) / 2 + offsetX, (flyObjData.fromLocalPos.y + flyObjData.toLocalPos.y) / 2, 0);
//                    for (int j = 0; j < segement; j++)
//                    {
//                        float t = j / (float)segement;
//                        posList.Add(BezierUtil.BezierCurve(flyObjData.fromLocalPos, controlPos, flyObjData.toLocalPos, t));
//                    }
//                    Sequence sequence = DOTween.Sequence();
//                    sequence.Insert(0, itemList[tempI].transform.DOLocalPath(posList.ToArray(), moveAniTime).SetEase(Ease.InSine));
//                    sequence.Insert(0, itemList[tempI].transform.DOScale(Vector3.one * targetScaleRatio, moveAniTime));
//                    sequence.SetUpdate(true);
//                    sequence.OnComplete(() =>
//                    {
//                        if (tempI == itemList.Count - 1)
//                        {
//                            flyObjData.onComplete?.Invoke();
//                        }

//                        var evtFlyObjSingleComplete = GameEventPool.CreateEvent<EvtFlyObjSingleComplete>();
//                        evtFlyObjSingleComplete.flyObjType = flyObjData.flyObjType;
//                        evtFlyObjSingleComplete.logicId = flyObjData.logicId;
//                        evtFlyObjSingleComplete.flyObjTag = flyObjData.flyObjTag;
//                        evtFlyObjSingleComplete.addRealCount = flyObjData.realCount / (float)flyObjData.showCount;
//                        SendGameEvent(evtFlyObjSingleComplete);

//                        if (tempI == itemList.Count - 1 && isLastInGroup)
//                        {
//                            var evtFlyObjGroupComplete = GameEventPool.CreateEvent<EvtFlyObjGroupComplete>();
//                            evtFlyObjGroupComplete.flyObjType = flyObjData.flyObjType;
//                            evtFlyObjGroupComplete.logicId = flyObjData.logicId;
//                            evtFlyObjGroupComplete.flyObjTag = flyObjData.flyObjTag;
//                            SendGameEvent(evtFlyObjGroupComplete);
//                        }

//                        Object.Destroy(itemList[tempI]);
//                        //PlayEffect(1037, targetTrans, needRecycleOnPlayComplete: true);
//                    });
//                });
//                timer += moveTimeInterval;
//            }
//        });
//    }

//    #endregion 动画表现
//}
