using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ExtensionUnit : Editor
{
    private string m_Categories;
    private string m_Extensions;

    public string Categories
    {
        get { return m_Categories; }
        set { m_Categories = value; }
    }
    public string Extensions
    {
        get { return m_Extensions; }
        set { m_Extensions = value; }
    }
}
