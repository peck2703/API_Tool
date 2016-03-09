using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * This will be the main entrance of the API for the E-Z Damage  *
 * Calculator. This class should handle all the class creation,  *
 * and the inspector side of the modifications should be handled *
 * by the DamageWindow.cs file.                                  *
 *                                                               *
 * Author: Michael Peck                                          *
 * Date: 2/18/2016                                               *
 *                                                               *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

namespace API_TOOL
{
    public class DamageCalculator : Editor
    {
        public GameObject testSubject;
        GameObject healthScript;
        

        [MenuItem("E-Z Damage/Create GameObject")]
        private static void CreateGameObject()
        {
            GameObject selectedGO = Selection.activeGameObject;
            int option = EditorUtility.DisplayDialogComplex("Select Type of Object",
                "Pick One of the Following Objects to Start",
                "Character/NPC",
                "Weapon",
                "Advanced/Custom");

            switch (option)
            {
                case 0:
                    if (selectedGO != null)
                    {
                        string choice;
                        //Create a character object by first populating a window
                        if (EditorUtility.DisplayDialog("Select Defaults for Character Class",
                            "Would You Like to Load the Default Settings for Characters, or Load Customs Settings?",
                            "Default",
                            "Custom"))
                        {
                            choice = "Default";

                            GameObject go = new GameObject("Character_EZ_Character");
                            go.transform.SetParent(selectedGO.transform);

                            BaseCharacter myBase = (BaseCharacter)go.AddComponent<BaseCharacter>();//.SetDefault(choice);
                            myBase.SetDefault(choice);
                        }
                        else
                        {
                            choice = "Custom";

                            GameObject go = new GameObject("MyCharacterGameObject");
                            go.transform.SetParent(selectedGO.transform);

                            BaseCharacter myBase = (BaseCharacter)go.AddComponent<BaseCharacter>();//.SetDefault(choice);
                            myBase.SetDefault(choice);
                        }
                    }
                    else
                        Debug.LogError("Object Not Selected");
                    break;
                case 1:
                    //Create a Weapon object by first populating a window
                    if (selectedGO != null)
                    {
                        //Create a character object by first populating a window

                        GameObject go = new GameObject();
                        go.transform.SetParent(selectedGO.transform);

                        go.AddComponent<BaseWeapon>();
                        go.name = selectedGO.name + "_EZ_Weapon";
                    }
                    else
                        Debug.LogError("Object Not Selected");
                    break;
                case 2:
                    /*Prompt another Dialog box complex to give other default 
                     * subclasses or the user can create his own object based
                     * on other subclasses made by the Developers*/
                    int secondary = EditorUtility.DisplayDialogComplex("Select Type of Advanced Object",
                       "Pick One of the Following Objects to Start or Choose a Similar base Class",
                       "Vehicle / Object",
                       "Elemental (Broken)",
                       "Back");
                    switch (secondary)
                    {
                        case 0:
                            //Create a Vehicle Object using a template of character
                            if (selectedGO != null)
                            {
                                //Create a character object by first populating a window

                                GameObject go = new GameObject("Vehicle");
                                go.transform.SetParent(selectedGO.transform);

                                go.AddComponent<BaseCharacter>();
                                go.name = selectedGO.name + "_EZ_Character";
                                go.GetComponent<BaseCharacter>().SetDefault("Custom");
                            }
                            else
                                Debug.LogError("Object Not Selected");
                            break;
                        case 1:
                            /*Option for expansion, can disable this for now, and at release
                             * re-enable it if we get that into the master build, else we will
                             * leave it for future reference or as an update if requested by users */
                            Debug.LogWarning("Feature Not Yet Established");
                            break;
                        case 2:
                            //Restart the dialog boxes so that it gets cleared
                            CreateGameObject();
                            break;
                    }
                    break;
            }
            //  MonoScript[] scripts = new (MonoScript[])Resources.FindObjectsOfTypeAll<MonoScript>;
            MonoScript[] scripts = Resources.FindObjectsOfTypeAll<MonoScript>();
            List<MonoScript> result = new List<MonoScript>();

            foreach (MonoScript m in scripts)
            {
                if (m.GetClass() != null && m.GetClass().IsSubclassOf(typeof(MonoScript)))
                {
                    result.Add(m);
                    Debug.Log(m.name);
                }
            }
            result.ToArray();
        }
    }
}
