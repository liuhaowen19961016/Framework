using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class UILayer
    {
        private GameObject layerGo;
        public GameObject LayerGo => layerGo;

        private EUILayerType layerType;
        public EUILayerType LayerType => layerType;

        private List<UIViewBase> uiViewList; //当前层级下的所有界面

        public void Init(GameObject layerGo, EUILayerType layerType)
        {
            this.layerGo = layerGo;
            this.layerType = layerType;
            uiViewList = new List<UIViewBase>();
        }

        public void AddView(UIViewBase view)
        {
            uiViewList.Add(view);
            SetAllViewOrderInLayer();
        }

        public void RemoveView(UIViewBase view)
        {
            uiViewList.Remove(view);
            SetAllViewOrderInLayer();
        }

        /// <summary>
        /// 设置当前层级下所有界面的OrderInLayer
        /// </summary>
        private void SetAllViewOrderInLayer()
        {
            int originOrderInLayer = (int)layerType * UIMgr.LAYER_ORDERINLAYER;
            for (int i = 0; i < uiViewList.Count; i++)
            {
                uiViewList[i].OrderInLayer = originOrderInLayer;
                originOrderInLayer += UIMgr.VIEW_ORDERINLAYER;
            }
        }

        /// <summary>
        /// 查找当前层级下的最顶部界面
        /// </summary>
        public UIViewBase GetTopView()
        {
            if (uiViewList.Count <= 0)
                return null;
            return uiViewList[0];
        }
    }
}