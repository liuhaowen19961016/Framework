using UnityEngine;

/// <summary>
/// 颜色工具类
/// </summary>
public static class ColorUtils
{
    /// <summary>
    /// Color转Hex
    /// </summary>
    public static string Color2Hex(Color color, bool haveAlpha = true)
    {
        string hex;
        if (haveAlpha)
        {
            hex = ColorUtility.ToHtmlStringRGBA(color);
        }
        else
        {
            hex = ColorUtility.ToHtmlStringRGB(color);
        }
        return hex;
    }

    /// <summary>
    /// Hex转Color
    /// </summary>
    public static Color Hex2Color(string hex)
    {
        Color color;
        bool b = ColorUtility.TryParseHtmlString(hex, out color);
        if (!b)
        {
            Debug.LogError("转换失败，颜色格式有误");
        }
        return color;
    }

    /// <summary>
    /// Color转HSV
    /// </summary>
    public static void Color2HSV(Color color, out float h, out float s, out float v)
    {
        Color.RGBToHSV(color, out h, out s, out v);
    }

    /// <summary>
    /// Color转HSV
    /// </summary>
    public static Color HSV2Color(float h, float s, float v)
    {
        Color color = Color.HSVToRGB(h, s, v);
        return color;
    }
}