using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * This will be a base class that will create a class that will  *
 * derive from this class and will be easier for us to control   *
 * the variables that need to communicate between the damage     *
 * scripts. This goes also for the rest of the base classes      *
 *                                                               *
 * Author: Michael Peck                                          *
 * Date: 2/18/2016                                               *
 *                                                               *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

namespace API_TOOL
{
    public class BaseCharacter : MonoBehaviour
    {
        [HideInInspector]
        public float EZ_baseDamage;
        [HideInInspector]
        public List<Transform> EZ_BodyParts;            // or Transform[] ...This will grab the collider.
        [HideInInspector]
        public float EZ_Multiplier;                     // Multiplier will change based on which Body Part it is.
        [HideInInspector]
        public bool EZ_FallingDamageBool;              // Whether or not Falling Damage is activated.
        [HideInInspector]
        public bool EZ_InstantKill;                    // Whether or not Instant Kill is activated.
        [HideInInspector]
        public int EZ_Health; // What is this number?  // The Starting Health
        [HideInInspector]
        public float EZ_FallingDamage;                 // Base Falling Damage.


        /* Are we doing Array of body parts or listing them out? Or default one and custom the other?*/
        [HideInInspector]
        public GameObject/*List<GameObject>*/ EZ_BodyHead;          //= 1.8f;
        [HideInInspector]
        public GameObject/*List<GameObject>*/ EZ_BodyTorso;         //= 1.2f;
        [HideInInspector]
        public GameObject/*List<GameObject>*/ EZ_BodyRightArm;      //= 0.8f;
        [HideInInspector]
        public GameObject/*List<GameObject>*/ EZ_BodyLeftArm;       //= 0.8f;         // Upper vs Lower arm omitted because research states that the values are equal.
        [HideInInspector]
        public GameObject/*List<GameObject>*/ EZ_BodyRightLeg;      //= 0.8f;
        [HideInInspector]
        public GameObject/*List<GameObject>*/ EZ_BodyLeftLeg;       //= 0.8f;     
        [HideInInspector]
        public GameObject/*List<GameObject>*/ BodyUpperRightLeg;    //= 1.04f;        // We should consider condensing left and right of each extremity.
        [HideInInspector]
        public GameObject/*List<GameObject>*/ BodyUpperLeftLeg;     //= 1.04f;
        [HideInInspector]
        public GameObject/*List<GameObject>*/ BodyLowerRightLeg;    //= 1.03f;  
        [HideInInspector]
        public GameObject[]/*List<GameObject>*/ BodyLowerLeftLeg;     //= 1.03f;

        //Setting them up as base values...based on the average dmg rate from Anthony's research.

        [HideInInspector]
        public float EZ_BodyHeadRate = 1.8f;
        [HideInInspector]
        public float EZ_BodyTorsoRate = 1.2f;
        [HideInInspector]
        public float EZ_BodyRightArmRate = 0.8f;
        [HideInInspector]
        public float EZ_BodyLeftArmRate = 0.8f;         // Upper vs Lower arm omitted because research states that the values are equal.
        [HideInInspector]
        public float EZ_BodyRightLegRate = 0.8f;
        [HideInInspector]
        public float EZ_BodyLeftLegRate = 0.8f;
        [HideInInspector]
        public float EZ_BodyUpperRightLegRate = 1.04f;        // We should consider condensing left and right of each extremity.
        [HideInInspector]
        public float EZ_BodyUpperLeftLegRate = 1.04f;
        [HideInInspector]
        public float EZ_BodyLowerRightLegRate = 1.03f;
        [HideInInspector]
        public  float EZ_BodyLowerLeftLegRate = 1.03f;

        string defaultChoice;

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

        public BaseCharacter(GameObject attached, List<Transform> limbs, List<float> mulitplier)
        {
            attached = this.transform.parent.gameObject;

        }

        /* For the Default Selection:
        ** 
        ** The user will input the body parts name (in the array?),
        ** if the variable is empty, it is null.
        ** We assign the correct damage rate to the body part,
        ** Check if the body part has been hit, if so...
        ** This class may be called from another script.
        ** Check the name of the hit gameobject
        ** EZ_Multiplier = hit object multiplier
        ** health -= EZ_baseDamage * EZ_Mulitplier.
        ** reset muliplier.


        ** For the Addition of a body part:

        ** Probably easier to have a list, since adding and removing are easier
        ** Add to the gameObject list...if possible to name the added element, we should consider that
        ** Have them add a multiplier, maybe have text explaining the range of the multiplier.
        ** We should consider clamping the range.
        ** Check if the body part has been hit, if so...
        ** This class may be called from another script.
        ** Check the name of the hit gameobject
        ** EZ_Multiplier = hit object multiplier
        ** health -= EZ_baseDamage * EZ_Mulitplier.
        ** reset EZ_Multiplier.


        ** For an Falling damage:

        ** If bool is selected,
        ** Assign maximum height for fall damage before death.
        ** Ask for multiplier, maybe have text explaining the range of the multiplier.
        ** Need to talk about how to detect if player is falling and the distance of the fall.
        ** health -= EZ_FallingDamageBase * EZ_Multiplier.
        ** Reset EZ_Multiplier.

        ** For Instant Kill:

        ** If bool is selected,
        ** If any body part on the list is hit
        ** Call Death script
        ** We need to make a death script...or set health to 0;
        ** Perhaps allow user to give their death script...or input method name and SendMessage.DontRequireReceiver("");


        ** For Object Damage:
        
        ** Same flow as Player damage, but we should have another name for health...durability?
        
        
        ** For Weapon Damage:
        
        ** Add another multiplier that increases the rate of damage
        ** Based on how powerful the weapon is.
        ** flow is the same as player damage. 
        */
    }
}
