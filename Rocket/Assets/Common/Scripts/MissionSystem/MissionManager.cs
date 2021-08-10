using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        private const float _delayDecreaseStep = 0.3f;
        private int _currentCargoIndex = 0;
        private int _cargoCount = 1;
        private DropStatus _currentDropStatus = DropStatus.Waiting;
        private float _delayBeforeDrop = 4 + _delayDecreaseStep;

        private DropStatus CurrentDropStatus
        {
            get => _currentDropStatus;
            set => _currentDropStatus = value;
        }

        public int CargoCount
        {
            get => _cargoCount;
            set => _cargoCount = value;
        }

        public delegate void Mission (DropStatus dropStatus);

        public static event Mission TimeToDrop;
        
        public delegate void Cargo(GameObject cargo);

        public static event Cargo SetCargo;

        public static event Action Landing; 

        private void OnEnable()
        {
            CargoDropController.OnCargoDrop += UpdateCargoStatus;
            LounchManager.MiddleEngineEnable += engineEnabled =>
            {
                StartCoroutine(DropStart());
            };
        }

        private void OnDisable()
        {
            CargoDropController.CargoDropping -= UpdateCargoStatus;
        }

        void UpdateCargoStatus()
        {
            _currentCargoIndex++;
            DropEventInvoker(DropStatus.End);
            if (_currentCargoIndex >= _cargoCount)
            {
                Landing?.Invoke();
                return;
            }
            StartCoroutine(DropStart());
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

        private IEnumerator DropStart()
        {
            /*SetCargo?.Invoke(currentMissionInfo.CargoList[_currentCargoIndex]);*/
            _delayBeforeDrop -= _delayDecreaseStep;
            yield return new WaitForSeconds(_delayBeforeDrop);
            DropEventInvoker(DropStatus.Start);
        }

    }
    
    public enum DropStatus
    {
        Waiting,
        Start,
        End
    }
    
}
