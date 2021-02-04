using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasCount : MonoBehaviour
{
    private int Gas = 0;
    private Text GasText;
    
    void Start()
    {
        GasText = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        GasText.text = Gas.ToString();
    }

    public void IncreaseCoin(int coinPoint)
    {
        this.Gas += coinPoint;
    }
}
