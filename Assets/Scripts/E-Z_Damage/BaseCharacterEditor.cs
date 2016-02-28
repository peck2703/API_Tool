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

        void OnEnable()
        {
            if (currentGO.GetComponent<BaseCharacter>().DefaultChoice == "Default")
            {
                /*SerializedProperty headProp;
                SerializedProperty torsoProp;
                SerializedProperty leftArmProp;
                SerializedProperty rightArmProp;
                SerializedProperty leftLegProp;
                SerializedProperty rightLegProp;
                */

                Debug.Log(currentGO);
                if (currentGO.GetComponent<BaseCharacter>().DefaultChoice == "Default")
                {
                    Debug.Log("Default:  " + currentGO);
                }
                // Setup the SerializedProperties.
                //damageProp = serializedObject.FindProperty("damage");
                //armorProp = serializedObject.FindProperty("armor");
                //gunProp = serializedObject.FindProperty("gun");
            }
        }

        public override void OnInspectorGUI()
        {
            // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
            serializedObject.Update();

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
            ListIterator("EZ_BodyParts");
            // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
            serializedObject.ApplyModifiedProperties();
        }

        public void ListIterator(string listName)
        {
            //List object
                    Debug.Log(listName.ToString());
            SerializedProperty listIterator = serializedObject.FindProperty(listName);
                    Debug.Log(listIterator.serializedObject.ToString());
            Rect drawZone = GUILayoutUtility.GetRect(0f, 16f);
                    //Debug.Log(drawZone);
            bool showChildren = EditorGUI.PropertyField(drawZone, listIterator);
                    //Debug.Log(showChildren);
            listIterator.NextVisible(showChildren);
                    Debug.Log(listIterator.ToString());

            //List size
            drawZone = GUILayoutUtility.GetRect(0f, 16f);
            showChildren = EditorGUI.PropertyField(drawZone, listIterator);
            bool toBeContinued = listIterator.NextVisible(showChildren);

            //Elements
            int listElement = 0;
            while (toBeContinued)
            {
                drawZone = GUILayoutUtility.GetRect(0f, 16f);
                showChildren = EditorGUI.PropertyField(drawZone, listIterator);
                toBeContinued = listIterator.NextVisible(showChildren);
                listElement++;
            }
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