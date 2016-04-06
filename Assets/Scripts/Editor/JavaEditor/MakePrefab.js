/*#pragma strict
//Generate a prefab from a selection

@MenuItem ("Project Tools/ Make Prefab")
static function CreatePrefab()
{
    var selectedObjects : GameObject[] = Selection.gameObjects;
    for (var go : GameObject in selectedObjects)
    {
        var name : String = go.name;
        var localPath : String = "Assets/Prefabs/" + name + ".prefab";

        if(AssetDatabase.LoadAssetAtPath(localPath, GameObject))
        {
            if(EditorUtility.DisplayDialog("Caution", "Prefab already exists, Do you want to overwrite?", "Yes", "No"))
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

static function CreateNew(localPath : String, selectedObject : GameObject)
{
    var prefab : Object = PrefabUtility.CreateEmptyPrefab(localPath);
    PrefabUtility.ReplacePrefab(selectedObject, prefab);

    AssetDatabase.Refresh();

    DestroyImmediate(selectedObject);
    var clone : GameObject = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
}*/