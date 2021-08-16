using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class MissionModelViewer : MonoBehaviour
    {
        [SerializeField] private MissionModel _missionModel;
        private int _cargoCount;
        [SerializeField] private GameObject  _cargo;


        private void Awake()
        {
            InitMissionModel();
        }

        public void AddAccuracy(DropAccuracy accuracy)
        {
            _missionModel.Accuracies.Add(accuracy);
        }

        public int GetCargoCount()
        {
            return _cargoCount;
        }

        void InitMissionModel()
        {
            _cargoCount = Random.Range(_missionModel.MINCargoCount, _missionModel.MAXCargoCount);
            _missionModel.Accuracies = new List<DropAccuracy>(_cargoCount);
            _missionModel.Cargos = new Queue<GameObject>();
            for (int i = 0; i < _cargoCount; i++)
            {
                _missionModel.Cargos.Enqueue(_cargo);
            }
            Debug.Log(_missionModel.Cargos.Count);
        }

        public GameObject GetCargo()
        {
            if (_cargoCount > 0)
            {
                return _missionModel.Cargos.Dequeue();
            }
            return default;
        }
    }
}
