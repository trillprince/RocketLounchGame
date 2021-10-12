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
            ""name"": ""AndroidInput"",
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
            ""name"": ""EditorInput"",
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
                    ""name"": ""1D Axis"",
                    ""id"": ""f2eda2b6-a843-4642-96db-76cab75af85f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowPress"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""5e6d6eb6-4faf-4d88-95c4-599fff3af476"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""8d38df50-1f1c-40d4-8045-7dabd07af712"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ArrowPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // AndroidInput
        m_AndroidInput = asset.FindActionMap("AndroidInput", throwIfNotFound: true);
        m_AndroidInput_Acceleration = m_AndroidInput.FindAction("Acceleration", throwIfNotFound: true);
        // EditorInput
        m_EditorInput = asset.FindActionMap("EditorInput", throwIfNotFound: true);
        m_EditorInput_ArrowPress = m_EditorInput.FindAction("ArrowPress", throwIfNotFound: true);
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

    // AndroidInput
    private readonly InputActionMap m_AndroidInput;
    private IAndroidInputActions m_AndroidInputActionsCallbackInterface;
    private readonly InputAction m_AndroidInput_Acceleration;
    public struct AndroidInputActions
    {
        private @TouchControls m_Wrapper;
        public AndroidInputActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Acceleration => m_Wrapper.m_AndroidInput_Acceleration;
        public InputActionMap Get() { return m_Wrapper.m_AndroidInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AndroidInputActions set) { return set.Get(); }
        public void SetCallbacks(IAndroidInputActions instance)
        {
            if (m_Wrapper.m_AndroidInputActionsCallbackInterface != null)
            {
                @Acceleration.started -= m_Wrapper.m_AndroidInputActionsCallbackInterface.OnAcceleration;
                @Acceleration.performed -= m_Wrapper.m_AndroidInputActionsCallbackInterface.OnAcceleration;
                @Acceleration.canceled -= m_Wrapper.m_AndroidInputActionsCallbackInterface.OnAcceleration;
            }
            m_Wrapper.m_AndroidInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Acceleration.started += instance.OnAcceleration;
                @Acceleration.performed += instance.OnAcceleration;
                @Acceleration.canceled += instance.OnAcceleration;
            }
        }
    }
    public AndroidInputActions @AndroidInput => new AndroidInputActions(this);

    // EditorInput
    private readonly InputActionMap m_EditorInput;
    private IEditorInputActions m_EditorInputActionsCallbackInterface;
    private readonly InputAction m_EditorInput_ArrowPress;
    public struct EditorInputActions
    {
        private @TouchControls m_Wrapper;
        public EditorInputActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ArrowPress => m_Wrapper.m_EditorInput_ArrowPress;
        public InputActionMap Get() { return m_Wrapper.m_EditorInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(EditorInputActions set) { return set.Get(); }
        public void SetCallbacks(IEditorInputActions instance)
        {
            if (m_Wrapper.m_EditorInputActionsCallbackInterface != null)
            {
                @ArrowPress.started -= m_Wrapper.m_EditorInputActionsCallbackInterface.OnArrowPress;
                @ArrowPress.performed -= m_Wrapper.m_EditorInputActionsCallbackInterface.OnArrowPress;
                @ArrowPress.canceled -= m_Wrapper.m_EditorInputActionsCallbackInterface.OnArrowPress;
            }
            m_Wrapper.m_EditorInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ArrowPress.started += instance.OnArrowPress;
                @ArrowPress.performed += instance.OnArrowPress;
                @ArrowPress.canceled += instance.OnArrowPress;
            }
        }
    }
    public EditorInputActions @EditorInput => new EditorInputActions(this);
    public interface IAndroidInputActions
    {
        void OnAcceleration(InputAction.CallbackContext context);
    }
    public interface IEditorInputActions
    {
        void OnArrowPress(InputAction.CallbackContext context);
    }
}
