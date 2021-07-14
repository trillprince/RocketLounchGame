using System;
using System.Collections.Generic;
using Common.Scripts.UI.InGame;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        [SerializeField] private MissionInfo _currentMissionInfo;
        private bool [] _cargosDropped;
        private List<int> _cargoHeightList;
        private RocketHeight _rocketHeight;
        private int _currentCargoIndex = 0;
        private int _cargoCount;
        private int _lateDropHeight = 50;
        private int _perfectDropHeight = 150;

        public delegate void Mission (bool readyToDrop);

        public static event Mission TimeToDrop;


        public delegate void Cargo(GameObject cargo);

        public static event Cargo SetCargo;
        

        private void Start()
        {
            _cargoCount = _currentMissionInfo.CargoHightList.Count;
            _cargosDropped = new bool[_cargoCount];
            _cargoHeightList = _currentMissionInfo.CargoHightList;
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
            _currentMissionInfo = missionInfo;
        }

        private void Update()
        {
            DropTimeCheck();
        }

        void DropTimeCheck()
        {
            float rocketHeight = _rocketHeight.Height;
            if (_currentCargoIndex < _cargoCount && !_cargosDropped[_currentCargoIndex])
            {
                if (_cargoHeightList[_currentCargoIndex] - rocketHeight < _perfectDropHeight &&
                    rocketHeight - _cargoHeightList[_currentCargoIndex] < _lateDropHeight)
                {
                    TimeToDrop?.Invoke(true);
                    SetCargo?.Invoke(_currentMissionInfo.CargoList[_currentCargoIndex]);
                }
                else if (_cargoHeightList[_currentCargoIndex] - rocketHeight < _perfectDropHeight &&
                         rocketHeight - _cargoHeightList[_currentCargoIndex] > _lateDropHeight)
                {
                    UpdateCargoStatus();
                }
            }
        }
        void UpdateCargoStatus()
        {
            _cargosDropped[_currentCargoIndex] = true;
            _currentCargoIndex++;
            TimeToDrop?.Invoke(false);
        }
       
    }
}
