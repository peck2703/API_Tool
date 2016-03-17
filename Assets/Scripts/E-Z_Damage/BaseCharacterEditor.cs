﻿using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace API_TOOL
{
    // Custom Editor using SerializedProperties.
    // Automatic handling of multi-object editing, undo, and prefab overrides.
    [CustomEditor(typeof(BaseCharacter))]
    [CanEditMultipleObjects]
    [System.Serializable]
    public class BaseCharacterEditor : Editor
    {
        static List<Type> components;
        static List<string> componentNames;
        int index = 0;                                      //Index for the popup menu

        //List<MonoScript> result = new List<MonoScript>();

        GameObject currentGO = Selection.activeGameObject;
        GameObject[]/*List<GameObject>*/ EZ_BodyHead;          //= 1.8f;
        GameObject[]/*List<GameObject>*/ EZ_BodyTorso;         //= 1.2f;
        GameObject[]/*List<GameObject>*/ EZ_BodyRightArm;      //= 0.8f;
        GameObject[]/*List<GameObject>*/ EZ_BodyLeftArm;       //= 0.8f;         // Upper vs Lower arm omitted because research states that the values are equal.
        GameObject[]/*List<GameObject>*/ EZ_BodyRightLeg;      //= 0.8f;
        GameObject[]/*List<GameObject>*/ EZ_BodyLeftLeg;       //= 0.8f;     
        GameObject[]/*List<GameObject>*/ BodyUpperRightLeg;    //= 1.04f;        // We should consider condensing left and right of each extremity.
        GameObject[]/*List<GameObject>*/ BodyUpperLeftLeg;     //= 1.04f;
        GameObject[]/*List<GameObject>*/ BodyLowerRightLeg;    //= 1.03f;  
        GameObject[]/*List<Transform>*/ BodyLowerLeftLeg;     //= 1.03f;

        SerializedProperty headProp;
        SerializedProperty torsoProp;
        SerializedProperty leftArmProp;
        SerializedProperty rightArmProp;
        SerializedProperty leftLegProp;
        SerializedProperty rightLegProp;

        SerializedProperty headMultiplier;
        SerializedProperty torsoMultiplier;
        SerializedProperty leftArmMultiplier;
        SerializedProperty rightArmMultiplier;
        SerializedProperty leftLegMultiplier;
        SerializedProperty rightLegMultiplier;

        SerializedProperty healthReference;

        //List<string> arrayScripts = new List<string>();

        //private static Dictionary<string, MonoScript> AllScripts = new Dictionary<string, MonoScript>();

        void OnEnable()
        {
            if (currentGO.GetComponent<BaseCharacter>().DefaultChoice == "Default")
            {
                // Setup the SerializedProperties.
                headProp = serializedObject.FindProperty("EZ_BodyHead");
                torsoProp = serializedObject.FindProperty("EZ_BodyTorso");
                leftArmProp = serializedObject.FindProperty("EZ_BodyLeftArm");
                rightArmProp = serializedObject.FindProperty("EZ_BodyRightArm");
                leftLegProp = serializedObject.FindProperty("EZ_BodyLeftLeg");
                rightLegProp = serializedObject.FindProperty("EZ_BodyRightLeg");

                //Setup serialized properties for multiplier
                headMultiplier = serializedObject.FindProperty("EZ_BodyHeadRate");
                torsoMultiplier = serializedObject.FindProperty("EZ_BodyTorsoRate");
                leftArmMultiplier = serializedObject.FindProperty("EZ_BodyLeftArmRate");
                rightArmMultiplier = serializedObject.FindProperty("EZ_BodyRightArmRate");
                leftLegMultiplier = serializedObject.FindProperty("EZ_BodyLeftLegRate");
                rightLegMultiplier = serializedObject.FindProperty("EZ_BodyRightLegRate");
                healthReference = serializedObject.FindProperty("healthScript");

                //foreach (var script in result)
                //{
                //    Debug.Log(script.name);
                //}
                Assembly _assembly = Assembly.Load("Assembly-CSharp");

                components = new List<Type>();
                componentNames = new List<string>();

                foreach (Type type in _assembly.GetTypes())
                {
                    if (type.IsClass)
                    {
                        if (type.BaseType.FullName.Contains("MonoBehaviour"))
                        {
                            components.Add(type);
                            componentNames.Add(type.Name);
                            //                    Debug.Log(type.Name);
                        }
                        else
                        {
                            if (!type.BaseType.FullName.Contains("System"))
                            {
                                Type _type = type.BaseType;
                                components.Add(_type);
                                componentNames.Add(type.Name);
                                //                        Debug.Log(type.Name);
                            }
                        }
                    }
                }
            }
            foreach (string val in componentNames)
            {
                Debug.Log(val);
            }
        }
        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(headProp, new GUIContent("Head"),false, null);
            EditorGUILayout.PropertyField(torsoProp, new GUIContent("Torso"), false, null);
            EditorGUILayout.PropertyField(leftArmProp, new GUIContent("Left Arm"), false, null);
            EditorGUILayout.PropertyField(rightArmProp, new GUIContent("Right Arm"), false, null);
            EditorGUILayout.PropertyField(leftLegProp, new GUIContent("Left Leg"), false, null);
            EditorGUILayout.PropertyField(rightLegProp, new GUIContent("Right Leg"), false, null);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(headMultiplier, new GUIContent("Head Multiplier"), false, null);
            EditorGUILayout.PropertyField(torsoMultiplier, new GUIContent("Torso Mulitplier"), false, null);
            EditorGUILayout.PropertyField(leftArmMultiplier, new GUIContent("Left Arm Mulitplier"), false, null);
            EditorGUILayout.PropertyField(rightArmMultiplier, new GUIContent("Right Arm Multiper"), false, null);
            EditorGUILayout.PropertyField(leftLegMultiplier, new GUIContent("Left Leg Multiplier"), false, null);
            EditorGUILayout.PropertyField(rightLegMultiplier, new GUIContent("Right Leg Multiplier"), false, null);
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();
            index = EditorGUILayout.Popup("Script:", index, componentNames.ToArray(), EditorStyles.popup);
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
            /*EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(healthReference, new GUIContent("Health Script"), false, null);
            EditorGUILayout.EndVertical();*/
        }

        // Custom GUILayout progress bar.
        void ProgressBar(float value, string label)
        {
            // Get a rect for the progress bar using the same margins as a textfield:
            Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
            EditorGUI.ProgressBar(rect, value, label);
            EditorGUILayout.Space();
        }
    }
}