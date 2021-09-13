using System;
using System.Collections;
using System.Collections.Generic;
using Common.Scripts.Cargo;
using Common.Scripts.Infrastructure;
using Common.Scripts.Rocket;
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
        }

        public GameObject GetCargo()
        {
            if (CargoCount > 0)
            {
                CargoCount--;
                return _cargo;
            }
            return default;
        }
        private int GetRandomCargoCount()
        {
            return Random.Range(_missionModel.MINCargoCount,_missionModel.MAXCargoCount);
        }
        
    }
}
