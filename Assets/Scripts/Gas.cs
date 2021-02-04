using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
 private int galon = 1;

    GasCount _gasCountCounter;
    
    void Start()
    {
        _gasCountCounter = FindObjectOfType<GasCount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            _gasCountCounter.IncreaseCoin(galon);
            Destroy(gameObject);
        }
    }
}
