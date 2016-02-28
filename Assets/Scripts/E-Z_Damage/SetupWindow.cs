using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace API_TOOL
{
    public class SetupWindow : EditorWindow
    {
        [MenuItem("Window/E-Z Setup")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SetupWindow), false, "Set-Up");
        }

        List<GameObject> limbs;
        public GameObject obj = null;
        GameObject unsavedGO;

        float buttonHeightOffset = 27.0f;
        string defaultChoice;
        int countPerLimb;

        /* Public accessor property for the Editor window to review the choice for each
         * cloned copy of this script. This can only be set upon construction*/
        public string DefaultChoice
        {
            get { return defaultChoice; }
        }

        //Public access method to set the default value when it is constructed in the DamageCalculatorClass.cs
        public void SetDefault(string DefaultChoice)
        {
            defaultChoice = DefaultChoice;
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
                    //Access the default/custom string to determine the setup of the Inspector
                    if (currentGO.GetComponent<BaseCharacter>().DefaultChoice == "Default")
                    {
                        for (int i = 0; i < countPerLimb; i++)
                        {
                            EditorGUI.ObjectField(new Rect(2, 2+ (i * buttonHeightOffset), position.width, 25), "Head(s)", unsavedGO, typeof(GameObject), true);
                        }
                        if (GUI.Button(new Rect(2, 29, 75, 25), "Add Head"))
                        {
                            countPerLimb++;
                        }
                        //currentGO.GetComponent<BaseCharacter>().EZ_BodyHead.Add(unsavedGO.gameObject);
                    }
                }
            }
        }
    }
}
