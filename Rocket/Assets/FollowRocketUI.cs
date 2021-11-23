using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Rocket;
using TMPro;
using UnityEngine;
using Zenject;

public class FollowRocketUI : MonoBehaviour
{ 
    private Transform _lookAt;
    private Vector3 _offset = new Vector3(2.3f,3f,0);
    private Camera _camera;
    private TextMeshProUGUI _textMesh;
    private RocketHealth _rocketRepairCount;
    private RocketDistance _rocketDistance;


    [Inject]
    private void Constructor(RocketController rocketController)
    {
        _lookAt = rocketController.GetComponentInChildren<Transform>();
        _rocketRepairCount = rocketController.Health;
        _rocketDistance = rocketController.CoveredDistance;
        _camera = Camera.main;
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateUIPos();
        UpdateText();
    }

    private void UpdateText()
    {
        _textMesh.text = $"{(int)_rocketDistance.CoveredDistance}";
    }

    private void UpdateUIPos()
    {
        Vector3 pos = _camera.WorldToScreenPoint(_lookAt.position + _offset);

        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
}