using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            CarPanretCode _car = FindObjectOfType<CarPanretCode>();
            _car.SaveToCheckPoint(gameObject);
        }
    }
}
