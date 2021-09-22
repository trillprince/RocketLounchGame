using Common.Scripts.MissionSystem;
using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    [CreateAssetMenu(fileName = "EndOfGameModel", menuName = "ScriptableObjects/Gameplay/Models/EndOfGameModel")]
    public class EndOfGameModel : ScriptableObject,IWindowModel
    {
        [SerializeField] public GameObject EndOfGameWindow;
        private string _key = "EndOfGame";

        public GameObject GetWindowObject()
        {
            return EndOfGameWindow;
        }

        public string GetKey()
        {
            return _key;
        }
    }
}