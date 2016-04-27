using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

// The script must be placed in the Editor FolderInit in order to execute.

public class FolderInitWindow : EditorWindow {

    // List must be public in order for children FolderInits to be created.
    public static List<FolderInit> FolderInits;

    private FolderInit FolderInit;
    private static Vector2 scrollPosition;
    private static int textWidth = 200;
    private static int buttonWidth = 25;
    private static bool assetsOnlyReadOnly = true;
    private static bool FolderInitsImported = false;
    private static bool FolderInitsAdded = false;

    [MenuItem("Project Tools/E-Z Folder")]

    static void createWindow () {
        FolderInitWindow window = (FolderInitWindow)EditorWindow.GetWindow(typeof(FolderInitWindow));
        window.title = "Build Folder";
        window.minSize = new Vector2(500f, 500f);

        init();
    }
	
	// Update is called once per frame
	private static void init() {
        FolderInits = new List<FolderInit>();

        FolderInit assetsFolderInit = new FolderInit();

        if (FolderInits.Count == 0 || !FolderInitExists(assetsFolderInit))
            FolderInits.Add(assetsFolderInit);
    }

    void OnGUI() {

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        EditorGUI.indentLevel = 0;

        DrawFolderInits(FolderInits[0]);

        GUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();

        if (FolderInits.Count <= 1) {
            EditorGUILayout.EndHorizontal();
            return;
        }

        if (GUILayout.Button("Generate Folder", EditorStyles.miniButtonLeft)) {
                CreateFolderInit(FolderInits[0]);
                AssetDatabase.Refresh();
        }

        EditorGUILayout.EndHorizontal();
    }

    private static void DrawFolderInits(FolderInit FolderInit) {
        EditorGUI.indentLevel++;
        if (FolderInit.readOnly) { // Assets FolderInit ONLY 

            EditorGUILayout.BeginHorizontal();

            GUIStyle option = null;
            if (FolderInit.folderName == "Assets" || assetsOnlyReadOnly)
                option = EditorStyles.miniButton;
            else
                option = EditorStyles.miniButtonLeft;

            int widthOfButton = buttonWidth;

            if (GUILayout.Button("Add Folder", option, GUILayout.Width(100))) {
                if (FolderInit.folderName == "Assets")
                    FolderInitsAdded = true;

                string parentPath = FolderInit.parentPath == null ? "" : FolderInit.parentPath + "/";
                FolderInit newFolderInit = new FolderInit(parentPath + FolderInit.folderName, "New Folder");

                if (!FolderInitExists(newFolderInit)) {
                    FolderInits.Add(newFolderInit);
                    FolderInit.childFolders.Add(newFolderInit);
                }
            }
            if (FolderInit.folderName != "Assets") {
                    FolderInit.readOnly = false;

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }

        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(">  " + FolderInit.folderName);
            FolderInit.folderName = GUILayout.TextField(FolderInit.folderName, GUILayout.Width(textWidth));

            if (GUILayout.Button(" + ", EditorStyles.miniButtonLeft, GUILayout.Width(buttonWidth))) {
                if (FolderInit.folderName == "")
                    FolderInit.folderName = "New Folder";

                    FolderInit newFolderInit = new FolderInit(FolderInit.parentPath + "/" + FolderInit.folderName, "New Folder");

                    if (!FolderInitExists(newFolderInit)) {
                        FolderInits.Add(newFolderInit);
                        FolderInit.childFolders.Add(newFolderInit);
                    }
            }

            if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(buttonWidth))) {
                if (FolderInits.Contains(FolderInit)) {
                    FolderInit.delete = true;
                }

                EditorGUILayout.EndHorizontal();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        foreach (FolderInit childFolderInit in FolderInit.childFolders) {
            if (!childFolderInit.delete)
                DrawFolderInits(childFolderInit);
        }

        EditorGUI.indentLevel--;

        for (int i = 0; i < FolderInit.childFolders.Count; i++) {

            if (FolderInit.childFolders[i].delete) {
                FolderInit.childFolders.Remove(FolderInit.childFolders[i]);
                break;
            }
        }
    } // end DrawFolderInits

    private static bool FolderInitExists(FolderInit FolderInit) {
        if (!FolderInits.Contains(FolderInit))
            return false;

        return true;
    }

    private static bool PathExists(string path) {
        return (Directory.Exists(path));
    }

    static void CreateFolderInit(FolderInit FolderInit) {

        string FolderInitPath = null;

        if (FolderInit.parentPath == null)
            FolderInitPath = FolderInit.folderName;
        else
            FolderInitPath = FolderInit.parentPath + "/" + FolderInit.folderName;

        if (!PathExists(FolderInitPath)) {
            AssetDatabase.CreateFolder(FolderInit.parentPath, FolderInit.folderName);
        }

        foreach (FolderInit childFolderInit in FolderInit.childFolders) {
            CreateFolderInit(childFolderInit);
        }
    }

    static int GetFolderInitIndex(string FolderInitName) {

        for (int i = 0; i < FolderInits.Count; i++) {
            if (FolderInits[i].folderName == FolderInitName) {
                return i;
            }
        }
        return 0;
    }

    static List<string> CleanDirectories(List<string> directories) {
        List<string> dirs = new List<string>();

        foreach (string dir in directories) {
            dirs.Add(CleanStringToAssetRoot(dir));
        }

        return dirs;
    }

    static string CleanStringToAssetRoot(string str) {
        int index = 0;

        for (int i = 0; i < str.Length; i++) {
            string subString = str.Substring(index);

            if (subString.StartsWith("Assets")) {
                return subString;
            }
            index++;
        }

        return str;
    }

    // Recursively called function to get child paths
    static List<string> GetAllDirectories(string path) {
        // Get directories in 'path'
        string[] directories = Directory.GetDirectories(path);

        List<string> directoriesList = new List<string>();

        // Loop through child directories
        for (int i = 0; i < directories.Length; i++) {
            directoriesList.Add(directories[i]);
            // Get directories in child directory
            List<string> dirs = GetAllDirectories(directories[i]);

            foreach (string str in dirs) {
                directoriesList.Add(str);
            }
        }

        return directoriesList;
    }
}
