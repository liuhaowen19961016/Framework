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
    public List<int> realCountList = new();

    public override void OnInit()
    {
        gameEventType = EGameEventType.EnqueueFlyRewardData;
    }

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
        realCountList.Clear();
    }
}

/// <summary>
/// 本组飞奖励完成
/// </summary>
public class EvtPlayFlyRewardGroupComplete : GameEventDataBase
{
    public int itemId;
    public int realCount;

    public override void OnInit()
    {
        gameEventType = EGameEventType.PlayFlyRewardGroupComplete;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        itemId = 0;
        realCount = 0;
    }
}

/// <summary>
/// 单类型飞奖励完成
/// </summary>
public class EvtPlayFlyRewardSingleTypeComplete : GameEventDataBase
{
    public int itemId;
    public int realCount;

    public override void OnInit()
    {
        gameEventType = EGameEventType.PlayFlyRewardSingleTypeComplete;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        itemId = 0;
        realCount = 0;
    }
}

/// <summary>
/// 单次飞奖励完成
/// </summary>
public class EvtPlayFlyRewardSingleOnceComplete : GameEventDataBase
{
    public int itemId;
    public float perRealCount;

    public override void OnInit()
    {
        gameEventType = EGameEventType.PlayFlyRewardSingleOnceComplete;
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