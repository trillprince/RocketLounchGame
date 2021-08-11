using System.Collections.Generic;
using Common.Scripts.Cargo;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "MissionModel", menuName = "ScriptableObjects/Gameplay/MissionModel")]
    public class MissionModel: ScriptableObject
    {
        [SerializeField] private int _cargoCount;
        [SerializeField] private List<DropAccuracy> _accuracies;
        [SerializeField] private List<GameObject> _cargos;
        
        public int CargoCount
        {
            get => _cargoCount;
            set => _cargoCount = value;
        }

        public List<DropAccuracy> Accuracies
        {
            get => _accuracies;
            set => _accuracies = value;
        }

        public List<GameObject> Cargos
        {
            get => _cargos;
            set => _cargos = value;
        }
    }
}