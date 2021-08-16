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
    [SerializeField] private RocketControl rocketMovement;
    private Vector2 _heightDir;
    private bool _rocketLounched;

    private void OnEnable()
    {
        RocketControl.RocketMoving += IsMoving;
        HeightText.Height += GetHeight;
    }

    private void OnDisable()
    {
        RocketControl.RocketMoving -= IsMoving;
        HeightText.Height -= GetHeight;
    }

    private void IsMoving(bool isMoving)
    {
        _rocketLounched = isMoving;
    }

    public float Height
    {
        get => _height;
        set => _height = value;
    }

    private void Awake()
    {
        _bg = FindObjectOfType<BGScroll>();
        rocketMovement = GetComponent<RocketControl>();
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
