using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDirectoryConfig : ConfigBase
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
        
    /// <summary>
    /// 路径
    /// </summary>
    public string DirPath { get; set; }
        
    /// <summary>
    /// 注释
    /// </summary>
    public string Comment { get; set; }
}
