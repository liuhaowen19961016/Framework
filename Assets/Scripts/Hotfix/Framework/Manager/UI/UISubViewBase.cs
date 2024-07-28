using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI界面的子界面基类
    /// </summary>
    public abstract class UISubViewBase : UIBase
    {
        private string uiSubViewName;
        public string UISubViewName => uiSubViewName;

        public void InternalInit(string uiSubViewName, object viewData)
        {
            this.uiSubViewName = uiSubViewName;
            OnInit(viewData);
        }

        public void InternalCreate(GameObject go)
        {
            Transform uiSubViewTrans = GameUtils.FindTrans(go.transform, uiSubViewName);
            if (uiSubViewTrans == null)
            {
                Debug.LogError($"{go.name}下找不到{uiSubViewName}节点！！！！！");
            }
            else
            {
                this.go = uiSubViewTrans.gameObject;
            }
            OnCreate();
        }
    }
}