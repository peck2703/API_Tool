using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

class SampleExtensionBank : Editor
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
        return differentCategories.Count;
    }

    public bool PopulateList()
    {
        bool success = true;
        string path = "Assets/Scripts/Editor/SampleExtensions.txt";
        sourceFile = new FileInfo("Assets/Scripts/Editor/SampleExtensions.txt");

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
                //Debug.Log("Categories are : " + line);

                line = myStreamReader.ReadLine();
                while (line.Substring(0,1) == ".")
                {
                    //Debug.Log("Extensions are: " + line);
                    anExtension.Extensions = line;
                    theExtensions.Add(anExtension);

                    //Debug.Log("Extension Added: " + anExtension.Extensions);

                    line = myStreamReader.ReadLine();
                    //Debug.Log("After Extension is: " + line);
                }
                line = myStreamReader.ReadLine();
                //Debug.Log("Next Category is: " + line);
            }
            myStreamReader.Close();
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
