using Common.Scripts.Rocket;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class BGScroll : MonoBehaviour
{
    private Material _material;
    private Vector2 _offset;
    [Range(-2, 2)] public float _xVelocity = 0;
    [Range(-2, 2)] public float _yVelocity;
    private RocketMovementController _rocketMovement;
    private bool _rocketLounched;
    [FormerlySerializedAs("_smoothness")] [SerializeField] private float _moveSmoothness = 100;

    [Inject]
    private void Construct(RocketMovementController rocketMovement)
    {
        _rocketMovement = rocketMovement;
    }
    
    private void OnEnable()
    {
        LounchManager.OnRocketLounch += LounchRocket;
    }

    private void OnDisable()
    {
        LounchManager.OnRocketLounch -= LounchRocket;
    }

    private void LounchRocket(bool isLounched)
    {
        _rocketLounched = isLounched;
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(_xVelocity, _yVelocity);
    }

    void Update()
    {
        if (!_rocketLounched)
        {
            return;
        }
        ScrollFromRocketDir();
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }


    private void ReinitializeOffset()
    {
        _offset = new Vector2(_xVelocity, _yVelocity).normalized * _rocketMovement.RocketSpeed/_moveSmoothness;
    }

    public void ScrollFromRocketDir()
    {
        _xVelocity = _rocketMovement.GetRocketDirection().x;
        _yVelocity = _rocketMovement.GetRocketDirection().y;
        Invoke("ReinitializeOffset",1f);;
    }
    
}