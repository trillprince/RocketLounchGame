using System;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.InGame
{
    public class CargoDropListener : MonoBehaviour
    {
        public delegate void DropCargo();
        public static event DropCargo CargoDropped;
        
        private bool _dropReady;
        private CargoDropSlider _cargoDropSlider;
        private List<CargoDropSlider.DropAccurateness> _dropAccuratenesses;
        private MissionManager _missionManager;

        private void Awake()
        {
            _cargoDropSlider = FindObjectOfType<CargoDropSlider>();
            _missionManager = GetComponent<MissionManager>();
        }
        

        private void OnEnable()
        {
            MissionManager.TimeToDrop += DropReady;
            InputManager.OnTouchStartEvent += DropOnOnTouch;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= DropReady;
            InputManager.OnTouchStartEvent -= DropOnOnTouch;
        }

        private void Start()
        {
            _dropAccuratenesses =
                new List<CargoDropSlider.DropAccurateness>(_missionManager.CargoCount);
        }

        void DropReady(bool isReady)
        {
            _dropReady = isReady;
        }

        void DropOnOnTouch(Vector2 touchPos)
        {
            if (_dropReady)
            {
                CargoDropped?.Invoke();
                CheckDropAccurateness(_cargoDropSlider.GetDropAccurateness());
            }
        }

        void CheckDropAccurateness(CargoDropSlider.DropAccurateness dropAccurateness)
        {
            _dropAccuratenesses.Add(dropAccurateness);
        }

        public CargoDropSlider.DropAccurateness GetLastAccurateness()
        {
            if(_dropAccuratenesses.Count>0)
            {
                return _dropAccuratenesses[_dropAccuratenesses.Count - 1];
            }
            return default;
        }
    
    
    }
}
