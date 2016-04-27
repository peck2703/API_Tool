using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class ExtensionWindow : EditorWindow
{
    ExtensionBank extBank;

    string[] exts;
    public bool[] extToggles;

    public static List<string> extensions;
    public static List<string> categories;

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
        //Get all Category names and store them in a List<>
        LoadFolderNames();

        extBank = new ExtensionBank();

        //Populate the bank of Extensions from the .txt file
        bool success;
        success = extBank.PopulateList();

        exts = new string[extBank.GetNumOfExtensions];

        if (success)
        {
            for (int i = 0; i < extBank.GetNumOfExtensions; i++)
            {
                string shortExtensions = extBank.GetExtensions(i);
            }
        }
    }

    void OnGUI()
    {
        extToggles = new bool[extBank.GetNumOfExtensions];

        int[] index = new int[extBank.GetNumOfExtensions];
        int selectedIndex = 0;
        //scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.BeginVertical();
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Height(475), GUILayout.Width(475));
     //   for (int i = 0; i < extToggles.Length; i++)
     //   {
     //       index[i] = EditorGUI.Popup(new Rect(0, (0 + (20 * i) + 5), position.width - 25, (position.height + (20 * i))), extBank.GetExtensions(i), 0, categories.ToArray(), EditorStyles.popup);
      //      selectedIndex++;
      //  }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    void ListExtensions()
    {
        
    }
    void LoadFolderNames()
    {
        var path = "Assets/Scripts/Editor/Extensions.txt";

        if (File.Exists(path))
        {
            try
            {
                var fileContent = File.ReadAllLines(path);
                categories = new List<string>();
                foreach (var line in fileContent)
                {
                    //Debug.Log("Extension is: " + line);
                    if (line != "")
                    {
                        if ((line.Substring(0, 1) != "" || line.Substring(0, 1) != null) && line.Substring(0, 1) != ".")
                        {
                            categories.Add(line);
                            //Debug.Log("Category is: " + line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }
        }
    }
    void OrganizeScripts()                  //Handles between MonoBehaviour and Editor Scripts
    {
        Assembly _assembly = Assembly.Load("Assembly-CSharp");

        foreach (Type type in _assembly.GetTypes())
        {
            if (type.IsClass)
            {
                if (type.BaseType.FullName.Contains("MonoBehaviour"))           //Standard Unity Scripts
                {

                }
                else if (type.BaseType.FullName.Contains("Editor"))             //Unity Editor Files
                {

                }
                else                                                            //All others, likely .js scripts
                {

                }
            }
        }
    }
}