// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMaster : IInputActionCollection
{
    private InputActionAsset asset;
    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Soldier"",
            ""id"": ""f833ed95-5533-4043-8c44-f25cb12ea8dd"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""id"": ""f572e282-2403-424c-b82e-996ee69f8bbf"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Aim"",
                    ""id"": ""6702c0ca-c5d0-4d81-9c82-cbe1a6d3b457"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Movement"",
                    ""id"": ""53b5fb37-d4c6-435e-af6b-85fea87a2612"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Jump"",
                    ""id"": ""c984e99b-e2ca-4569-8c7a-a3bb2d638f40"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Crouch"",
                    ""id"": ""ef9ab5aa-29bf-4cdb-a89f-84cee68a3958"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Sprint"",
                    ""id"": ""06fa61d0-16b0-4906-9b6b-d468d0b01e1b"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Reload"",
                    ""id"": ""0eadcb52-037b-45f3-9d1e-2924f038f023"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""SwapWeapon"",
                    ""id"": ""50f3cf38-0125-4346-b2ba-046b6d53e065"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8f638284-e227-443f-bab5-7fd78eab63b4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""835f9de4-fcb0-4897-9317-c6c1d65f4bb3"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""wasd"",
                    ""id"": ""4e13f95f-6ba3-4b26-abb2-62fe6ff7199c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7c690c48-359c-4c92-af1a-3992ca7a1f1f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""292b3ac5-a67c-4ddd-9720-d29319694d71"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b5b3b521-b695-4c89-bca5-658efc729bc8"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97873ee6-a382-48a7-a5d4-baa913993d97"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""dpad"",
                    ""id"": ""bf749e4d-79b9-4fbf-b9c7-2f6a6b8cf6d3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""983ba0ef-aabd-40ee-874e-c921aa207a78"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5a41a4f5-a49b-4ea8-9c07-b28b62562486"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7be04007-3660-4661-bc11-0a67eede5227"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b2247ef9-129e-4635-aa6f-61325588b500"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""f6b10b3e-b1dc-4c6c-9e45-0103a454e783"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""84055ceb-ccd4-48c3-9889-cff5039f79e0"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""b6ffdfbb-bff4-4dfa-a596-2f6e9b14bd7a"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""79fc70b9-a799-4341-b725-bea14d8c7f3c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""37ca0fb0-928e-4078-a8cc-dc10d4ac698f"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""e2b79dfc-8bb0-48ae-8b7f-99a2285a441c"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""5a5c9566-06cc-47a3-a23a-fe4a3746557a"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""1dd6ac8d-44fc-4c07-a308-48265b639c17"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""c7c37ec4-1721-464f-95c0-4d71730124d2"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""0e1c1a3f-8de2-46dd-bde3-437bd2909f46"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""ShoulderButtons"",
                    ""id"": ""e4706669-2fd3-41fd-ac2e-ee641420ce19"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapWeapon"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b6ae5c36-0e9b-41a6-afc8-accc68ddba30"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SwapWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ee5fe695-e7e3-42af-bf8a-2173e6d53fe8"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SwapWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""basedOn"": """",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""basedOn"": """",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Soldier
        m_Soldier = asset.GetActionMap("Soldier");
        m_Soldier_Shoot = m_Soldier.GetAction("Shoot");
        m_Soldier_Aim = m_Soldier.GetAction("Aim");
        m_Soldier_Movement = m_Soldier.GetAction("Movement");
        m_Soldier_Jump = m_Soldier.GetAction("Jump");
        m_Soldier_Crouch = m_Soldier.GetAction("Crouch");
        m_Soldier_Sprint = m_Soldier.GetAction("Sprint");
        m_Soldier_Reload = m_Soldier.GetAction("Reload");
        m_Soldier_SwapWeapon = m_Soldier.GetAction("SwapWeapon");
    }

    ~InputMaster()
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

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

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

    // Soldier
    private InputActionMap m_Soldier;
    private ISoldierActions m_SoldierActionsCallbackInterface;
    private InputAction m_Soldier_Shoot;
    private InputAction m_Soldier_Aim;
    private InputAction m_Soldier_Movement;
    private InputAction m_Soldier_Jump;
    private InputAction m_Soldier_Crouch;
    private InputAction m_Soldier_Sprint;
    private InputAction m_Soldier_Reload;
    private InputAction m_Soldier_SwapWeapon;
    public struct SoldierActions
    {
        private InputMaster m_Wrapper;
        public SoldierActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot { get { return m_Wrapper.m_Soldier_Shoot; } }
        public InputAction @Aim { get { return m_Wrapper.m_Soldier_Aim; } }
        public InputAction @Movement { get { return m_Wrapper.m_Soldier_Movement; } }
        public InputAction @Jump { get { return m_Wrapper.m_Soldier_Jump; } }
        public InputAction @Crouch { get { return m_Wrapper.m_Soldier_Crouch; } }
        public InputAction @Sprint { get { return m_Wrapper.m_Soldier_Sprint; } }
        public InputAction @Reload { get { return m_Wrapper.m_Soldier_Reload; } }
        public InputAction @SwapWeapon { get { return m_Wrapper.m_Soldier_SwapWeapon; } }
        public InputActionMap Get() { return m_Wrapper.m_Soldier; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(SoldierActions set) { return set.Get(); }
        public void SetCallbacks(ISoldierActions instance)
        {
            if (m_Wrapper.m_SoldierActionsCallbackInterface != null)
            {
                Shoot.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnShoot;
                Shoot.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnShoot;
                Shoot.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnShoot;
                Aim.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnAim;
                Aim.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnAim;
                Aim.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnAim;
                Movement.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnMovement;
                Jump.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnJump;
                Crouch.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnCrouch;
                Crouch.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnCrouch;
                Crouch.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnCrouch;
                Sprint.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnSprint;
                Sprint.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnSprint;
                Sprint.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnSprint;
                Reload.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnReload;
                Reload.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnReload;
                Reload.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnReload;
                SwapWeapon.started -= m_Wrapper.m_SoldierActionsCallbackInterface.OnSwapWeapon;
                SwapWeapon.performed -= m_Wrapper.m_SoldierActionsCallbackInterface.OnSwapWeapon;
                SwapWeapon.canceled -= m_Wrapper.m_SoldierActionsCallbackInterface.OnSwapWeapon;
            }
            m_Wrapper.m_SoldierActionsCallbackInterface = instance;
            if (instance != null)
            {
                Shoot.started += instance.OnShoot;
                Shoot.performed += instance.OnShoot;
                Shoot.canceled += instance.OnShoot;
                Aim.started += instance.OnAim;
                Aim.performed += instance.OnAim;
                Aim.canceled += instance.OnAim;
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Crouch.started += instance.OnCrouch;
                Crouch.performed += instance.OnCrouch;
                Crouch.canceled += instance.OnCrouch;
                Sprint.started += instance.OnSprint;
                Sprint.performed += instance.OnSprint;
                Sprint.canceled += instance.OnSprint;
                Reload.started += instance.OnReload;
                Reload.performed += instance.OnReload;
                Reload.canceled += instance.OnReload;
                SwapWeapon.started += instance.OnSwapWeapon;
                SwapWeapon.performed += instance.OnSwapWeapon;
                SwapWeapon.canceled += instance.OnSwapWeapon;
            }
        }
    }
    public SoldierActions @Soldier
    {
        get
        {
            return new SoldierActions(this);
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface ISoldierActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnSwapWeapon(InputAction.CallbackContext context);
    }
}
