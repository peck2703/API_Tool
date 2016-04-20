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
    private static bool assetsOnlyReadOnly = true;
    private static bool extensionsImported = false;
    private static bool extensionsAdded = false;

    private static StreamReader myStreamReader;
    private static StreamWriter myStreamWriter;

    [MenuItem("Project Tools/E-Z Organizer ")]
    static void Organize()
    {
        ExtensionWindow myWindow = (ExtensionWindow)EditorWindow.GetWindow(typeof(ExtensionWindow));
        myWindow.title = "Extensions";
        myWindow.minSize = new Vector2(500f, 500f);

        //init();
    }

    void Awake()
    {
        extBank = new ExtensionBank();

        bool success;

        success = extBank.PopulateList();

        Debug.Log("Successfully opened file?? " + success);

        categories = new string[extBank.GetNumOfCategories()];
        //Debug.Log("Number of Cats is: " + categories.Length);

        exts = new string[extBank.GetNumOfExtensions];
        //Debug.Log("Number of Exts is: " + exts.Length);

        char[] delimiter = { System.Convert.ToChar(",") };
      //  Debug.Log("Number of Exts is w/ Delimiter: " + exts.Length);

        Debug.Log(success);
       // success = extBank.PopulateList();

        if (success)
        {
            Debug.Log(extBank.GetNumOfExtensions);
            for (int i = 0; i < extBank.GetNumOfExtensions; i++)
            {
                Debug.Log("Number of times " + i);
                /*string shortExtensions = extBank.GetExtensions(i);
                Debug.Log("Extension at index 0 is: " + extBank.GetExtensions(0));
                Debug.Log("Extension at index 1 is: " + extBank.GetExtensions(1));
                Debug.Log("Extension at index 2 is: " + extBank.GetExtensions(2));
                Debug.Log("Extension is: " + extBank.GetExtensions(i));
                exts = shortExtensions.Split(delimiter);*/
            }
        }
    }
}