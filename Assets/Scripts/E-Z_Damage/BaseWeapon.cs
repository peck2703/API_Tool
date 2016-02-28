using UnityEngine;
using System.Collections;

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
    public class BaseWeapon : MonoBehaviour
    {
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
        // Use this for initialization
        public virtual void Start()
        {

        }

        // Update is called once per frame
        public virtual void Update()
        {

        }
    }
}
