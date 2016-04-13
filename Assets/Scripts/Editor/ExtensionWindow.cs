using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExtensionWindow : EditorWindow
{
    ExtensionBank extBank;
    public static string[] standardExt =
    {
        ".3gpp",
        ".wav",
        ".wma",
        ".aac",
        ".tff",
        ".mat",
        ".prefabs",
        ".unity",
        ".cs"
    };
    string[] categories;
    string[] exts;
    public bool[] extToggles;


    public static Vector2 scrollPosition;
    private static int textFieldWidth = 200;
    private static int buttonWidth = 25;
    private static bool assetsOnlyReadOnly = true;
    private static bool extensionsImported = false;
    private static bool extensionsAdded = false;

    FileInfo sourceFile;
    private static StreamWriter myStreamWriter;

    [MenuItem("Project Tools/E-Z Organizer")]
    static void createWindow()
    {
        ExtensionWindow exWindow = (ExtensionWindow)EditorWindow.GetWindow(typeof(ExtensionWindow));

        exWindow.title = "Build Folders";
        exWindow.minSize = new Vector2(500f, 500f);

        //init();
    }
    void OnGUI()
    {
        extBank = new ExtensionBank();

        bool success;
        categories = new string[extBank.GetNumOfCategories()];
        exts = new string[extBank.GetNumOfExtensions];
        char[] delimiter = { System.Convert.ToChar(",") };


        success = extBank.PopulateList();
        if (success)
        {
            for (int i = 0; i < extBank.GetNumOfExtensions; i++)
            {
                string shortExtensions = extBank.GetExtensions(i);
                exts = shortExtensions.Split(delimiter);
            }
        }
    }
    void OnEnable()
    {
        Debug.Log(categories.Length);
    }
}