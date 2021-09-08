using UnityEngine;

namespace Common.Scripts.Rocket
{
    [CreateAssetMenu(fileName = "PauseOfGameModel", menuName = "ScriptableObjects/Gameplay/Models/PauseOfGameModel")]
    public class PauseOfGameWindow : ScriptableObject, IWindowModel
    {
        [SerializeField] private GameObject _window;
        private string _key = "PauseOfGame";

        public GameObject GetWindowObject()
        {
            return _window;
        }

        public string GetKey()
        {
            return _key;
        }
    }
}