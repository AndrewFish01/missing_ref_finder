# Missing References Finder
A tool for finding missing references for objects such as GameObjects, ScriptableObject and etc. in a project (Assets).
The tool checks all files that are in the Assets folder.

## How to use
To launch the tool, go to the **CustomWindow->MissingReferencesFinder** section. Next, a window will open with a list of objects that have missing links. 
The parent object is displayed in the **"Objects"** column. 
In the **"Components** column, the type and name of the object in which the link is missing is displayed. 
The last column shows the full path of the property.

## How it works
There are several ways to search for files.

1. [AssetDatabase.LoadAllAssetsAtPath](https://docs.unity3d.com/ScriptReference/AssetDatabase.LoadAllAssetsAtPath.html)
2. [AssetDatabase.FindAssets](https://docs.unity3d.com/ScriptReference/AssetDatabase.FindAssets.html)
3. [Directory.GetFiles](https://docs.microsoft.com/ru-ru/dotnet/api/system.io.directory.getfiles?view=net-5.0)

I chose the second option because it can search for files by the specified path, filter files by type, etc. Also, in my opinion, it is easier to implement.

After searching for all the files, you need to get their serialized fields and check them for missing links. To do this, I used [SerializedObject](https://docs.unity3d.com/ScriptReference/SerializedObject.html) with which you can get [all the serialized fields](https://docs.unity3d.com/ScriptReference/SerializedObject.GetIterator.html) of an object and check them.
Two parameters **objectReferenceValue** and **objectReferenceInstanceIDValue** must be checked for a sterilized field.
