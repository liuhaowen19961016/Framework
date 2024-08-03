using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GenUIData
{
    public GameObject prefab;
    public ClassData uiViewData;
    public Dictionary<string, ClassData> uiSubViewDataDict = new Dictionary<string, ClassData>();
    public StringBuilder errorStr = new StringBuilder();
}

public class ClassData
{
    public string className;
    public Dictionary<string, FieldData> fieldDataDict = new Dictionary<string, FieldData>();
    public List<string> namespaceList = new List<string>();
}

public class FieldData
{
    public EGenUIType genUIType;
    public string fieldName;
    public string fieldTypeName;
    public string fieldPath;
}

/// <summary>
/// 生成UI的结构类型
/// </summary>
public enum EGenUIStructureType
{
    View = 1,
    SubView,
    Container,
}

/// <summary>
/// 生成UI的类型
/// </summary>
public enum EGenUIType
{
    Common = 1, //通用的（UGUI组件等）
    SubView, //子界面
    Container,
}

/// <summary>
/// 生成UI的存档，防止逻辑界面覆盖生成
/// </summary>
public struct GenUIArchive
{
    public string className;
    public string filePath;
}