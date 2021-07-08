using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Firebase;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void OnEnable()
    {
        FirebaseInit.FirebaseInited += LoadLevel;
    }

    private void OnDisable()
    {
        FirebaseInit.FirebaseInited -= LoadLevel;
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(1);
    }
    
}
