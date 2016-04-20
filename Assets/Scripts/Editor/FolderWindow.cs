using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

// The script must be placed in the Editor Folder in order to execute.

public class FolderWindow : EditorWindow {

    // List must be public in order for children folders to be created.
    public static List<Folder> folders;

    private static Vector2 scrollPosition;
    private static int textWidth = 200;
    private static int buttonWidth = 25;
    private static bool assetsOnlyReadOnly = true;
    private static bool foldersImported = false;
    private static bool foldersAdded = false;


    //[MenuItem("Project Tools/E-Z Organizer")]
    // Use this for initialization
    static void createWindow () {
        FolderWindow window = (FolderWindow)EditorWindow.GetWindow(typeof(FolderWindow));
        window.title = "Build Folders";
        window.minSize = new Vector2(500f, 500f);

        init();
    }
	
	// Update is called once per frame
	private static void init() {
        if (folders != null)
            folders.Clear();

        folders = new List<Folder>();

        Folder assetsFolder = new Folder();

        if (folders.Count == 0 || !FolderExists(assetsFolder))
            folders.Add(assetsFolder);

        foldersImported = false;
    }

    void OnGUI() {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        EditorGUI.indentLevel = 0;

        DrawFolders(folders[0]);

        GUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();

        if (!foldersImported && !foldersAdded) {
            if (GUILayout.Button("Import Current Folders", EditorStyles.miniButton)) {
                ImportFolders();
            }
        }

        if (folders.Count <= 1) {
            EditorGUILayout.EndHorizontal();
            return;
        }

        if (GUILayout.Button("Generate Folders", EditorStyles.miniButtonLeft)) {
            CreateFolder(folders[0]);
            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("Clear Folders", EditorStyles.miniButtonRight)) {
            init();
        }
        EditorGUILayout.EndHorizontal();
    }

    private static void DrawFolders(Folder folder) {
        EditorGUI.indentLevel++;
        if (folder.readOnly) { // Assets Folder ONLY 

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(folder.folderName == "Assets" ? "" + folder.folderName : ">  " + folder.folderName);

            GUIStyle option = null;
            if (folder.folderName == "Assets" || assetsOnlyReadOnly)
                option = EditorStyles.miniButton;
            else if (folder.imported)
                option = EditorStyles.miniButton;
            else
                option = EditorStyles.miniButtonLeft;

            int widthOfButton = folder.folderName == "Assets" ? (buttonWidth * 4) : buttonWidth;

            if (GUILayout.Button("+", option, GUILayout.Width(widthOfButton))) {
                if (folder.folderName == "Assets")
                    foldersAdded = true;

                string parentPath = folder.parentPath == null ? "" : folder.parentPath + "/";
                Folder newFolder = new Folder(parentPath + folder.folderName, "New Folder");

                if (!FolderExists(newFolder)) {
                    folders.Add(newFolder);
                    folder.childFolders.Add(newFolder);
                }
            }

            if (!folder.imported) {
                if (folder.folderName != "Assets") {
                    if (GUILayout.Button("Edit", EditorStyles.miniButtonMid, GUILayout.Width(buttonWidth * 2))) {
                        folder.readOnly = false;
                        folder.editMode = true;
                    }

                    if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(widthOfButton))) {
                        if (folders.Contains(folder)) {
                            folder.markedForDelete = true;
                        }

                        EditorGUILayout.EndHorizontal();
                        return;
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

        }

        else {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(">  " + folder.folderName);
            folder.folderName = GUILayout.TextField(folder.folderName, GUILayout.Width(textWidth));

            if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(buttonWidth))) {
                if (folder.folderName == "")
                    folder.folderName = "New Folder";

                if (folder.editMode) {
                    folder.readOnly = true;
                    folder.editMode = false;
                }
                else {
                    folder.readOnly = true;
                    assetsOnlyReadOnly = false;
                    Folder newFolder = new Folder(folder.parentPath + "/" + folder.folderName, "New Folder");

                    if (!FolderExists(newFolder)) {
                        folders.Add(newFolder);
                        folder.childFolders.Add(newFolder);
                    }
                }
            }

            if (GUILayout.Button(">", EditorStyles.miniButtonRight, GUILayout.Width(buttonWidth))) {
                if (folders.Contains(folder)) {
                    folder.markedForDelete = true;
                    //folders.Remove(folder);
                }

                EditorGUILayout.EndHorizontal();
                return;
            }
            EditorGUILayout.EndHorizontal();
        }

        foreach (Folder childFolder in folder.childFolders) {
            if (!childFolder.markedForDelete)
                DrawFolders(childFolder);
            //else
            //folder.childFolders.Remove(childFolder);
        }

        EditorGUI.indentLevel--;

        for (int i = 0; i < folder.childFolders.Count; i++) {

            if (folder.childFolders[i].markedForDelete) {
                folder.childFolders.Remove(folder.childFolders[i]);
                break;
            }
        }
    } // end DrawFolders

    private static bool FolderExists(Folder folder) {
        if (!folders.Contains(folder))
            return false;

        return true;
    }

    private static bool PathExists(string path) {
        return (Directory.Exists(path));
    }

    static void CreateFolder(Folder folder) {

        string folderPath = null;

        if (folder.parentPath == null)
            folderPath = folder.folderName;
        else
            folderPath = folder.parentPath + "/" + folder.folderName;

        if (!PathExists(folderPath)) {
            AssetDatabase.CreateFolder(folder.parentPath, folder.folderName);
        }
        else
            Debug.Log("The folder: '" + folder.folderName + "' at '" + folderPath + "'" + " already exists.");

        foreach (Folder childFolder in folder.childFolders) {
            CreateFolder(childFolder);
        }
    }

    static void ImportFolders() {
        //Folder assetFolder = new Folder ();
        //folders.Clear ();
        //folders.Add (assetFolder);
        init();

        foldersImported = true;

        string assetsRootPath = Directory.GetCurrentDirectory() + "\\Assets";

        // Get a list of all directories
        List<string> directories = CleanDirectories(GetAllDirectories(assetsRootPath));

        foreach (string path in directories) {

            string[] pathSplit = path.Split('\\');

            string str = "";

            for (int i = 0; i < pathSplit.Length; i++) {

                if (i < pathSplit.Length - 1) {
                    str += pathSplit[i] + "/";
                }
                else {
                    Folder folder = new Folder(str, pathSplit[i]);
                    folder.readOnly = true;
                    folder.imported = true;

                    int pathIndex = GetFolderIndex(pathSplit[i - 1]);
                    if (folders[pathIndex] != null && pathIndex < folders.Count)
                    {
                        folders[pathIndex].childFolders.Add(folder);
                    }
                    folders.Add(folder);
                }
            }
        } // end foreach
    }

    static int GetFolderIndex(string folderName) {

        for (int i = 0; i < folders.Count; i++) {
            if (folders[i].folderName == folderName) {
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

public class Folder
{
    public string folderName;
    public string parentPath;
    public List<Folder> childFolders;
    public bool readOnly = false;
    public bool markedForDelete = false;
    public bool editMode = false;
    public bool imported = false;

    public Folder()
    {
        this.folderName = "Assets";
        this.parentPath = null;
        this.childFolders = new List<Folder>();
        this.readOnly = true;
        this.markedForDelete = false;
        this.editMode = false;
        this.imported = false;
    }

    public Folder(string parentPath, string folderName)
    {
        this.folderName = folderName;
        this.parentPath = parentPath;
        this.childFolders = new List<Folder>();
        this.readOnly = false;
        this.markedForDelete = false;
        this.editMode = false;
        this.imported = false;
    }
}
