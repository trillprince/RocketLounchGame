using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Rocket;
using UnityEngine;
using Zenject;

namespace Common.Scripts.MissionSystem
{
    public class DropStatusController : MonoBehaviour
    {
        private const float _delayDecreaseStep = 0.3f;
        private int _currentCargoIndex = 0;
        private int _cargoCount;
        [SerializeField] private MissionModelViewer _missionModelViewer;
        private DropStatus _currentDropStatus = DropStatus.Waiting;
        private float _delayBeforeDrop = 4 + _delayDecreaseStep;

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

        public int CargoCount
        {
            get => _cargoCount;
            set => _cargoCount = value;
        }

        private void OnEnable()
        {
            CargoDropController.OnCargoDrop += UpdateCargoStatus;
            CargoDropController.OnGetAccuracy += SetModelAccuracy;
            GameStateController.OnStateSwitch += GameStateListener;
        }

        private void OnDisable()
        {
            CargoDropController.OnCargoDrop -= UpdateCargoStatus;
            CargoDropController.OnGetAccuracy -= SetModelAccuracy;
            GameStateController.OnStateSwitch -= GameStateListener;
        }

        private void Awake()
        {
            _missionModelViewer = GetComponentInParent<MissionModelViewer>();
        }

        private void Start()
        {
            CargoCount = _missionModelViewer.CargoCount;
        }

        void UpdateCargoStatus()    
        {
            _currentCargoIndex++;
            DropEventInvoker(DropStatus.End);
            Debug.Log($" {_currentCargoIndex} {_cargoCount}");
            if (_currentCargoIndex == _cargoCount)
            {
                Debug.Log($"out of cargo {_currentCargoIndex} {_cargoCount}");
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
            SetCargo?.Invoke(_missionModelViewer.GetCargo());
            _delayBeforeDrop -= _delayDecreaseStep;
            yield return new WaitForSeconds(_delayBeforeDrop);
            DropEventInvoker(DropStatus.Start);
        }

        private void SetModelAccuracy(DropAccuracy dropAccuracy)
        {
            _missionModelViewer.AddAccuracy(dropAccuracy);
        }

        private void GameStateListener(GameState gameState)
        {
            if (gameState == GameState.CargoDrop)
            {
                Debug.Log("cargo drop state");
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
