using System.Collections.Generic;
using Common.Scripts.Cargo;
using UnityEngine;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "MissionModel", menuName = "ScriptableObjects/Gameplay/MissionModel")]
    public class MissionModel: ScriptableObject
    {
        [SerializeField] private int _minCargoCount;
        [SerializeField] private int _maxCargoCount;
        [SerializeField] private List<DropAccuracy> _accuracies;
        private Queue<GameObject> _cargos;
        
        public List<DropAccuracy> Accuracies
        {
            get => _accuracies;
            set => _accuracies = value;
        }

        public int MINCargoCount => _minCargoCount;

        public int MAXCargoCount => _maxCargoCount;

        public Queue<GameObject> Cargos
        {
            get => _cargos;
            set => _cargos = value;
        }

       
    }
}