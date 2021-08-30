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
        public int CargoCount { get; private set; }
        [SerializeField] private GameObject  _cargo;


        private void Awake()
        {
            InitMissionModel();
        }
        

        public void AddAccuracy(DropAccuracy accuracy)
        {
            _missionModel.Accuracies.Add(accuracy);
        }
        

        void InitMissionModel()
        {
            CargoCount = GetRandomCargoCount();
            _missionModel.Accuracies = new List<DropAccuracy>(CargoCount);
            _missionModel.Cargos = new Queue<GameObject>();
            for (int i = 0; i < CargoCount; i++)
            {
                _missionModel.Cargos.Enqueue(_cargo);
            }
        }

        public GameObject GetCargo()
        {
            if (CargoCount > 0)
            {
                return _missionModel.Cargos.Dequeue();
            }
            return default;
        }
        private int GetRandomCargoCount()
        {
            return Random.Range(_missionModel.MINCargoCount,_missionModel.MAXCargoCount);
        }
    }
}
