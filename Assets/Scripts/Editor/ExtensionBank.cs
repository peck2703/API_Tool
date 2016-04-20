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
        //Debug.Log("Inside Get Num of Categories");
        if (PopulateList())
        {
            Debug.Log("List Populated");
            for (int i = 0; i < theExtensions.Count; i++)
            {
                if (!differentCategories.Contains(theExtensions[i].Categories.ToString()))
                {
                    differentCategories.Add(theExtensions[i].Categories.ToString());
                    Debug.Log(theExtensions[i].Categories);
                }
            }
        }
        return differentCategories.Count;

    }
    public bool PopulateList()
    {
        bool success = true;
        string path = "Assets/Scripts/Editor/Extensions.txt";
        sourceFile = new FileInfo("Assets/Scripts/Editor/Extensions.txt");

        if (!File.Exists(path))
        {
            Debug.Log("File Does Not Exist");
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

                string[] shortExts = line.Split(',');
                for (int i = 0; i < shortExts.Length; i++)
                {
                    anExtension.Extensions = shortExts[i];
                    Debug.Log("Extension " + shortExts[i] + " at index: " + i);
                    theExtensions.Add(anExtension);

                    //Check for more than one extension in each Category
                    /*if (shortExts.Length < 1)
                    {
                        for (int j = 0; j < theExtensions.Count; j++)
                        {
                            Debug.Log("Inside for loop at iteration " + j);
                            Debug.Log("Extension at index " + j + " is: " + anExtension.Extensions);
                        }
                    }*/
                }
                line = myStreamReader.ReadLine();
            }
        }
        catch
        {
            success = false;
        }
        //Debug.Log("At the end, the total Number of Extensions is: " + theExtensions.Count);
        for (int m = 0; m < theExtensions.Count; m++)
        {
            //Debug.Log("Extension at index m is: " + GetExtensions(m));
        }
        return success;
    }
}
