using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LounchManager : MonoBehaviour
{

    public delegate void Station();

    public static event Station RocketLounch;
    

    public void LounchTheRocket()
    {
        
        RocketLounch?.Invoke();
    }
}
