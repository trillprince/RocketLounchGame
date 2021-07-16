using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoLifeCycle : MonoBehaviour
{
    private float _timeTillDestroy = 4f;

    private void Start()
    {
        StartCoroutine(WaitTillDestroy());
    }

    IEnumerator WaitTillDestroy()
    {
        yield return new WaitForSeconds(_timeTillDestroy);
        Destroy(gameObject);
    }
}
