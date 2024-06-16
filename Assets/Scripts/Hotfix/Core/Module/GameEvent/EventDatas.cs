using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    #region UI

    #region 飞物体效果

    /// <summary>
    /// 添加飞物体数据
    /// </summary>
    public class EvtAddFlyObjData : EventBase
    {
        public List<EFlyObjType> flyObjTypes = new();
        public List<int> logicIdList = new();
        public List<Vector3> fromScreenPosList = new();
        public List<Vector3> toScreenPosList = new();
        public List<int> showCountList = new(); //显示的物体数量
        public List<int> realCountList = new(); //真实获取的物体数量

        public List<Action> onSpawnFlyObjItemOverList = new();
        public List<Action> onCompleteList = new();
        public List<EFlyObjTag> tagList = new();

        public EvtAddFlyObjData()
        {
            EventType = EEventType.AddFlyObjData;
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            flyObjTypes.Clear();
            logicIdList.Clear();
            fromScreenPosList.Clear();
            toScreenPosList.Clear();
            showCountList.Clear();
            onCompleteList.Clear();
            realCountList.Clear();
            onSpawnFlyObjItemOverList.Clear();
            tagList.Clear();
        }
    }

    /// <summary>
    /// 本组飞物体完成
    /// </summary>
    public class EvtFlyObjGroupComplete : EventBase
    {
        public EFlyObjType flyObjType;
        public EFlyObjTag flyObjTag;
        public int logicId;

        public EvtFlyObjGroupComplete()
        {
            EventType = EEventType.FlyObjGroupComplete;
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            logicId = 0;
        }
    }

    /// <summary>
    /// 单个飞物体完成
    /// </summary>
    public class EvtFlyObjSingleComplete : EventBase
    {
        public EFlyObjType flyObjType;
        public EFlyObjTag flyObjTag;
        public int logicId;
        public float addRealCount;

        public EvtFlyObjSingleComplete()
        {
            EventType = EEventType.FlyObjSingleComplete;
        }

        public override void OnRecycle()
        {
            base.OnRecycle();
            logicId = 0;
            addRealCount = 0;
        }
    }

    #endregion 飞物体效果

    #endregion UI
}