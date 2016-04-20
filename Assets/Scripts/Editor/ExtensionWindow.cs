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

        exWindow.title = "Extensions";
        exWindow.minSize = new Vector2(500f, 500f);

        //init();
    }
    void Awake()
    {
        extBank = new ExtensionBank();

        bool success;

        success = extBank.PopulateList();

        Debug.Log("Successfully opened file?? " + success);

        categories = new string[extBank.GetNumOfCategories()];
        exts = new string[extBank.GetNumOfExtensions];
        //Debug.Log("Number of Exts is: " + exts.Length);
        char[] delimiter = { System.Convert.ToChar(",") };
        //Debug.Log("Number of Exts is w/ Delimiter: " + exts.Length);

        if (success)
        {
            for (int i = 0; i < extBank.GetNumOfExtensions; i++)
            {
                string shortExtensions = extBank.GetExtensions(i);
                //Debug.Log("Extension at index 0 is: " + extBank.GetExtensions(0));
                //Debug.Log("Extension at index 1 is: " + extBank.GetExtensions(1));
                //Debug.Log("Extension at index 2 is: " + extBank.GetExtensions(2));
                //Debug.Log("Extension is: " + extBank.GetExtensions(i));
                exts = shortExtensions.Split(delimiter);
            }
        }
    }
    void OnEnable()
    {

    }
}