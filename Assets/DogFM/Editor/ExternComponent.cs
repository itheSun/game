using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExternComponent : Editor
{
    [MenuItem("GameObject/UI/OptionBar", false, 10)]
    public static void OptionBar(MenuCommand menuCommand)
    {
        GameObject optionBar = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Package/OptionBar", typeof(GameObject));
        GameObject go = Instantiate<GameObject>(optionBar);
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create" + go.name);
        Selection.activeGameObject = go;
    }
}
