using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FolderInit : Editor {

    public string folderName;
    public string parentPath;
    public List<FolderInit> childFolders;
    public bool readOnly = false;
    public bool delete = false;

    public FolderInit()
    {
        this.folderName = "Assets";
        this.parentPath = null;
        this.childFolders = new List<FolderInit>();
        this.readOnly = true;
        this.delete = false;
    }

    public FolderInit(string parentPath, string folderName)
    {
        this.folderName = folderName;
        this.parentPath = parentPath;
        this.childFolders = new List<FolderInit>();
        this.readOnly = false;
        this.delete = false;
    }
}
