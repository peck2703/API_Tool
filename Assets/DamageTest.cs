using UnityEngine;
using System.Collections;

public class DamageTest : MonoBehaviour {
    float dmg;

    public float Damage {
        get { return dmg; }
        set { dmg = value; }
    }

    // For on Collion to read, both objects need a collider.
    // At least one of the objects need a non- kinematic rigidbody.
    void OnCollisionEnter(Collision col) {
        Debug.Log(col.gameObject.name);
    }

    void Update() {
        
    }
}
