using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Input;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RocketLandingController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _curFuel;
    [SerializeField] private float _thrustForce;
    private bool _isLanding;

    private void Awake()
    {
        _curFuel = _maxFuel;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.OnTouchStartEvent += Landing;
    }

    void Landing(Vector2 touchPos)
    {
        if (_curFuel > 0)
        {
            _curFuel -= Time.deltaTime;
            _rb.AddForce(_rb.transform.up * _thrustForce * touchPos,ForceMode.Impulse);
        }
        else if (Physics.Raycast(transform.position, Vector3.down, 0.05f, LayerMask.GetMask("Ground")))
        {
            _curFuel += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aye");
    }
}
