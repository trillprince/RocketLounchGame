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
        private int _endDropHeight = 300;
        private int _startDropHeight = 10;
        private DropStatus _currentDropStatus = DropStatus.Waiting;

        private DropStatus CurrentDropStatus
        {
            get => _currentDropStatus;
            set => _currentDropStatus = value;
        }

        public enum DropStatus
        {
            Waiting,
            Start,
            End
        }
        
        public delegate void Mission (DropStatus dropStatus);

        public static event Mission TimeToDrop;


        public delegate void Cargo(GameObject cargo);

        public static event Cargo SetCargo;

        public int CargoCount => _cargoCount;

       


        private void Start()
        {
            _cargoCount = currentMissionInfo.CargoHeightList.Count;
            _cargosDropped = new bool[CargoCount];
            _cargoHeightList = currentMissionInfo.CargoHeightList;
        }
        private void OnEnable()
        {
            CargoDropListener.CargoDropped += UpdateCargoStatus;
        }

        private void OnDisable()
        {
            CargoDropListener.CargoDropped -= UpdateCargoStatus;
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
            if (!_cargosDropped[_currentCargoIndex] && _cargoHeightList[_currentCargoIndex] - rocketHeight < _startDropHeight )
            {
                if ( rocketHeight - _cargoHeightList[_currentCargoIndex] < _endDropHeight)
                {
                    DropEventInvoker(DropStatus.Start);
                    SetCargo?.Invoke(currentMissionInfo.CargoList[_currentCargoIndex]);
                }
                else if (rocketHeight - _cargoHeightList[_currentCargoIndex] > _endDropHeight)
                {
                    UpdateCargoStatus();
                }
            }
        }
        void UpdateCargoStatus()
        {
            _cargosDropped[_currentCargoIndex] = true;
            if (CargoCount - _currentCargoIndex > 1)
            {
                _currentCargoIndex++;
            }
            DropEventInvoker(DropStatus.End);
        }

        void DropEventInvoker(DropStatus dropStatusToSet)
        {
            if (dropStatusToSet == CurrentDropStatus)
            {
                return;
            }
            CurrentDropStatus = dropStatusToSet;
            TimeToDrop?.Invoke(CurrentDropStatus);
        }
    }
}
