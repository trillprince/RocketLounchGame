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
    private MovementStateController _movementStateMovementState;
    private bool _bgMove;
    [SerializeField] private float _moveSmoothness = 100;

    [Inject]
    private void Construct(MovementStateController movementStateMovementState)
    {
        _movementStateMovementState = movementStateMovementState;
    }

    private void OnEnable()
    {
        LaunchManager.OnRocketLounch += BgMove;
        LandingController.Landing += Landing;
    }

    private void OnDisable()
    {
        LaunchManager.OnRocketLounch -= BgMove;
        LandingController.Landing -= Landing;
    }

    private void Landing()
    {
        BgMove(false);
    }

    private void BgMove(bool isLounched)
    {
        _bgMove = isLounched;
    }

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _offset = new Vector2(_xVelocity, _yVelocity);
    }

    void Update()
    {
        if (!_bgMove)
        {
            return;
        }
        ScrollFromRocketDir();
        _material.mainTextureOffset += _offset * Time.deltaTime;
    }


    private void ReinitializeOffset()
    {
        _offset = new Vector2(_xVelocity, _yVelocity).normalized * RocketSpeed.CurrentSpeed/_moveSmoothness;
    }

    public void ScrollFromRocketDir()
    {
        _xVelocity = RocketSpeed.GetRocketDirection().x;
        _yVelocity = RocketSpeed.GetRocketDirection().y;
        Invoke("ReinitializeOffset",1f);;
    }
    
}