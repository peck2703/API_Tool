using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CatergoryUnit : Editor
{
    private string m_Categories;
    public string Categories
    {
        get { return m_Categories; }
        set { m_Categories = value; }
    }
}
