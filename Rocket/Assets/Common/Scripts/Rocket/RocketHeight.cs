using Common.Scripts;
using Common.Scripts.MissionSystem;
using Common.Scripts.Rocket;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketHeight : MonoBehaviour
{
    [SerializeField] private float _height = 0;
    [SerializeField] private BGScroll _bg;
    [SerializeField] private RocketMovement _rocket;
    private Vector2 _heightDir;
    private bool _rocketLounched;

    private void OnEnable()
    {
        LounchManager.MiddleEngineEnable += LounchRocket;
        HeightText.Height += GetHeight;
    }

    private void OnDisable()
    {
        LounchManager.MiddleEngineEnable -= LounchRocket;
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
        _rocket = GetComponent<RocketMovement>();
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
        Height += _bg._yVelocity * _rocket.RocketSpeed * Time.deltaTime;
    }

    float GetHeight()
    {
        return Mathf.Floor(Height);
    }
    
}
