using System;
using System.Collections.Generic;
using UnityEngine;

#region UI

#region 飞奖励

/// <summary>
/// 添加飞奖励数据
/// </summary>
public class EvtEnqueueFlyRewardData : GameEventDataBase
{
    public List<int> itemIdList = new();
    public List<Vector3> fromWorldPosList = new();
    public List<int> showCountList = new();
    public List<Action> onCompleteList = new();
    public List<int> realCountList = new();

    public EvtEnqueueFlyRewardData()
    {
        gameEventType = EGameEventType.EnqueueFlyRewardData;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        itemIdList.Clear();
        fromWorldPosList.Clear();
        showCountList.Clear();
        onCompleteList.Clear();
        realCountList.Clear();
    }
}

/// <summary>
/// 本组飞奖励完成
/// </summary>
public class EvtFlyRewardGroupComplete : GameEventDataBase
{
    public int itemId;

    public EvtFlyRewardGroupComplete()
    {
        gameEventType = EGameEventType.FlyRewardGroupComplete;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        itemId = 0;
    }
}

/// <summary>
/// 单个飞奖励完成
/// </summary>
public class EvtFlyRewardSingleComplete : GameEventDataBase
{
    public int itemId;
    public float perRealCount;

    public EvtFlyRewardSingleComplete()
    {
        gameEventType = EGameEventType.FlyRewardSingleComplete;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        itemId = 0;
        perRealCount = 0;
    }
}

#endregion 飞奖励

#endregion UI