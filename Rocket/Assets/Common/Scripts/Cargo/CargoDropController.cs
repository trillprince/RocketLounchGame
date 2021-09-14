using System;
using System.Collections.Generic;
using Common.Scripts.Input;
using Common.Scripts.MissionSystem;
using Common.Scripts.UI;
using UnityEngine;

namespace Common.Scripts.Cargo
{
    public class CargoDropController : MonoBehaviour
    {
        private bool _dropReady;
        public delegate void DropCargo();
        public static event DropCargo CargoDropping;
        public static event DropCargo OnCargoDrop;

        public static Action<DropAccuracy> OnGetAccuracy;

        public delegate void DropStatusSender();

        public static event DropStatusSender OnGetStartDropStatus;

        bool DropReady
        {
            get => _dropReady;
            set => _dropReady = value;
        }
        

        private void OnEnable()
        {
            GameLoopController.TimeToDrop += CheckDropStatus;
            InputManager.OnTouchStart += DropOnTouch;
            CargoDropSlider.OnGetDropAccuracy += SetDropAccuracy;
        }

        private void OnDisable()
        {
            GameLoopController.TimeToDrop -= CheckDropStatus;
            InputManager.OnTouchStart -= DropOnTouch;
            CargoDropSlider.OnGetDropAccuracy -= SetDropAccuracy;
        }

        void CheckDropStatus(DropStatus dropStatus)
        {
            if (dropStatus == DropStatus.Start)
            {
                OnGetStartDropStatus?.Invoke();
                DropReady = true;
            }
        }

        void DropOnTouch(Vector2 touchPos)
        {
            if (DropReady)
            {
                CargoDropping?.Invoke();
            }
        }

        void SetDropAccuracy(DropAccuracy accuracy)
        {
            OnCargoDrop?.Invoke();
            OnGetAccuracy?.Invoke(accuracy);
            DropReady = false;
        }
    }
    public enum DropAccuracy
    {
        NoInteraction,
        NotGood,
        Nice,
        Perfect
    }
}
