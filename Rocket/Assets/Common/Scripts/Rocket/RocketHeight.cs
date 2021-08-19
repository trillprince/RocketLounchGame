using Common.Scripts;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class RocketHeight : MonoBehaviour
{
    [SerializeField] private float _height = 0;
    [SerializeField] private BGScroll _bg;
    private Vector2 _heightDir;

    private void OnEnable()
    {
        HeightText.Height += GetHeight;
    }

    private void OnDisable()
    {
        HeightText.Height -= GetHeight;
    }


    public float Height
    {
        get => _height;
        set => _height = value;
    }

    private void Awake()
    {
        _bg = FindObjectOfType<BGScroll>();
    }
    

    private void Update()
    {
        if (MovementStateController.RocketAutoMove)
        {
            HeightValueUpdate();
        }
    }

    private void HeightValueUpdate()
    {
        Height += _bg._yVelocity * RocketSpeed.CurrentSpeed * Time.deltaTime;
    }

    float GetHeight()
    {
        return Mathf.Floor(Height);
    }
    
}
