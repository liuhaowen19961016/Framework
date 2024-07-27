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
}

public class FieldData
{
    public string name;
    public Type type;
    public string path;
}