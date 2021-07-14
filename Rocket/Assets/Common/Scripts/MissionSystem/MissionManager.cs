using System;
using System.Collections.Generic;
using Common.Scripts.UI.InGame;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class MissionManager : MonoBehaviour
    { 
        [SerializeField] private MissionInfo currentMissionInfo;
        private bool [] _cargosDropped;
        private List<int> _cargoHeightList;
        private RocketHeight _rocketHeight;
        private int _currentCargoIndex = 0;
        private int _cargoCount;
        private int _lateDropHeight = 50;
        private int _perfectDropHeight = 50;

        public delegate void Mission (bool readyToDrop);

        public static event Mission TimeToDrop;


        public delegate void Cargo(GameObject cargo);

        public static event Cargo SetCargo;
        

        private void Start()
        {
            _cargoCount = currentMissionInfo.CargoHeightList.Count;
            _cargosDropped = new bool[_cargoCount];
            _cargoHeightList = currentMissionInfo.CargoHeightList;
        }
        private void OnEnable()
        {
            DropCargoButton.CargoDropped += UpdateCargoStatus;
        }

        private void OnDisable()
        {
            DropCargoButton.CargoDropped -= UpdateCargoStatus;
        }

        [Inject]
        public void Constructor(RocketHeight rocketMovement)
        {
            _rocketHeight = rocketMovement;
        }
        
        public void MissionInfo(MissionInfo missionInfo)
        {
            currentMissionInfo = missionInfo;
        }

        private void Update()
        {
            DropTimeCheck();
        }

        void DropTimeCheck()
        {
            float rocketHeight = _rocketHeight.Height;
            if (!_cargosDropped[_currentCargoIndex] && _cargoHeightList[_currentCargoIndex] - rocketHeight < _perfectDropHeight )
            {
                if ( rocketHeight - _cargoHeightList[_currentCargoIndex] < _lateDropHeight)
                {
                    TimeToDrop?.Invoke(true);
                    SetCargo?.Invoke(currentMissionInfo.CargoList[_currentCargoIndex]);
                }
                else if (rocketHeight - _cargoHeightList[_currentCargoIndex] > _lateDropHeight)
                {
                    UpdateCargoStatus();
                }
            }
        }
        void UpdateCargoStatus()
        {
            _cargosDropped[_currentCargoIndex] = true;
            if (_cargoCount - _currentCargoIndex > 1)
            {
                _currentCargoIndex++;
            }
            TimeToDrop?.Invoke(false);
        }
       
    }
}
