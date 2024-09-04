using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewConfig : ConfigBase
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// RESOURCE表ID
    /// </summary>
    public int ResourceId { get; set; }

    /// <summary>
    /// todo 路径（临时用的）
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// UI所属层级
    /// </summary>
    public int LayerType { get; set; }

    /// <summary>
    /// 界面类型
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 打开时是否关闭之前的界面
    /// </summary>
    public bool ClosePre { get; set; }

    /// <summary>
    /// 能否显示多个
    /// </summary>
    public bool DisplayMultiple;
    
    /// <summary>
    /// 注释
    /// </summary>
    public string Comment { get; set; }
}