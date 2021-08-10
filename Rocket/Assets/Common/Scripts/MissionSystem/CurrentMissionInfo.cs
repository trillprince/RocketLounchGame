using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common.Scripts.MissionSystem
{
    [CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Gameplay/Mission")]
    public class CurrentMissionInfo : ScriptableObject
    {
        public string _nameOfMission;
        public int cargoCount;



    }

}