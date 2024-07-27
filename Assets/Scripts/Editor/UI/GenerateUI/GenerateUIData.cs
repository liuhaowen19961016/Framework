using System;
using System.Collections.Generic;
using UnityEngine;

public class GenUIData
{
    public GameObject prefab;
    public ClassData uiViewData;
    public Dictionary<string, ClassData> subViewDataDict = new Dictionary<string, ClassData>();
}

public class ClassData
{
    public string className;
    public Dictionary<string, FieldData> fieldDataDict = new Dictionary<string, FieldData>();
}

public class FieldData
{
    public string name;
    public Type type;
    public string path;
}