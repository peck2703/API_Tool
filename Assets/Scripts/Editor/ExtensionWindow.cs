using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExtensionWindow : EditorWindow
{
    public static List<string> extensions;
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
        myWindow.title = "Create Your Folders";
        myWindow.minSize = new Vector2(500f, 500f);

        init();
    }

    private static void init()
    {
        if (extensions != null)
            extensions.Clear();

        extensions = new List<string>();

        //Load default extensions. Add as needed, then add to custom .txt
        extensions.AddRange(standardExt);
        string assetExtension;

        foreach (var ext in extensions)
        {
            /*May cause errors here with the foreach loop, may need
             * to add some later*/
            assetExtension = ext;
            if (!ExtensionExists(assetExtension))
                extensions.Add(assetExtension);
        }
        extensionsImported = false;
    }

    private static bool ExtensionExists(string ext)
    {
        if (!extensions.Contains(ext))
            return false;

        return true;
    }

    private static void DrawFolders(Folder folder)
    {
        EditorGUI.indentLevel++;
        if (folder.readOnly) // Assets Folder ONLY
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(folder.folderName == "Assets" ? "" + folder.folderName : "∟  " + folder.folderName);

            GUIStyle option = null;
            if (folder.folderName == "Assets" || assetsOnlyReadOnly)
                option = EditorStyles.miniButton;
            else if (folder.imported)
                option = EditorStyles.miniButton;
            else
                option = EditorStyles.miniButtonLeft;

            int widthOfButton = folder.folderName == "Assets" ? (buttonWidth * 4) : buttonWidth;

            if (GUILayout.Button("+", option, GUILayout.Width(widthOfButton)))
            {
                if (folder.folderName == "Assets")
                    foldersAdded = true;

                string parentPath = folder.parentPath == null ? "" : folder.parentPath + "/";
                Folder newFolder = new Folder(parentPath + folder.folderName, "New Folder");

                if (!FolderExists(newFolder))
                {
                    folders.Add(newFolder);
                    folder.childFolders.Add(newFolder);
                }
            }
            if (!folder.imported)
            {
                if (folder.folderName != "Assets")
                {
                    if (GUILayout.Button("Edit", EditorStyles.miniButtonMid, GUILayout.Width(buttonWidth * 2)))
                    {
                        folder.readOnly = false;
                        folder.editMode = true;
                    }

                    if (GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(widthOfButton)))
                    {
                        if (folders.Contains(folder))
                        {
                            folder.markedForDelete = true;
                        }

                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("∟  " + folder.folderName);
            folder.folderName = GUILayout.TextField(folder.folderName, GUILayout.Width(textFieldWidth));
            if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(buttonWidth)))
            {
                if (folder.folderName == "")
                    folder.folderName = "New Folder";

                if (folder.editMode)
                {
                    folder.readOnly = true;
                    folder.editMode = false;
                }
                else
                {
                    folder.readOnly = true;
                    assetsOnlyReadOnly = false;
                    Folder newFolder = new Folder(folder.parentPath + "/" + folder.folderName, "New Folder");

                    if (!FolderExists(newFolder))
                    {
                        folders.Add(newFolder);
                        folder.childFolders.Add(newFolder);
                    }
                }
            }
            if (GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(buttonWidth)))
            {
                if (folders.Contains(folder))
                {
                    folder.markedForDelete = true;
                    //folders.Remove(folder);
                }

                EditorGUILayout.EndHorizontal();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        foreach (Folder childFolder in folder.childFolders)
        {
            if (!childFolder.markedForDelete)
                DrawFolders(childFolder);
            //else
            //folder.childFolders.Remove(childFolder);
        }

        EditorGUI.indentLevel--;

        for (int i = 0; i < folder.childFolders.Count; i++)
        {
            if (folder.childFolders[i].markedForDelete)
            {
                folder.childFolders.Remove(folder.childFolders[i]);
                break;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

	}
}
