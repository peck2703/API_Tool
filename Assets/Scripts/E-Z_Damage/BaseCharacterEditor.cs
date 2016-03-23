using UnityEditor;
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
        [SerializeField]
        static List<Type> components = new List<Type>();
        [SerializeField]
        static List<string> componentNames = new List<string>();
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

        [SerializeField]
        SerializedProperty headProp;
        [SerializeField]
        SerializedProperty torsoProp;
        [SerializeField]
        SerializedProperty leftArmProp;
        [SerializeField]
        SerializedProperty rightArmProp;
        [SerializeField]
        SerializedProperty leftLegProp;
        [SerializeField]
        SerializedProperty rightLegProp;

        [SerializeField]
        SerializedProperty headMultiplier;
        [SerializeField]
        SerializedProperty torsoMultiplier;
        [SerializeField]
        SerializedProperty leftArmMultiplier;
        [SerializeField]
        SerializedProperty rightArmMultiplier;
        [SerializeField]
        SerializedProperty leftLegMultiplier;
        [SerializeField]
        SerializedProperty rightLegMultiplier;

        [SerializeField]
        SerializedProperty healthReference;

        //List<string> arrayScripts = new List<string>();

        //private static Dictionary<string, MonoScript> AllScripts = new Dictionary<string, MonoScript>();

        void OnEnable()
        {
            hideFlags = HideFlags.HideAndDontSave;

            if (currentGO.GetComponent<BaseCharacter>().DefaultChoice == "Default")
            {
                // Setup the SerializedProperties.
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                headProp = serializedObject.FindProperty("EZ_BodyHead");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                torsoProp = serializedObject.FindProperty("EZ_BodyTorso");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                leftArmProp = serializedObject.FindProperty("EZ_BodyLeftArm");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                rightArmProp = serializedObject.FindProperty("EZ_BodyRightArm");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                leftLegProp = serializedObject.FindProperty("EZ_BodyLeftLeg");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                rightLegProp = serializedObject.FindProperty("EZ_BodyRightLeg");

                //Setup serialized properties for multiplier
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                headMultiplier = serializedObject.FindProperty("EZ_BodyHeadRate");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                torsoMultiplier = serializedObject.FindProperty("EZ_BodyTorsoRate");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                leftArmMultiplier = serializedObject.FindProperty("EZ_BodyLeftArmRate");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                rightArmMultiplier = serializedObject.FindProperty("EZ_BodyRightArmRate");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                leftLegMultiplier = serializedObject.FindProperty("EZ_BodyLeftLegRate");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                rightLegMultiplier = serializedObject.FindProperty("EZ_BodyRightLegRate");
                hideFlags = HideFlags.DontUnloadUnusedAsset;
                healthReference = serializedObject.FindProperty("healthScript");

                //foreach (var script in result)
                //{
                //    Debug.Log(script.name);
                //}
                
                Assembly _assembly = Assembly.Load("Assembly-CSharp");

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
            if (headProp != null)
            { EditorGUILayout.PropertyField(headProp, new GUIContent("Head"), false, null); }
            if (torsoProp != null)
            { EditorGUILayout.PropertyField(torsoProp, new GUIContent("Torso"), false, null); }
            if (leftArmProp != null)
            { EditorGUILayout.PropertyField(leftArmProp, new GUIContent("Left Arm"), false, null); }
            if (rightArmProp != null)
            { EditorGUILayout.PropertyField(rightArmProp, new GUIContent("Right Arm"), false, null); }
            if (leftLegProp != null)
            { EditorGUILayout.PropertyField(leftLegProp, new GUIContent("Left Leg"), false, null); }
            if (rightLegProp != null)
            { EditorGUILayout.PropertyField(rightLegProp, new GUIContent("Right Leg"), false, null); }

            EditorGUILayout.Space();
            /*
            EditorGUILayout.PropertyField(headMultiplier, new GUIContent("Head Multiplier"), false, null);
            EditorGUILayout.PropertyField(torsoMultiplier, new GUIContent("Torso Mulitplier"), false, null);
            EditorGUILayout.PropertyField(leftArmMultiplier, new GUIContent("Left Arm Mulitplier"), false, null);
            EditorGUILayout.PropertyField(rightArmMultiplier, new GUIContent("Right Arm Multiper"), false, null);
            EditorGUILayout.PropertyField(leftLegMultiplier, new GUIContent("Left Leg Multiplier"), false, null);
            EditorGUILayout.PropertyField(rightLegMultiplier, new GUIContent("Right Leg Multiplier"), false, null);
            EditorGUILayout.EndVertical();
            */
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