using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *                                                               *
 *                                                               *
 * Author: Michael Peck                                          *
 * Date: 2/18/2016                                               *
 *                                                               *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class DamageCalculatiorInspector : Editor {
#if UNITY_EDITOR
    [MenuItem ("E-Z Damage/Create HelloWorld/")]
    [MenuItem ("E-Z Damage/Create AwesomeSauce")]
    
    /*private static void CreateHelloWorldGameObject()
    {
        if (EditorUtility.DisplayDialog("Hello World","Do you really want to do this?","Create","Cancel"))
        {
            new GameObject("Hello World");
        }
    }*/
#endif
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        //May Require the Weapon and the character classes to be used as the serialized properties



        //EditorGUILayout.PropertyField();
        //if (lookAtPoint.vector3Value.y > (target as LookAtPoint).transform.position.y)
        //{
            EditorGUILayout.LabelField("(Above this object)");
        //}
        //if (lookAtPoint.vector3Value.y < (target as LookAtPoint).transform.position.y)
        //{
            EditorGUILayout.LabelField("(Below this object)");
        //}


        serializedObject.ApplyModifiedProperties();
    }
}
