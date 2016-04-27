using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

public class ExtensionBank : Editor
{
    List<ExtensionUnit> theExtensions = new List<ExtensionUnit>();
    List<CatergoryUnit> theCategories = new List<CatergoryUnit>();
    List<string> differentCategories = new List<string>();
    FileInfo sourceFile;

    public int GetNumOfExtensions
    {
        get { return theExtensions.Count; }
    }
    public string GetExtensions(int index)
    {
        return theExtensions[index].Extensions;
    }
    public string GetCategories(int index)
    {
        return theCategories[index].Categories;
    }
    public int GetNumOfCategories()
    {
        return differentCategories.Count;
    }
    public bool PopulateList()
    {
        ExtensionUnit anExtension;
        bool success = true;
        var path = "Assets/Scripts/Editor/Extensions.txt";

        if(File.Exists(path))
        {
            try
            {
                var fileContent = File.ReadAllLines(path);

                foreach (var line in fileContent)
                {
                    anExtension = new ExtensionUnit();

                    //Debug.Log("Extension is: " + line);
                    if (line != "")
                    {
                        if(line.Substring(0,1) == ".")
                        {
                            Debug.Log("Extension is: " + line);
                            anExtension.Extensions = line;
                            theExtensions.Add(anExtension);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.Log(ex);
                success = false;
            }
        }
        return success;
    }
}
