using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ExtensionUnit : Editor
{
    
    private string m_Extensions;

    
    public string Extensions
    {
        get { return m_Extensions; }
        set { m_Extensions = value; }
    }
}
