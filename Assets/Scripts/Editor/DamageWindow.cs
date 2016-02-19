using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.AnimatedValues;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                                                               *
 *                                                               *
 *      Author: Michael Peck                                     *
 *      Date: 2/18/2016                                          *
 *                                                               *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class DamageWindow : EditorWindow
{
    [MenuItem("Window/E-Z Damage")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DamageWindow),false, "E-Z Damage");
    }

    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    //Animating Window for Fade FX
    AnimBool m_ShowExtraFields;
    AnimBool m_HideExtraFields;
    string m_String;
    Color m_Color = Color.white;
    int m_Number = 0;

    void OnEnable()
    {
        m_ShowExtraFields = new AnimBool(false);
        m_HideExtraFields = new AnimBool(false);
        m_ShowExtraFields.valueChanged.AddListener(Repaint);
        m_HideExtraFields.valueChanged.AddListener(Repaint);
    }

    void OnGUI()
    {
        //GUILayout.Label("Base Settings", EditorStyles.label);
        //myString = EditorGUILayout.TextField("Text Field", myString);

        /*groupEnabled = EditorGUILayout.BeginFadeGroup("Optional Settings", groupEnabled);

        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);

        EditorGUILayout.EndFadeGroup();

        //groupEnabled = EditorGUILayout.BeginFadeGroup
        */


        m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields", m_ShowExtraFields.target);

        //Extra block that can be toggled on and off.
        if (EditorGUILayout.BeginFadeGroup(m_ShowExtraFields.faded))
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PrefixLabel("Color");
            m_Color = EditorGUILayout.ColorField(m_Color);
            EditorGUILayout.PrefixLabel("Text");
            m_String = EditorGUILayout.TextField(m_String);
            EditorGUILayout.PrefixLabel("Number");
            m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();

        m_HideExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields", m_HideExtraFields.target);

        if (EditorGUILayout.BeginFadeGroup(m_HideExtraFields.faded))
        {

         //   m_ShowExtraFields.target = EditorGUILayout.ToggleLeft("Show extra fields", m_ShowExtraFields.target);

            EditorGUI.indentLevel++;
            EditorGUILayout.PrefixLabel("Color");
            m_Color = EditorGUILayout.ColorField(m_Color);
            EditorGUILayout.PrefixLabel("Text");
            m_String = EditorGUILayout.TextField(m_String);
            EditorGUILayout.PrefixLabel("Number");
            m_Number = EditorGUILayout.IntSlider(m_Number, 0, 10);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndFadeGroup();
    }
}
