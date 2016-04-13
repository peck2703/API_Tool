using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ExtensionBank : Editor
{
    List<ExtensionUnit> theExtensions = new List<ExtensionUnit>();
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
        return theExtensions[index].Categories;
    }
    public int GetNumOfCategories()
    {
        Debug.Log("Inside Get Num of Categories");
        for (int i = 0; i < theExtensions.Count; i++)
        {
            if (!differentCategories.Contains(theExtensions[i].Categories.ToString()))
            {
                differentCategories.Add(theExtensions[i].Categories.ToString());
                Debug.Log(theExtensions[i].Categories);
            }
        }
        return differentCategories.Count;
    }
    public bool PopulateList()
    {
        bool success = true;
        string path = "Assets/Scripts/Editor/Extensions.txt";
        sourceFile = new FileInfo("Extensions.txt");

        if (!File.Exists(path))
        {
            TextWriter tw = new StreamWriter(path, true);
            tw.Close();
        }
        string line;
        ExtensionUnit anExtension;

        try
        {
            StreamReader myStreamReader = sourceFile.OpenText();
            line = myStreamReader.ReadLine();

            while (line != null)
            {
                anExtension = new ExtensionUnit();

                anExtension.Categories = line;
                line = myStreamReader.ReadLine();

                anExtension.Extensions = line;
                theExtensions.Add(anExtension);

                line = myStreamReader.ReadLine();
            }
        }
        catch
        {
            success = false;
        }
        return success;
    }
}
