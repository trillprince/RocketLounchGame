using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class GameLoopController : MonoBehaviour
    {
        private int _currentCargoIndex = 0;
        private int _cargoCount;
        private DropStatus _currentDropStatus = DropStatus.Waiting;
        private float _delayBeforeDrop = 4;
        [SerializeField] private GameObject _cargo;
        private ISatelliteFactory _satelliteFactory;

        public delegate void Mission (DropStatus dropStatus);

        public static event Mission TimeToDrop;
        
        public delegate void Cargo(GameObject cargo);

        public static event Cargo SetCargo;

        public static event Action OnOutOfCargo;


        private DropStatus CurrentDropStatus
        {
            get => _currentDropStatus;
            set => _currentDropStatus = value;
        }

        private void OnEnable()
        {
            CargoDropController.OnCargoDrop += UpdateCargoStatus;
            GameStateController.OnStateSwitch += GameStateListener;
        }

        private void OnDisable()
        {
            CargoDropController.OnCargoDrop -= UpdateCargoStatus;
            GameStateController.OnStateSwitch -= GameStateListener;
        }

        private void Constructor(BasicSatelliteFactory satelliteFactory)
        {
            _satelliteFactory = satelliteFactory;
        }

        void UpdateCargoStatus()    
        {
            _currentCargoIndex++;
            DropEventInvoker(DropStatus.End);
            if (_currentCargoIndex == _cargoCount)
            {
                OnOutOfCargo?.Invoke();
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
            SetCargo?.Invoke(_cargo);
            yield return new WaitForSeconds(_delayBeforeDrop);
            DropEventInvoker(DropStatus.Start);
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                StartCoroutine(DropStart());
            }
        }

    }

    public enum DropStatus
    {
        Waiting,
        Start,
        End
    }
    
}
