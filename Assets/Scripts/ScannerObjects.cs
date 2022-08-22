using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class ScannerObjects : IDisposable
{
    public List<MissingRefInfo> ScanProject()
    {
        var GUIDs = AssetDatabase.FindAssets("", new []{"Assets"});
        var objects = new List<MissingRefInfo>();
        
        foreach (var id in GUIDs)
        {
            var path = AssetDatabase.GUIDToAssetPath(id);
            var obj = AssetDatabase.LoadMainAssetAtPath(path);
            List<FieldInfo> fieldsInfo;
            
            switch (obj)
            {
                case GameObject:
                {
                    var components = obj.GetComponentsInChildren<Component>() as Object[];
                    fieldsInfo = CheckComponents(components);
                    break;
                }
                default:
                    fieldsInfo = CheckSerializedProperty(obj);
                    break;
            }

            if (fieldsInfo.Count == 0) continue;
            objects.Add(new MissingRefInfo(obj, fieldsInfo));
        }

        return objects;
    }

    private List<FieldInfo> CheckComponents(Object[] objects)
    {
        var fields = new List<FieldInfo>();
        
        foreach (var item in objects)
        {
            var refsInfo = CheckSerializedProperty(item);
            fields.AddRange(refsInfo);
        }

        return fields;
    }

    private List<FieldInfo> CheckSerializedProperty(Object obj)
    {
        var fields = new List<FieldInfo>();
        
        using var serObj = new SerializedObject(obj);
        using var iter = serObj.GetIterator();
            
        while (iter.NextVisible(true))
        {
            if(iter.propertyType != SerializedPropertyType.ObjectReference) continue;
                
            if (iter.objectReferenceValue == null && iter.objectReferenceInstanceIDValue != 0)
            {
                fields.Add(new FieldInfo{Component = obj, PropertyPath = iter.propertyPath});
            }
        }

        return fields;
    }

    public void Dispose()
    {
        
    }
}