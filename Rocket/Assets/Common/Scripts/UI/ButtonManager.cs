using UnityEngine;

namespace Common.Scripts.UI
{
    public class ButtonManager : MonoBehaviour,IButtonController
    {
        private IControlledButton[] _controlledButtons;
        private IControlledText[] _controlledText;

        private void OnEnable()
        {
            _controlledButtons = GetComponentsInChildren<IControlledButton>();
            _controlledText = GetComponentsInChildren<IControlledText>();
        }

        public void ButtonsActive(bool isActive)
        {
            foreach (IControlledButton button in _controlledButtons)
            {
                button.SetInteractStatus(isActive);
            }

            foreach (IControlledText text in _controlledText)
            {
                text.IsActive(isActive);
            }
        }
    }
}