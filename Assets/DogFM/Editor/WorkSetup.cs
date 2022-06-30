using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Folder
{
    public string name;
    public string path;
    public Folder parent;
    public List<Folder> sub;

}

public abstract class BaseStructure
{

}

public class CommonStructure : BaseStructure
{
    static Folder plugins = new Folder() { name = "Plugins" };
    static Folder prefabs = new Folder() { name = "Prefabs" };
    static Folder resources = new Folder() { name = "Resources" };
    static Folder scripts = new Folder() { name = "Scripts" };
    static Folder streamingAssets = new Folder() { name = "StreamingAssets" };
    public static List<Folder> Folders()
    {
        return new List<Folder>()
        {
            plugins,
            prefabs,
            resources,
            scripts,
            streamingAssets,
        };
    }
}

public class WorkSetup : Editor
{
    const string basePath = "Assets";

    [MenuItem("CatFM/Setup", false, 0)]
    public static void SetUp()
    {
        foreach (Folder folderObject in CommonStructure.Folders())
        {
            if (!AssetDatabase.IsValidFolder(basePath + '/' + folderObject.name))
            {
                AssetDatabase.CreateFolder(basePath, folderObject.name);
                Debug.Log(string.Format("folder {0} created successful", folderObject.name));
            }
            else
            {
                Debug.LogWarning(string.Format("folder {0} is exists", folderObject.name));
            }
        }
    }
}
