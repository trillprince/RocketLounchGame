using UnityEngine;
using Zenject;

namespace Common.Scripts
{
    public class LounchManager : MonoBehaviour
    {
        public delegate void Station();
        public static event Station RocketLounch;
        
        
        public void LounchTheRocket()
        {
            RocketLounch?.Invoke();
        }
    }
}
