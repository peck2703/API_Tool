using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;

public class MakeFolder : Editor
{
    [MenuItem("Project Tools/Make Folders")]
    static void MakeFolders()
    {
        GenerateFolders();
    }

    static void GenerateFolders()
    {
        string projectPath = Application.dataPath + "/";
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
        Directory.CreateDirectory(projectPath + "Textures");

        AssetDatabase.Refresh();
    }
}
