using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    [CreateAssetMenu(fileName = "EndOfGameModel", menuName = "ScriptableObjects/Gameplay/EndOfGameModel")]
    public class EndOfGameModel : ScriptableObject
    {
        [SerializeField] public GameObject EndOfGameWindow;
        [SerializeField] public MissionModel MissionModel;
    }
}