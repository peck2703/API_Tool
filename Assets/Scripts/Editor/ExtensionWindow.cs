using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExtensionWindow : EditorWindow
{
    ExtensionBank extBank;

    string[] categories;
    string[] exts;
    public bool[] extToggles;


    public static List<string> extensions;

    public static Vector2 scrollPosition;
    private static int textFieldWidth = 200;
    private static int buttonWidth = 25;

    private static StreamReader myStreamReader;
    private static StreamWriter myStreamWriter;

    [MenuItem("Project Tools/E-Z Organizer ")]
    static void Organize()
    {
        ExtensionWindow myWindow = (ExtensionWindow)EditorWindow.GetWindow(typeof(ExtensionWindow));
        myWindow.title = "Extensions";
        myWindow.minSize = new Vector2(500f, 500f);
    }

    void Awake()
    {
        extBank = new ExtensionBank();

        bool success;

        success = extBank.PopulateList();

        Debug.Log("Successfully opened file?? " + success);

        categories = new string[extBank.GetNumOfCategories()];
        exts = new string[extBank.GetNumOfExtensions];

        Debug.Log(success);

        if (success)
        {
            Debug.Log(extBank.GetNumOfExtensions);
            for (int i = 0; i < extBank.GetNumOfExtensions; i++)
            {
                string shortExtensions = extBank.GetExtensions(i);
                Debug.Log("Extension Window sees " + shortExtensions + " as an extension");
            }
        }
    }
}