using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 提示框方向类型
/// </summary>
public enum ETooltipDirType
{
    Up,
    Down,
    Left,
    Right,
    Auto,
}

/// <summary>
/// 提示框界面基类
/// </summary>
public class UIWin_BaseTooltip : MonoBehaviour
{
    //public class ViewData
    //{
    //    public Vector2 pos;//位置（世界坐标）
    //    public RectTransform showRect;//显示区域
    //    public ETooltipDirType tooltipDirType = ETooltipDirType.Auto;//方向类型
    //    public float offset;//偏移量
    //    public bool showArrow = true;//是否显示箭头
    //    public bool clickAnyClose = true;//是否点击任意位置关闭
    //    public bool clickSelfClose = true;//是否点击自身关闭
    //    public Vector2 margin = new Vector2(60, 40);//页面边距
    //}

    //private const UILayerId UILAYERID = UILayerId.Top;

    //private ViewData viewData;
    //private Camera uiCamera;
    //private RectTransform showRect;
    //protected RectTransform tooltipRect;
    //protected RectTransform arrowRect;

    //private float[] showRectCorners = new float[4];//显示区域边界（上下左右）

    //public override void InitializeParams()
    //{
    //    base.InitializeParams();
    //    layerId = UILAYERID;
    //    NeedBackToLastUI = false;
    //}

    //public override void onCreate()
    //{
    //    base.onCreate();
    //    if (_viewData == null || _viewData.Length <= 0)
    //        return;
    //    viewData = _viewData[0] as ViewData;
    //    if (viewData == null)
    //        return;
    //    InitData();
    //}

    //public override void onShow(object[] objs)
    //{
    //    base.onShow(objs);
    //    if (viewData == null)
    //    {
    //        Hide();
    //        return;
    //    }

    //    SetView();
    //    if (tooltipRect == null)
    //    {
    //        Log.Error($"每个提示框子类都需要重写SetView方法设置tooltipRect");
    //        Hide();
    //        return;
    //    }
    //    tooltipRect.transform.localPosition = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(viewData.pos, uiCamera), showRect, uiCamera);
    //    CalcData();
    //    Adjust();
    //}

    //public override void onUpdate()
    //{
    //    base.onUpdate();
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (viewData.clickAnyClose)
    //        {
    //            if (viewData.clickSelfClose)
    //            {
    //                Hide();
    //            }
    //            else
    //            {
    //                bool clickSelf = false;
    //                PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
    //                pointerEventData.position = Input.mousePosition;
    //                List<RaycastResult> raycastResultCache = new List<RaycastResult>();
    //                EventSystem.current.RaycastAll(pointerEventData, raycastResultCache);
    //                foreach (var raycastResult in raycastResultCache)
    //                {
    //                    if (raycastResult.gameObject.transform.IsChildOf(gameObject.transform))
    //                    {
    //                        clickSelf = true;
    //                        break;
    //                    }
    //                }
    //                if (!clickSelf)
    //                {
    //                    Hide();
    //                }
    //            }
    //        }
    //    }
    //}

    //private void InitData()
    //{
    //    uiCamera = UIManager.Instance.root.camera;
    //    showRect = viewData.showRect == null ? UIManager.Instance.GetLayerRoot(UILAYERID) : viewData.showRect;
    //    Vector3[] tempArray = new Vector3[4];
    //    showRect.GetWorldCorners(tempArray);
    //    showRectCorners[0] = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tempArray[1], uiCamera), showRect, uiCamera).y;
    //    showRectCorners[1] = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tempArray[0], uiCamera), showRect, uiCamera).y;
    //    showRectCorners[2] = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tempArray[0], uiCamera), showRect, uiCamera).x;
    //    showRectCorners[3] = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tempArray[3], uiCamera), showRect, uiCamera).x;
    //}

    //private void CalcData()
    //{
    //    //计算方向类型
    //    if (viewData.tooltipDirType == ETooltipDirType.Auto)
    //    {
    //        Vector2 localPosInShowRect = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tooltipRect.transform.position, uiCamera), showRect, uiCamera);
    //        float distToUp = showRectCorners[0] - (localPosInShowRect.y + tooltipRect.rect.height / 2);
    //        float distToDown = (localPosInShowRect.y - tooltipRect.rect.height / 2) - showRectCorners[1];
    //        float distToLeft = (localPosInShowRect.x - tooltipRect.rect.width / 2) - showRectCorners[2];
    //        float distToRight = showRectCorners[3] - (localPosInShowRect.x + tooltipRect.rect.width / 2);
    //        List<float> tempList = new List<float>() { distToUp, distToDown, distToLeft, distToRight, };
    //        int tooltipDirTypeIndex = tempList.IndexOf(tempList.Max());
    //        viewData.tooltipDirType = (ETooltipDirType)tooltipDirTypeIndex;
    //    }
    //}

    //private void Adjust()
    //{
    //    float goRectOffset = viewData.offset;//整体偏移
    //    float arrowOffset = 0;//箭头偏移
    //    bool showArrow = arrowRect != null && viewData.showArrow;
    //    switch (viewData.tooltipDirType)
    //    {
    //        case ETooltipDirType.Up:
    //            goRectOffset += tooltipRect.rect.height / 2;
    //            if (showArrow)
    //            {
    //                goRectOffset += arrowRect.rect.height / 2;
    //                arrowOffset += tooltipRect.rect.height / 2;
    //                arrowOffset += arrowRect.rect.height / 2;
    //                arrowRect.localPosition += Vector3.down * arrowOffset;
    //                arrowRect.eulerAngles = new Vector3(0, 0, 0);
    //            }
    //            tooltipRect.localPosition += Vector3.up * goRectOffset;
    //            break;
    //        case ETooltipDirType.Down:
    //            goRectOffset += tooltipRect.rect.height / 2;
    //            if (showArrow)
    //            {
    //                goRectOffset += arrowRect.rect.height / 2;
    //                arrowOffset += tooltipRect.rect.height / 2;
    //                arrowOffset += arrowRect.rect.height / 2;
    //                arrowRect.localPosition += Vector3.up * arrowOffset;
    //                arrowRect.eulerAngles = new Vector3(0, 0, 180);
    //            }
    //            tooltipRect.localPosition += Vector3.down * goRectOffset;
    //            break;
    //        case ETooltipDirType.Left:
    //            goRectOffset += tooltipRect.rect.width / 2;
    //            if (showArrow)
    //            {
    //                goRectOffset += arrowRect.rect.width / 2;
    //                arrowOffset += tooltipRect.rect.width / 2;
    //                arrowOffset += arrowRect.rect.width / 2;
    //                arrowRect.localPosition += Vector3.right * arrowOffset;
    //                arrowRect.eulerAngles = new Vector3(0, 0, 90);
    //            }
    //            tooltipRect.localPosition += Vector3.left * goRectOffset;
    //            break;
    //        case ETooltipDirType.Right:
    //            goRectOffset += tooltipRect.rect.width / 2;
    //            if (showArrow)
    //            {
    //                goRectOffset += arrowRect.rect.width / 2;
    //                arrowOffset += tooltipRect.rect.width / 2;
    //                arrowOffset += arrowRect.rect.width / 2;
    //                arrowRect.localPosition += Vector3.left * arrowOffset;
    //                arrowRect.eulerAngles = new Vector3(0, 0, -90);
    //            }
    //            tooltipRect.localPosition += Vector3.right * goRectOffset;
    //            break;
    //        default:
    //            break;
    //    }

    //    ClampInShowRect();
    //}

    //private void ClampInShowRect()
    //{
    //    Vector3[] tooltipRectCorners = new Vector3[4];
    //    tooltipRect.GetWorldCorners(tooltipRectCorners);
    //    if (viewData.tooltipDirType == ETooltipDirType.Up
    //        || viewData.tooltipDirType == ETooltipDirType.Down)
    //    {
    //        float tooltipRectCornerRight = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tooltipRectCorners[3], uiCamera), showRect, uiCamera).x;
    //        float tooltipRectCornerLeft = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tooltipRectCorners[0], uiCamera), showRect, uiCamera).x;
    //        if (tooltipRectCornerRight > showRectCorners[3])
    //        {
    //            float offset = tooltipRectCornerRight - showRectCorners[3];
    //            tooltipRect.localPosition += Vector3.left * offset;
    //            arrowRect.transform.localPosition += Vector3.right * offset;
    //        }
    //        else if (tooltipRectCornerLeft < showRectCorners[2])
    //        {
    //            float offset = showRectCorners[2] - tooltipRectCornerLeft;
    //            tooltipRect.localPosition += Vector3.right * offset;
    //            arrowRect.transform.localPosition += Vector3.left * offset;
    //        }
    //    }
    //    else if (viewData.tooltipDirType == ETooltipDirType.Left
    //        || viewData.tooltipDirType == ETooltipDirType.Right)
    //    {
    //        float tooltipRectCornerUp = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tooltipRectCorners[1], uiCamera), showRect, uiCamera).y;
    //        float tooltipRectCornerDown = CTUtils.Screen2UILocal(CTUtils.UIWorld2Screen(tooltipRectCorners[0], uiCamera), showRect, uiCamera).y;
    //        if (tooltipRectCornerUp > showRectCorners[0])
    //        {
    //            float offset = tooltipRectCornerUp - showRectCorners[0];
    //            tooltipRect.localPosition += Vector3.down * offset;
    //            arrowRect.transform.localPosition += Vector3.up * offset;
    //        }
    //        else if (tooltipRectCornerDown < showRectCorners[1])
    //        {
    //            float offset = showRectCorners[1] - tooltipRectCornerDown;
    //            tooltipRect.localPosition += Vector3.up * offset;
    //            arrowRect.transform.localPosition += Vector3.down * offset;
    //        }
    //    }
    //}

    ///// <summary>
    ///// 设置界面
    ///// </summary>
    ///// 子物体必须重写，需要根据尺寸动态调整位置！！！！！
    //public virtual void SetView()
    //{
    //    //界面显示
    //    //设置m_GoRect物体
    //    //设置m_GoRect物体尺寸
    //    //设置箭头物体（可以没有）
    //}
}