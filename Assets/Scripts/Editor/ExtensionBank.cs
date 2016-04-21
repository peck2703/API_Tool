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

            Debug.Log("First Extension is: " + line);

            while (!myStreamReader.EndOfStream)
            {
                anExtension = new ExtensionUnit();

                anExtension.Categories = line;
                line = myStreamReader.ReadLine();

                /*if(line.Substring(0,1) == ".")
                {
                    //Debug.Log(line.Substring(0, 1));
                }*/


                //Debug.Log(line.Substring(0, 1));
                /*while(line.Substring(0,1) == ".")
                {
                    anExtension.Extensions = line;
                    theExtensions.Add(anExtension);

                    //Next extension
                    line = myStreamReader.ReadLine();
                }*/

                Debug.Log("Next Extension is: " + line);
                //Empty blank space
                line = myStreamReader.ReadLine();
            }
            myStreamReader.Close();
        }
        catch
        {
            success = false;
        }

        //Debug.Log("At the end, the total Number of Extensions is: " + theExtensions.Count);
        /*for (int m = 0; m < theExtensions.Count; m++)
        {
            //Debug.Log("Extension at index m is: " + GetExtensions(m));
        }*/
        return success;
    }
}
