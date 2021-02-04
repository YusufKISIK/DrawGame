using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            CarPanretCode _car = FindObjectOfType<CarPanretCode>();
            _car.InstantiateCheckpoint();
        }
    }
}
