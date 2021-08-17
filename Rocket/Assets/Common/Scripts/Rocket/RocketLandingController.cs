using Common.Scripts.Input;
using UnityEngine;

public class RocketLandingController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _thrustForce;
    private int _destroySpeed = 7;
    private Vector2 _touchPos;
    private Vector2 _moveToVec;
    private bool _isHeld;
    private bool _landingReady;
    private bool _landingDone;
    private readonly float _maxRayDistance = 0.3f;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        InputManager.OnTouchHold += TouchHold;
        InputManager.OnTouchHoldEnd += StopTouchHold; 
        RocketControl.Landing += RocketControlOnLanding;
    }

    private void OnDisable()
    {
        InputManager.OnTouchHold -= TouchHold;
        InputManager.OnTouchHoldEnd -= StopTouchHold;
        RocketControl.Landing -= RocketControlOnLanding;
    }

    private void RocketControlOnLanding()
    {
        _rb.isKinematic = false;
        _landingReady = true;
    }

    private void FixedUpdate()
    {
        if (_isHeld)
        {
            Flying();
        }
        if (_landingReady)
        {
            GroundCheck();
        }
    }

    void TouchHold(Vector2 touchPos)
    {
        _touchPos = touchPos;
        _isHeld = true;
    }

    void StopTouchHold()
    {
        _isHeld = false;
    }

    void Flying()
    {
        int touchPart = 0;
        if (_touchPos.x < Screen.width / 2)
        {
            touchPart = 1;
        }
        else
        {
            touchPart = -1;
        }
        _moveToVec = new Vector3(touchPart, 0.8f);
        _rb.AddForce(_moveToVec * _thrustForce, ForceMode.Impulse);
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit,_maxRayDistance))
        {
            if(_landingDone) return;
            bool onPad = hit.collider.CompareTag("LounchPad");
            bool crashing = _rb.velocity.magnitude >= _destroySpeed;
            if (onPad && !crashing)
            {
                Debug.Log("you won");
                _landingDone = true;
            }
            else if (hit.collider != null)
            {
                Debug.Log("you crashed");
                _landingDone = true;
            }
        }
    }
}