using UnityEngine;
using UnityEditor;
using System.Collections;

public class MakePrefab : Editor
{
    [MenuItem("Project Tools/Make Prefab")]
    static void CreatePrefab()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach(var go in selectedObjects)
        {
            string name = go.name;
            string localPath = "Assets/Prefabs/" + name + ".prefab";

            if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
            {
                if(EditorUtility.DisplayDialog("Caution", "Prefab already exists, Do you want to overwrite?","Yes","No"))
                {
                    CreateNew(localPath, go);
                }
            }
            else
            {
                CreateNew(localPath, go);
            }
        }
    }
    static void CreateNew(string localPath, GameObject selectedObject)
    {
        Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
        PrefabUtility.ReplacePrefab(selectedObject, prefab);

        AssetDatabase.Refresh();

        DestroyImmediate(selectedObject);
        GameObject clone = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
    }
}
