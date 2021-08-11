using System.Collections.Generic;
using Common.Scripts.Cargo;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common.Scripts.MissionSystem
{
    public class MissionModelViewer : MonoBehaviour
    {
        [SerializeField] private MissionModel _missionModel;
        [SerializeField] private GameObject _cargo;
        private int _minCargoCount;
        private int _maxCargoCount;

        public void AddAccuracy(DropAccuracy accuracy)
        {
            _missionModel.Accuracies.Add(accuracy);
        }

        public int GetCargoCount()
        {
            return _missionModel.CargoCount;
        }

        public void InitMissionModel(int minCargoCount, int maxCargoCount)
        {
            _missionModel.CargoCount = Random.Range(minCargoCount, maxCargoCount);
            _missionModel.Accuracies = new List<DropAccuracy>(_missionModel.CargoCount);
            _missionModel.Cargos = new List<GameObject>(_missionModel.CargoCount);
        }
    }
}
