using UnityEngine;
using UnityEditor;
using System.Collections;

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
  
public class DamageCalculator : Editor {

    [MenuItem("E-Z Damage/Create HelloWorld/Test")]
    private static void CreateGameObject()
    {
        int option = EditorUtility.DisplayDialogComplex("Select Type of Object",
            "Pick One of the Following Objects to Start",
            "Character/NPC",
            "Weapon",
            "Advanced/Custom");
        switch(option)
        {
            case 0:
                //Create a character object by first populating a window
                break;
            case 1:
                //Create a Weapon object by first populating a window
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
                switch(secondary)
                {
                    case 0:
                        //Create a Vehicle Object using a template of character
                        break;
                    case 1:
                        /*Option for expansion, can disable this for now, and at release
                         * re-enable it if we get that into the master build, else we will
                         * leave it for future reference or as an update if requested by users */
                        break;
                    case 2:
                        //Restart the dialog boxes so that it gets cleared
                        CreateGameObject();
                        break;
                }
            break;
        }
    }
}
