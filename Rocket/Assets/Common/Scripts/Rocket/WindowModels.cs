using System;
using System.Reflection;
using UnityEngine;

namespace Common.Scripts.Rocket
{
    [CreateAssetMenu(fileName = "WindowModels", menuName = "ScriptableObjects/Gameplay/ModelsDatabase/WindowModels")]
    public class WindowModels: ScriptableObject
    {
        private IWindowModel[] _iWindowModels;
        [SerializeField] private ScriptableObject[] _windowModels;
        private bool _modelsInted = false;
        public IWindowModel GetSpecificModel (string key)
        {
            if (!_modelsInted)
            {
                _iWindowModels = new IWindowModel[_windowModels.Length];
                for (int i = 0; i < _windowModels.Length; i++)
                {
                    _iWindowModels[i] = (IWindowModel) _windowModels[i];
                }
                _modelsInted = true;
            }
            foreach (IWindowModel iWindowModel in _iWindowModels)
            {
                if (iWindowModel.GetKey() == key)
                {
                    return iWindowModel;
                }
            }
            return default;
        }
    }
}