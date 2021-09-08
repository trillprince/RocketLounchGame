using Common.Scripts.MissionSystem;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    [CreateAssetMenu(fileName = "EndOfGameModel", menuName = "ScriptableObjects/Gameplay/Models/EndOfGameModel")]
    public class EndOfGameModel : ScriptableObject,IWindowModel
    {
        [SerializeField] public GameObject EndOfGameWindow;
        [SerializeField] public MissionModel MissionModel;
        public GameObject GetWindowObject()
        {
            return EndOfGameWindow;
        }

        public string GetKey()
        {
            throw new System.NotImplementedException();
        }
    }
}