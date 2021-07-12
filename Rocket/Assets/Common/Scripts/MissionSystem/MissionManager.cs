using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        [SerializeField] private MissionInfo _currentMissionInfo;
        private RocketHeight _rocketHeight;
        private int _currentCargoIndex = 0;
        private int _cargoCount;
        private int _lateDropHeight = 50;
        private int _perfectDropHeight = 150;

        public delegate void Mission (bool readyToDrop);

        public static event Mission TimeToDrop;

        private void Start()
        {
            _cargoCount = _currentMissionInfo.CargoHignessList.Count;
        }

        private void OnEnable()
        {
            DropCargoButton.CargoDropped += CargoDrop;
        }

        private void OnDisable()
        {
            DropCargoButton.CargoDropped -= CargoDrop;
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
            int cargoHight = _currentMissionInfo.CargoHignessList[_currentCargoIndex];
            float rocketHeight = _rocketHeight.Height;
            if (cargoHight - rocketHeight < _perfectDropHeight &&
                rocketHeight - cargoHight < _lateDropHeight)
            {
                TimeToDrop?.Invoke(true);
            }
            else if (cargoHight - rocketHeight < _perfectDropHeight &&
                     rocketHeight - cargoHight > _lateDropHeight)
            {
                TimeToDrop?.Invoke(false);
                _currentCargoIndex++;
                Debug.Log(_currentCargoIndex);
            }
            else
            {
                TimeToDrop?.Invoke(false);
            }
            
        }
        void CargoDrop()
        {
            _currentCargoIndex++;
        }
        
        
        
        
        
    }
}
