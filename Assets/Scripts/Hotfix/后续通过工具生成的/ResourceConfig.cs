using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceConfig : ConfigBase
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
        
    /// <summary>
    /// RESOURCEDIRECTORY表ID
    /// </summary>
    public int DirectoryId { get; set; }
        
    /// <summary>
    /// 资源路径
    /// </summary>
    public string ResourcePath { get; set; }
        
    /// <summary>
    /// 注释
    /// </summary>
    public string Comment { get; set; }
}
