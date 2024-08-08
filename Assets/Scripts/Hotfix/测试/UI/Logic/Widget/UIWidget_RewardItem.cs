using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIWidget_RewardItem : UIWidget_RewardItemBase
{
    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("UIWidget_RewardItem OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("UIWidget_RewardItem OnDestroy");
    }
}
