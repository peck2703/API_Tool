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
namespace API_TOOL{
    public class DamageWindow : EditorWindow{
        public static string defaultChoice;

        [MenuItem("Window/E-Z Damage")]
        public static void ShowWindow(){
            EditorWindow.GetWindow(typeof(DamageWindow), false, "E-Z Damage");
        }

        bool myBool = true;
        float myFloat = 1.23f;

        void OnEnable(){
            Repaint();
            //Debug.Log("OnEnable: " + Selection.activeGameObject.GetComponent<BaseCharacter>().DefaultChoice);
        }

        /*public void OnInspectorUpdate(){
            // This will only get called 10 times per second.
            if (Selection.activeGameObject.GetComponent<BaseCharacter>())
                Repaint();
        }*/

        void OnGUI(){
            //Check if active GameObject has a BaseCharacter script object attached
            if (Selection.activeGameObject.GetComponent<BaseCharacter>()){
                
                //Refresh the Window using the built-in function Repaint() that
                Repaint();

                /* Local variable that uses the public accessor from the BaseCharacter Class and stores it
                 * locally so the Window knows which set to display*/
                string compareDefaultChoice =
                    Selection.activeGameObject.GetComponent<BaseCharacter>().DefaultChoice;

                /* Check to see which DefaultChoice is assigned to the Base Character then 
                 * displays the appropriate List<> of objects that each Base Class have */
                if (compareDefaultChoice == "Default"){
                    myBool = EditorGUILayout.Toggle("Toggle", myBool);
                    myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
                }
                /* Check to see which DefaultChoice is assigned to the Base Character then 
                 * displays the appropriate List<> of objects that each Base Class have */
                else if (compareDefaultChoice == "Custom"){
                    myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
                    myBool = EditorGUILayout.Toggle("Toggle", myBool);
                }
            }
            //Check if active GameObject has a BaseCharacter script object attached
            else if (Selection.activeGameObject.GetComponent<BaseWeapon>()){
                Debug.Log("Has Base Weapon");
                Repaint();
                string compareDefaultChoice =
                    Selection.activeGameObject.GetComponent<BaseWeapon>().DefaultChoice;
                Debug.Log("Window " + Selection.activeGameObject.GetComponent<BaseWeapon>().DefaultChoice);

                Debug.Log("OnGUI: " + Selection.activeGameObject.GetComponent<BaseWeapon>().DefaultChoice);
                
                myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
                myBool = EditorGUILayout.Toggle("Toggle", myBool);
            }
            else if (Selection.activeGameObject.GetComponent<BaseObject>())
            {
                Debug.Log("Has Base Weapon");
                Repaint();
                string compareDefaultChoice =
                    Selection.activeGameObject.GetComponent<BaseObject>().DefaultChoice;
                Debug.Log("Window " + Selection.activeGameObject.GetComponent<BaseObject>().DefaultChoice);

                Debug.Log("OnGUI: " + Selection.activeGameObject.GetComponent<BaseObject>().DefaultChoice);
                myBool = EditorGUILayout.Toggle("Toggle", myBool);
                myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            }
        }
    }
}
