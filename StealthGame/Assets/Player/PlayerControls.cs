//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Player/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""c9b77e94-0963-4142-a744-9d4d0439954a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6487a51b-2814-4bdf-beb0-7d5388fc36d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7d8bef3a-d90a-474b-adf8-eb63f39eb2d2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""18a6f4e6-72ea-4f8d-b912-dbd0d7f21cf7"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0f6aa11c-52b6-4824-90af-e532ffdae32f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4b11fcd9-01fd-4da1-b839-2eca83847595"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ca6d9962-2608-4a45-adb3-6ad366154f19"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""acf3047e-9c07-4576-acae-6903f259f527"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c7e11d63-c2d3-443f-ba95-1d38189154a6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""b8e9ded1-f224-4db1-af15-5a5069d83f4e"",
            ""actions"": [
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""b1309547-e8d6-4225-acba-e783d75482df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""1d1f754a-a701-45af-a057-ea4685d96e86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fb593ca4-3ddd-41be-97d1-2d3c6e9c884e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AimTeleport"",
                    ""type"": ""Button"",
                    ""id"": ""15af6f87-a3ba-4e31-b6a2-a686d19b75f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Teleport"",
                    ""type"": ""Button"",
                    ""id"": ""239d614f-c2b8-4ee6-8cf0-84f189164199"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftAttack"",
                    ""type"": ""Button"",
                    ""id"": ""7ee9a5a4-0f57-4d9f-9995-c39ebe22b1b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sheathe/Unsheathe"",
                    ""type"": ""Button"",
                    ""id"": ""5bfdb558-e8fb-4827-a591-8c0f4860515b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AimDistraction"",
                    ""type"": ""Button"",
                    ""id"": ""9bdcc0e6-464a-4841-9b69-0316c03cc53e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PutDistraction"",
                    ""type"": ""Button"",
                    ""id"": ""a8964835-ac8c-4976-9986-97416111f259"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dde706ff-746f-4a2c-ab9b-8903b385ef04"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""acb96368-37d8-49e3-b2f8-4f10a528ec48"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7725f47-2e4e-49f3-ac90-0c319fa1dd98"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a0e5d20b-a736-4680-824a-d4742e73ad32"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimTeleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e79545cc-9b0e-4cd5-a80e-d8a432efa3fb"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Teleport"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b335895-5a1a-44e6-a61d-50ef6c56a80f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eaeee8fb-97d2-45b5-b0c7-3ee80ffe10eb"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sheathe/Unsheathe"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70f08fde-cfeb-4e96-96da-2ce6df6c04d5"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AimDistraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ca2fff3-e20b-40fd-89e3-21ee8e37924c"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PutDistraction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""b18dc33c-1be0-4a08-b56b-6e2bd82d0015"",
            ""actions"": [
                {
                    ""name"": ""ToggleObjectives"",
                    ""type"": ""Button"",
                    ""id"": ""90021904-fe6f-4f46-afb2-25c122d409ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c3983469-79d4-456f-a80b-ad1154ec92df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4686158c-6a01-4ff5-8301-9cbee5c4fe03"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleObjectives"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47562c05-7865-44ba-8b94-6e361c4c69d1"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerMovement
        m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
        m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
        m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
        // PlayerActions
        m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
        m_PlayerActions_Sprint = m_PlayerActions.FindAction("Sprint", throwIfNotFound: true);
        m_PlayerActions_Crouch = m_PlayerActions.FindAction("Crouch", throwIfNotFound: true);
        m_PlayerActions_Jump = m_PlayerActions.FindAction("Jump", throwIfNotFound: true);
        m_PlayerActions_AimTeleport = m_PlayerActions.FindAction("AimTeleport", throwIfNotFound: true);
        m_PlayerActions_Teleport = m_PlayerActions.FindAction("Teleport", throwIfNotFound: true);
        m_PlayerActions_LeftAttack = m_PlayerActions.FindAction("LeftAttack", throwIfNotFound: true);
        m_PlayerActions_SheatheUnsheathe = m_PlayerActions.FindAction("Sheathe/Unsheathe", throwIfNotFound: true);
        m_PlayerActions_AimDistraction = m_PlayerActions.FindAction("AimDistraction", throwIfNotFound: true);
        m_PlayerActions_PutDistraction = m_PlayerActions.FindAction("PutDistraction", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_ToggleObjectives = m_UI.FindAction("ToggleObjectives", throwIfNotFound: true);
        m_UI_Interact = m_UI.FindAction("Interact", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerMovement
    private readonly InputActionMap m_PlayerMovement;
    private List<IPlayerMovementActions> m_PlayerMovementActionsCallbackInterfaces = new List<IPlayerMovementActions>();
    private readonly InputAction m_PlayerMovement_Movement;
    private readonly InputAction m_PlayerMovement_Camera;
    public struct PlayerMovementActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
        public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Camera.started += instance.OnCamera;
            @Camera.performed += instance.OnCamera;
            @Camera.canceled += instance.OnCamera;
        }

        private void UnregisterCallbacks(IPlayerMovementActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Camera.started -= instance.OnCamera;
            @Camera.performed -= instance.OnCamera;
            @Camera.canceled -= instance.OnCamera;
        }

        public void RemoveCallbacks(IPlayerMovementActions instance)
        {
            if (m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

    // PlayerActions
    private readonly InputActionMap m_PlayerActions;
    private List<IPlayerActionsActions> m_PlayerActionsActionsCallbackInterfaces = new List<IPlayerActionsActions>();
    private readonly InputAction m_PlayerActions_Sprint;
    private readonly InputAction m_PlayerActions_Crouch;
    private readonly InputAction m_PlayerActions_Jump;
    private readonly InputAction m_PlayerActions_AimTeleport;
    private readonly InputAction m_PlayerActions_Teleport;
    private readonly InputAction m_PlayerActions_LeftAttack;
    private readonly InputAction m_PlayerActions_SheatheUnsheathe;
    private readonly InputAction m_PlayerActions_AimDistraction;
    private readonly InputAction m_PlayerActions_PutDistraction;
    public struct PlayerActionsActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Sprint => m_Wrapper.m_PlayerActions_Sprint;
        public InputAction @Crouch => m_Wrapper.m_PlayerActions_Crouch;
        public InputAction @Jump => m_Wrapper.m_PlayerActions_Jump;
        public InputAction @AimTeleport => m_Wrapper.m_PlayerActions_AimTeleport;
        public InputAction @Teleport => m_Wrapper.m_PlayerActions_Teleport;
        public InputAction @LeftAttack => m_Wrapper.m_PlayerActions_LeftAttack;
        public InputAction @SheatheUnsheathe => m_Wrapper.m_PlayerActions_SheatheUnsheathe;
        public InputAction @AimDistraction => m_Wrapper.m_PlayerActions_AimDistraction;
        public InputAction @PutDistraction => m_Wrapper.m_PlayerActions_PutDistraction;
        public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActionsActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Add(instance);
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @Crouch.started += instance.OnCrouch;
            @Crouch.performed += instance.OnCrouch;
            @Crouch.canceled += instance.OnCrouch;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @AimTeleport.started += instance.OnAimTeleport;
            @AimTeleport.performed += instance.OnAimTeleport;
            @AimTeleport.canceled += instance.OnAimTeleport;
            @Teleport.started += instance.OnTeleport;
            @Teleport.performed += instance.OnTeleport;
            @Teleport.canceled += instance.OnTeleport;
            @LeftAttack.started += instance.OnLeftAttack;
            @LeftAttack.performed += instance.OnLeftAttack;
            @LeftAttack.canceled += instance.OnLeftAttack;
            @SheatheUnsheathe.started += instance.OnSheatheUnsheathe;
            @SheatheUnsheathe.performed += instance.OnSheatheUnsheathe;
            @SheatheUnsheathe.canceled += instance.OnSheatheUnsheathe;
            @AimDistraction.started += instance.OnAimDistraction;
            @AimDistraction.performed += instance.OnAimDistraction;
            @AimDistraction.canceled += instance.OnAimDistraction;
            @PutDistraction.started += instance.OnPutDistraction;
            @PutDistraction.performed += instance.OnPutDistraction;
            @PutDistraction.canceled += instance.OnPutDistraction;
        }

        private void UnregisterCallbacks(IPlayerActionsActions instance)
        {
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @Crouch.started -= instance.OnCrouch;
            @Crouch.performed -= instance.OnCrouch;
            @Crouch.canceled -= instance.OnCrouch;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @AimTeleport.started -= instance.OnAimTeleport;
            @AimTeleport.performed -= instance.OnAimTeleport;
            @AimTeleport.canceled -= instance.OnAimTeleport;
            @Teleport.started -= instance.OnTeleport;
            @Teleport.performed -= instance.OnTeleport;
            @Teleport.canceled -= instance.OnTeleport;
            @LeftAttack.started -= instance.OnLeftAttack;
            @LeftAttack.performed -= instance.OnLeftAttack;
            @LeftAttack.canceled -= instance.OnLeftAttack;
            @SheatheUnsheathe.started -= instance.OnSheatheUnsheathe;
            @SheatheUnsheathe.performed -= instance.OnSheatheUnsheathe;
            @SheatheUnsheathe.canceled -= instance.OnSheatheUnsheathe;
            @AimDistraction.started -= instance.OnAimDistraction;
            @AimDistraction.performed -= instance.OnAimDistraction;
            @AimDistraction.canceled -= instance.OnAimDistraction;
            @PutDistraction.started -= instance.OnPutDistraction;
            @PutDistraction.performed -= instance.OnPutDistraction;
            @PutDistraction.canceled -= instance.OnPutDistraction;
        }

        public void RemoveCallbacks(IPlayerActionsActions instance)
        {
            if (m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActionsActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_ToggleObjectives;
    private readonly InputAction m_UI_Interact;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ToggleObjectives => m_Wrapper.m_UI_ToggleObjectives;
        public InputAction @Interact => m_Wrapper.m_UI_Interact;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @ToggleObjectives.started += instance.OnToggleObjectives;
            @ToggleObjectives.performed += instance.OnToggleObjectives;
            @ToggleObjectives.canceled += instance.OnToggleObjectives;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @ToggleObjectives.started -= instance.OnToggleObjectives;
            @ToggleObjectives.performed -= instance.OnToggleObjectives;
            @ToggleObjectives.canceled -= instance.OnToggleObjectives;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IPlayerMovementActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCamera(InputAction.CallbackContext context);
    }
    public interface IPlayerActionsActions
    {
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAimTeleport(InputAction.CallbackContext context);
        void OnTeleport(InputAction.CallbackContext context);
        void OnLeftAttack(InputAction.CallbackContext context);
        void OnSheatheUnsheathe(InputAction.CallbackContext context);
        void OnAimDistraction(InputAction.CallbackContext context);
        void OnPutDistraction(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnToggleObjectives(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
