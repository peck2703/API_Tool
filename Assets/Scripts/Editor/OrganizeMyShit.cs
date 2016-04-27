using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;

public class OrganizeMyShit : Editor
{

    [MenuItem("Project Tools/Organize My Shit")]
    static void MakeFolders()
    {
        GenerateFolders();
        OrganizeFolders();
    }

    static void GenerateFolders()
    {
        /*string projectPath = Application.dataPath + "/";
        Directory.CreateDirectory(projectPath + "Audio");
        Directory.CreateDirectory(projectPath + "Characters");
        Directory.CreateDirectory(projectPath + "Fonts");
        Directory.CreateDirectory(projectPath + "Materials");
        Directory.CreateDirectory(projectPath + "Meshes");
        Directory.CreateDirectory(projectPath + "Prefabs");
        Directory.CreateDirectory(projectPath + "Resources");
        Directory.CreateDirectory(projectPath + "Scenes");
        Directory.CreateDirectory(projectPath + "Scripts");
        Directory.CreateDirectory(projectPath + "Shaders");
        Directory.CreateDirectory(projectPath + "Sprites");
        Directory.CreateDirectory(projectPath + "Textures");*/

        AssetDatabase.Refresh();
    }
    static void OrganizeFolders()
    {
        Debug.Log("Data path is: " + Application.dataPath.ToString());

        string localPath = Application.dataPath;

        DirectoryInfo dir = new DirectoryInfo(localPath);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            Debug.Log("Name is: " + f.Name);
            Debug.Log("Extension is: " + f.Extension);
            MoveAssetsIntoFolders(f.Name, f.Extension, f.FullName);
        }
    }
    static void MoveAssetsIntoFolders(string fileName, string extension, string oldPath)
    {
        string newPath;
        switch (extension)
        {
            //Audio Files - May need to include folders for ambiance and soundFX
            case ".wav":
            case ".wma":
            case ".aac":
                newPath = "Assets/Audio/";
                FileUtil.MoveFileOrDirectory(oldPath, newPath + fileName);
                AssetDatabase.Refresh();
                break;
            //Fonts - typically use .tff files but .tff files are also texture files
            case ".tff":
                newPath = "Assets/Fonts/";
                FileUtil.MoveFileOrDirectory(oldPath, newPath + fileName);
                AssetDatabase.Refresh();
                break;
            //Materials
            case ".mat":
                newPath = "Assets/Materials/";
                FileUtil.MoveFileOrDirectory(oldPath, newPath + fileName);
                AssetDatabase.Refresh();
                break;
            //Prefabs
            case ".prefab":
                newPath = "Assets/Prefabs/";
                FileUtil.MoveFileOrDirectory(oldPath, newPath + fileName);
                AssetDatabase.Refresh();
                break;
            //Scene - Levels
            case ".unity":
                newPath = "Assets/Scenes/";
                FileUtil.MoveFileOrDirectory(oldPath, newPath + fileName);
                AssetDatabase.Refresh();
                break;
            //C-Sharp Scripts - May need to include the type (MonoBehavior or Editor
            case ".cs":
                newPath = "Assets/Scripts/Runtime/";
                FileUtil.MoveFileOrDirectory(oldPath, newPath + fileName);
                AssetDatabase.Refresh();
                break;
        }
    }
}
