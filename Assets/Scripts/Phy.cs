using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phy : MonoBehaviour
{
    public float gravityScale = 1.0f;

    public static float globalGravity = -9.81f;
 
    Rigidbody car;
 
    void OnEnable ()
    {
        car = GetComponent<Rigidbody>();
        car.useGravity = false;
    }
 
    void FixedUpdate ()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        car.AddForce(gravity, ForceMode.Acceleration);
    }
}
