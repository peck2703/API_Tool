using UnityEngine;
using UnityEditor;
using System.Collections;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                                                               *
 *                                                               *
 *      Author: Michael Peck                                     *
 *      Date: 2/18/2016                                          *
 *                                                               *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
namespace API_TOOL
{
    public class DamageWindow : EditorWindow
    {
        public static string defaultChoice;

        [MenuItem("Window/E-Z Damage")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(DamageWindow), false, "E-Z Damage");
        }

        bool myBool = true;
        float myFloat = 1.23f;

        void OnEnable()
        {
            Repaint();
        }

        void OnGUI()
        {
            //Sets up a blank game object that handles the current active game object
            GameObject currentGO = Selection.activeGameObject;

            if (currentGO != null)
            {
                //Check if active GameObject has a BaseCharacter script object attached
                if (currentGO.GetComponent<BaseCharacter>())
                {
                    //Refresh the Window using the built-in function Repaint() that
                    Repaint();

                    /* Local variable that uses the public accessor from the BaseCharacter Class and stores it
                     * locally so the Window knows which set to display*/
                    string compareDefaultChoice =
                        Selection.activeGameObject.GetComponent<BaseCharacter>().DefaultChoice;

                    /* Check to see which DefaultChoice is assigned to the Base Character then 
                     * displays the appropriate List<> of objects that each Base Class have */
                    if (compareDefaultChoice == "Default")
                    {

                    }
                    /* Check to see which DefaultChoice is assigned to the Base Character then 
                     * displays the appropriate List<> of objects that each Base Class have */
                    else if (compareDefaultChoice == "Custom")
                    {

                    }
                }
                //Check if active GameObject has a BaseCharacter script object attached
                else if (currentGO.GetComponent<BaseWeapon>())
                {
                    Debug.Log("Has Base Weapon");
                    Repaint();
                    string compareDefaultChoice =
                        Selection.activeGameObject.GetComponent<BaseWeapon>().DefaultChoice;
                    Debug.Log("Window " + Selection.activeGameObject.GetComponent<BaseWeapon>().DefaultChoice);

                    Debug.Log("OnGUI: " + Selection.activeGameObject.GetComponent<BaseWeapon>().DefaultChoice);

                    myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
                    myBool = EditorGUILayout.Toggle("Toggle", myBool);
                }
                else if (currentGO.GetComponent<BaseObject>())
                {
                    Debug.Log("Has Base Object");
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
}
