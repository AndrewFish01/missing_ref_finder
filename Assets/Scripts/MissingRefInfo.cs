using System.Collections.Generic;
using UnityEngine;

public class MissingRefInfo
{
    public Object Prefab;
    public List<FieldInfo> FieldInfos;

    public MissingRefInfo(Object prefab, List<FieldInfo> fieldInfos)
    {
        Prefab = prefab;
        FieldInfos = fieldInfos;
    }
}

public struct FieldInfo
{
    public Object Component;
    public string PropertyPath;
}