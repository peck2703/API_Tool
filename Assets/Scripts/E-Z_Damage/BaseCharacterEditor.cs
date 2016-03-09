using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace API_TOOL
{
    // Custom Editor using SerializedProperties.
    // Automatic handling of multi-object editing, undo, and prefab overrides.
    [CustomEditor(typeof(BaseCharacter))]
    [CanEditMultipleObjects]

    public class BaseCharacterEditor : Editor
    {
        GameObject currentGO = Selection.activeGameObject;


        Transform[]/*List<Transform>*/ EZ_BodyHead;          //= 1.8f;
        Transform[]/*List<Transform>*/ EZ_BodyTorso;         //= 1.2f;
        Transform[]/*List<Transform>*/ EZ_BodyRightArm;      //= 0.8f;
        Transform[]/*List<Transform>*/ EZ_BodyLeftArm;       //= 0.8f;         // Upper vs Lower arm omitted because research states that the values are equal.
        Transform[]/*List<Transform>*/ EZ_BodyRightLeg;      //= 0.8f;
        Transform[]/*List<Transform>*/ EZ_BodyLeftLeg;       //= 0.8f;     
        Transform[]/*List<Transform>*/ BodyUpperRightLeg;    //= 1.04f;        // We should consider condensing left and right of each extremity.
        Transform[]/*List<Transform>*/ BodyUpperLeftLeg;     //= 1.04f;
        Transform[]/*List<Transform>*/ BodyLowerRightLeg;    //= 1.03f;  
        Transform[]/*List<Transform>*/ BodyLowerLeftLeg;     //= 1.03f;

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
            }
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();


            EditorGUILayout.PropertyField(headProp, new GUIContent("Head"),false, null);
            EditorGUILayout.FloatField("headMultiplier", headMultiplier, GUILayoutOption[] as null);
            // Show the custom GUI controls.
            /*EditorGUILayout.IntSlider(damageProp, 0, 100, new GUIContent("Damage"));
            

            // Only show the damage progress bar if all the objects have the same damage value:
            if (!damageProp.hasMultipleDifferentValues)
                ProgressBar(damageProp.intValue / 100.0f, "Damage");

            EditorGUILayout.IntSlider(armorProp, 0, 100, new GUIContent("Armor"));

            // Only show the armor progress bar if all the objects have the same armor value:
            if (!armorProp.hasMultipleDifferentValues)
                ProgressBar(armorProp.intValue / 100.0f, "Armor");

            EditorGUILayout.PropertyField(gunProp, new GUIContent("Gun Object"));*/
            //ListIterator("EZ_BodyParts");
            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
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