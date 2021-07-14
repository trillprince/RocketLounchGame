using Common.Scripts.MissionSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Scripts.UI.InGame
{
    public class DropCargoButton : MonoBehaviour
    {
        private Button _dropCargoButton;
        private TextMeshProUGUI _textMesh;
        public delegate void DropCargo();

        public static event DropCargo CargoDropped;
        public Button GetButton()
        {
            return _dropCargoButton;
        }
    
        private void Awake()
        {
            _textMesh = GetComponentInChildren<TextMeshProUGUI>();
            _dropCargoButton = GetComponentInChildren<Button>();
            _dropCargoButton.onClick.AddListener((() =>
            {
                CargoDropped?.Invoke();
            }));
            CargoButtonStatus(false);
        }

        private void OnEnable()
        {
            MissionManager.TimeToDrop += CargoButtonStatus;
        }

        private void OnDisable()
        {
            MissionManager.TimeToDrop -= CargoButtonStatus;
        }
    
        void CargoButtonStatus(bool isActive)
        {
            _dropCargoButton.gameObject.SetActive(isActive);
        }
    
    
    }
}
