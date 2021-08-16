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
    [SerializeField] private RocketMovementController rocketMovement;
    private Vector2 _heightDir;
    private bool _rocketLounched;

    private void OnEnable()
    {
        LounchManager.OnRocketLounch += LounchRocket;
        HeightText.Height += GetHeight;
    }

    private void OnDisable()
    {
        LounchManager.OnRocketLounch -= LounchRocket;
        HeightText.Height -= GetHeight;
    }

    private void LounchRocket(bool isLounched)
    {
        _rocketLounched = isLounched;
    }

    public float Height
    {
        get => _height;
        set => _height = value;
    }

    private void Awake()
    {
        _bg = FindObjectOfType<BGScroll>();
        rocketMovement = GetComponent<RocketMovementController>();
    }
    

    private void Update()
    {
        if (!_rocketLounched)
        {
            return;
        }
        HeightValueUpdate();
    }

    private void HeightValueUpdate()
    {
        Height += _bg._yVelocity * rocketMovement.RocketSpeed * Time.deltaTime;
    }

    float GetHeight()
    {
        return Mathf.Floor(Height);
    }
    
}
