// GENERATED AUTOMATICALLY FROM 'Assets/Common/Scripts/Input/TouchControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TouchControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TouchControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchControls"",
    ""maps"": [
        {
            ""name"": ""Accelerometer"",
            ""id"": ""e9e187f9-dcd4-46c0-bae9-1c9b9e25fe3d"",
            ""actions"": [
                {
                    ""name"": ""Acceleration"",
                    ""type"": ""Value"",
                    ""id"": ""768a57cf-e03c-40a1-be1d-7bc377d19200"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""41aac31a-b5ea-42d3-a7ed-b5ae2c407e8a"",
                    ""path"": ""<Accelerometer>/acceleration"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Acceleration"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Arrows"",
            ""id"": ""2fb57194-3f90-4016-93af-5c00ce992a62"",
            ""actions"": [
                {
                    ""name"": ""ArrowPress"",
                    ""type"": ""Button"",
                    ""id"": ""79ec4452-2d05-4d23-b9a9-6dd75e97f4e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b1875548-9ab4-4199-9eda-46f31e98f267"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1970adbe-9d95-4eab-a0e7-61ee3337e96c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Accelerometer
        m_Accelerometer = asset.FindActionMap("Accelerometer", throwIfNotFound: true);
        m_Accelerometer_Acceleration = m_Accelerometer.FindAction("Acceleration", throwIfNotFound: true);
        // Arrows
        m_Arrows = asset.FindActionMap("Arrows", throwIfNotFound: true);
        m_Arrows_ArrowPress = m_Arrows.FindAction("ArrowPress", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Accelerometer
    private readonly InputActionMap m_Accelerometer;
    private IAccelerometerActions m_AccelerometerActionsCallbackInterface;
    private readonly InputAction m_Accelerometer_Acceleration;
    public struct AccelerometerActions
    {
        private @TouchControls m_Wrapper;
        public AccelerometerActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Acceleration => m_Wrapper.m_Accelerometer_Acceleration;
        public InputActionMap Get() { return m_Wrapper.m_Accelerometer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AccelerometerActions set) { return set.Get(); }
        public void SetCallbacks(IAccelerometerActions instance)
        {
            if (m_Wrapper.m_AccelerometerActionsCallbackInterface != null)
            {
                @Acceleration.started -= m_Wrapper.m_AccelerometerActionsCallbackInterface.OnAcceleration;
                @Acceleration.performed -= m_Wrapper.m_AccelerometerActionsCallbackInterface.OnAcceleration;
                @Acceleration.canceled -= m_Wrapper.m_AccelerometerActionsCallbackInterface.OnAcceleration;
            }
            m_Wrapper.m_AccelerometerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Acceleration.started += instance.OnAcceleration;
                @Acceleration.performed += instance.OnAcceleration;
                @Acceleration.canceled += instance.OnAcceleration;
            }
        }
    }
    public AccelerometerActions @Accelerometer => new AccelerometerActions(this);

    // Arrows
    private readonly InputActionMap m_Arrows;
    private IArrowsActions m_ArrowsActionsCallbackInterface;
    private readonly InputAction m_Arrows_ArrowPress;
    public struct ArrowsActions
    {
        private @TouchControls m_Wrapper;
        public ArrowsActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ArrowPress => m_Wrapper.m_Arrows_ArrowPress;
        public InputActionMap Get() { return m_Wrapper.m_Arrows; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ArrowsActions set) { return set.Get(); }
        public void SetCallbacks(IArrowsActions instance)
        {
            if (m_Wrapper.m_ArrowsActionsCallbackInterface != null)
            {
                @ArrowPress.started -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnArrowPress;
                @ArrowPress.performed -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnArrowPress;
                @ArrowPress.canceled -= m_Wrapper.m_ArrowsActionsCallbackInterface.OnArrowPress;
            }
            m_Wrapper.m_ArrowsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ArrowPress.started += instance.OnArrowPress;
                @ArrowPress.performed += instance.OnArrowPress;
                @ArrowPress.canceled += instance.OnArrowPress;
            }
        }
    }
    public ArrowsActions @Arrows => new ArrowsActions(this);
    public interface IAccelerometerActions
    {
        void OnAcceleration(InputAction.CallbackContext context);
    }
    public interface IArrowsActions
    {
        void OnArrowPress(InputAction.CallbackContext context);
    }
}
