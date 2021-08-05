using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoDropController : MonoBehaviour
    {
        private bool _dropReady;
        private List<DropAccuracy> _dropAccuratenesses;

        private MissionManager _missionManager;
        
        public delegate void DropCargo();
        public static event DropCargo CargoDropping;
        public static event DropCargo OnCargoDrop;

        public delegate void DropStatusSender();

        public static event DropStatusSender OnGetStartDropStatus;

        bool DropReady
        {
            get => _dropReady;
            set => _dropReady = value;
        }


        private void Awake()
        {
            _missionManager = GetComponent<MissionManager>();
        }
        

        private void OnEnable()
        {
            MissionManager.TimeToDrop += CheckDropStatus;
            InputManager.OnTouchStartEvent += DropOnTouch;
            CargoDropSlider.OnGetDropAccuracy += SetDropAccuracy;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= CheckDropStatus;
            InputManager.OnTouchStartEvent -= DropOnTouch;
            CargoDropSlider.OnGetDropAccuracy -= SetDropAccuracy;
        }

        private void Start()
        {
            _dropAccuratenesses =
                new List<DropAccuracy>(_missionManager.CargoCount);
        }

        void CheckDropStatus(DropStatus dropStatus)
        {
            if (dropStatus == DropStatus.Start)
            {
                OnGetStartDropStatus?.Invoke();
                DropReady = true;
            }
            else if(dropStatus == DropStatus.Waiting)
            {
                
            }
            else if (dropStatus == DropStatus.End)
            {
                
            }
        }

        void DropOnTouch(Vector2 touchPos)
        {
            if (DropReady)
            {
                CargoDropping?.Invoke();
                DropReady = false;
            }
        }

        void SetDropAccuracy(DropAccuracy accuracy)
        {
            _dropAccuratenesses.Add(accuracy);
            OnCargoDrop?.Invoke();
        }
    }
    public enum DropAccuracy
    {
        NoInteraction,
        NotGood,
        Nice,
        Perfect
    }
}
