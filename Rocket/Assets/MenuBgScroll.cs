using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Background;
using Common.Scripts.Planet;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MenuBgScroll : MonoBehaviour
{
    private List<IMovableEnvironment> _movableEnvironments;
    private Material _material;
    private Vector2 _offset;
    [SerializeField] private float _smoothness;

    private void Awake()
    {
        _material = GetComponent<Image>().material;
    }

    void Update()
    {
        _offset = new Vector2(0.3f, 1).normalized / _smoothness;
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }
}
