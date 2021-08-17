using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LounchPad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rocket"))
        {
            Debug.Log("aye");
        }
    }
}
