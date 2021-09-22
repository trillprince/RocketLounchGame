using UnityEngine;

namespace Common.Scripts.UI
{
    public interface IWindowModel
    {
        public GameObject GetWindowObject();
        public string GetKey();
    }
}