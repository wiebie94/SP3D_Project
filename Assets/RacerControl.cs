// GENERATED AUTOMATICALLY FROM 'Assets/RacerControl.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @RacerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @RacerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""RacerControl"",
    ""maps"": [
        {
            ""name"": ""Racer"",
            ""id"": ""32c39d75-3a4b-43dd-9b54-641d63759c60"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e2617915-8067-4f02-bb1f-62bb879d115f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accellerate"",
                    ""type"": ""Value"",
                    ""id"": ""570f3cef-825f-4e92-bbfe-d76284999f49"",
                    ""expectedControlType"": ""Double"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""backwards"",
                    ""type"": ""Button"",
                    ""id"": ""1d93433e-1125-4390-8125-6bd53742f4f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCam"",
                    ""type"": ""Value"",
                    ""id"": ""0f45ffc2-67f0-4f4b-8a1d-5b83fc3b541b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""lookBack"",
                    ""type"": ""Button"",
                    ""id"": ""ded1c9d0-53c8-4bf2-a418-fbe7abbde0cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Item"",
                    ""type"": ""Button"",
                    ""id"": ""08cfe548-2c84-40a1-a0fb-0456850498af"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Respawn"",
                    ""type"": ""Button"",
                    ""id"": ""82b160f5-9b22-4d30-889f-5807e0562415"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuNav"",
                    ""type"": ""Value"",
                    ""id"": ""eb47d0fb-7f04-49e2-81b1-f0c73ee713da"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""OpenMenuInRace"",
                    ""type"": ""Button"",
                    ""id"": ""f630729a-142b-4a86-9158-fca7720a6052"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""95ab9a52-1fb6-4a31-aa80-e34e860ad774"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb3ce27c-bd92-49e6-8800-684f55a4f2fa"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accellerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8efefcff-e8c7-47e8-8475-9de84c20c24e"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""backwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31a83f69-c3a6-4883-a2f4-bae5971542ac"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""RotateCam"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d3b6d07-8105-41fb-b1d4-1c2ff9eceab4"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""lookBack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fb3c8f7-c997-4ba2-9f9f-5a303f161caf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5cc5e2eb-3246-4337-8260-c6ebe0f5addb"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99d7fa52-7846-4e51-b1c0-b371da6701da"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuNav"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61fe729d-9517-4779-a9e1-25a572ecdff8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""OpenMenuInRace"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""5ae9fcac-d858-4507-9195-4792c8e7fb79"",
            ""actions"": [
                {
                    ""name"": ""moveMenu"",
                    ""type"": ""Value"",
                    ""id"": ""da9f9a15-6a2e-4af9-affc-5afadc00adda"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""enter"",
                    ""type"": ""Button"",
                    ""id"": ""4fb293fe-47e4-4618-a85c-4997a79616ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""back"",
                    ""type"": ""Button"",
                    ""id"": ""f007ed99-eeb9-453f-9ecf-99c3aae8b049"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f67edbe-c65c-4608-82e9-bcb90caf53f1"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""moveMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""086fa1cc-141c-4f0c-9cc7-fe4e2373e40a"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""moveMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ec3364e-1fd1-4fa2-a896-d5312ba0034e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9159c8b0-7f5e-442f-bcf9-22e9aa2359d8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""New control scheme1"",
                    ""action"": ""back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""New control scheme"",
            ""bindingGroup"": ""New control scheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShock4GampadiOS>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""New control scheme1"",
            ""bindingGroup"": ""New control scheme1"",
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
        // Racer
        m_Racer = asset.FindActionMap("Racer", throwIfNotFound: true);
        m_Racer_Move = m_Racer.FindAction("Move", throwIfNotFound: true);
        m_Racer_Accellerate = m_Racer.FindAction("Accellerate", throwIfNotFound: true);
        m_Racer_backwards = m_Racer.FindAction("backwards", throwIfNotFound: true);
        m_Racer_RotateCam = m_Racer.FindAction("RotateCam", throwIfNotFound: true);
        m_Racer_lookBack = m_Racer.FindAction("lookBack", throwIfNotFound: true);
        m_Racer_Item = m_Racer.FindAction("Item", throwIfNotFound: true);
        m_Racer_Respawn = m_Racer.FindAction("Respawn", throwIfNotFound: true);
        m_Racer_MenuNav = m_Racer.FindAction("MenuNav", throwIfNotFound: true);
        m_Racer_OpenMenuInRace = m_Racer.FindAction("OpenMenuInRace", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_moveMenu = m_Menu.FindAction("moveMenu", throwIfNotFound: true);
        m_Menu_enter = m_Menu.FindAction("enter", throwIfNotFound: true);
        m_Menu_back = m_Menu.FindAction("back", throwIfNotFound: true);
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

    // Racer
    private readonly InputActionMap m_Racer;
    private IRacerActions m_RacerActionsCallbackInterface;
    private readonly InputAction m_Racer_Move;
    private readonly InputAction m_Racer_Accellerate;
    private readonly InputAction m_Racer_backwards;
    private readonly InputAction m_Racer_RotateCam;
    private readonly InputAction m_Racer_lookBack;
    private readonly InputAction m_Racer_Item;
    private readonly InputAction m_Racer_Respawn;
    private readonly InputAction m_Racer_MenuNav;
    private readonly InputAction m_Racer_OpenMenuInRace;
    public struct RacerActions
    {
        private @RacerControl m_Wrapper;
        public RacerActions(@RacerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Racer_Move;
        public InputAction @Accellerate => m_Wrapper.m_Racer_Accellerate;
        public InputAction @backwards => m_Wrapper.m_Racer_backwards;
        public InputAction @RotateCam => m_Wrapper.m_Racer_RotateCam;
        public InputAction @lookBack => m_Wrapper.m_Racer_lookBack;
        public InputAction @Item => m_Wrapper.m_Racer_Item;
        public InputAction @Respawn => m_Wrapper.m_Racer_Respawn;
        public InputAction @MenuNav => m_Wrapper.m_Racer_MenuNav;
        public InputAction @OpenMenuInRace => m_Wrapper.m_Racer_OpenMenuInRace;
        public InputActionMap Get() { return m_Wrapper.m_Racer; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RacerActions set) { return set.Get(); }
        public void SetCallbacks(IRacerActions instance)
        {
            if (m_Wrapper.m_RacerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnMove;
                @Accellerate.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnAccellerate;
                @Accellerate.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnAccellerate;
                @Accellerate.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnAccellerate;
                @backwards.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnBackwards;
                @backwards.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnBackwards;
                @backwards.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnBackwards;
                @RotateCam.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnRotateCam;
                @RotateCam.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnRotateCam;
                @RotateCam.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnRotateCam;
                @lookBack.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnLookBack;
                @lookBack.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnLookBack;
                @lookBack.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnLookBack;
                @Item.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnItem;
                @Item.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnItem;
                @Item.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnItem;
                @Respawn.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnRespawn;
                @Respawn.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnRespawn;
                @Respawn.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnRespawn;
                @MenuNav.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnMenuNav;
                @MenuNav.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnMenuNav;
                @MenuNav.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnMenuNav;
                @OpenMenuInRace.started -= m_Wrapper.m_RacerActionsCallbackInterface.OnOpenMenuInRace;
                @OpenMenuInRace.performed -= m_Wrapper.m_RacerActionsCallbackInterface.OnOpenMenuInRace;
                @OpenMenuInRace.canceled -= m_Wrapper.m_RacerActionsCallbackInterface.OnOpenMenuInRace;
            }
            m_Wrapper.m_RacerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Accellerate.started += instance.OnAccellerate;
                @Accellerate.performed += instance.OnAccellerate;
                @Accellerate.canceled += instance.OnAccellerate;
                @backwards.started += instance.OnBackwards;
                @backwards.performed += instance.OnBackwards;
                @backwards.canceled += instance.OnBackwards;
                @RotateCam.started += instance.OnRotateCam;
                @RotateCam.performed += instance.OnRotateCam;
                @RotateCam.canceled += instance.OnRotateCam;
                @lookBack.started += instance.OnLookBack;
                @lookBack.performed += instance.OnLookBack;
                @lookBack.canceled += instance.OnLookBack;
                @Item.started += instance.OnItem;
                @Item.performed += instance.OnItem;
                @Item.canceled += instance.OnItem;
                @Respawn.started += instance.OnRespawn;
                @Respawn.performed += instance.OnRespawn;
                @Respawn.canceled += instance.OnRespawn;
                @MenuNav.started += instance.OnMenuNav;
                @MenuNav.performed += instance.OnMenuNav;
                @MenuNav.canceled += instance.OnMenuNav;
                @OpenMenuInRace.started += instance.OnOpenMenuInRace;
                @OpenMenuInRace.performed += instance.OnOpenMenuInRace;
                @OpenMenuInRace.canceled += instance.OnOpenMenuInRace;
            }
        }
    }
    public RacerActions @Racer => new RacerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_moveMenu;
    private readonly InputAction m_Menu_enter;
    private readonly InputAction m_Menu_back;
    public struct MenuActions
    {
        private @RacerControl m_Wrapper;
        public MenuActions(@RacerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @moveMenu => m_Wrapper.m_Menu_moveMenu;
        public InputAction @enter => m_Wrapper.m_Menu_enter;
        public InputAction @back => m_Wrapper.m_Menu_back;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @moveMenu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveMenu;
                @moveMenu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveMenu;
                @moveMenu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMoveMenu;
                @enter.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnEnter;
                @enter.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnEnter;
                @enter.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnEnter;
                @back.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @back.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
                @back.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @moveMenu.started += instance.OnMoveMenu;
                @moveMenu.performed += instance.OnMoveMenu;
                @moveMenu.canceled += instance.OnMoveMenu;
                @enter.started += instance.OnEnter;
                @enter.performed += instance.OnEnter;
                @enter.canceled += instance.OnEnter;
                @back.started += instance.OnBack;
                @back.performed += instance.OnBack;
                @back.canceled += instance.OnBack;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_NewcontrolschemeSchemeIndex = -1;
    public InputControlScheme NewcontrolschemeScheme
    {
        get
        {
            if (m_NewcontrolschemeSchemeIndex == -1) m_NewcontrolschemeSchemeIndex = asset.FindControlSchemeIndex("New control scheme");
            return asset.controlSchemes[m_NewcontrolschemeSchemeIndex];
        }
    }
    private int m_Newcontrolscheme1SchemeIndex = -1;
    public InputControlScheme Newcontrolscheme1Scheme
    {
        get
        {
            if (m_Newcontrolscheme1SchemeIndex == -1) m_Newcontrolscheme1SchemeIndex = asset.FindControlSchemeIndex("New control scheme1");
            return asset.controlSchemes[m_Newcontrolscheme1SchemeIndex];
        }
    }
    public interface IRacerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnAccellerate(InputAction.CallbackContext context);
        void OnBackwards(InputAction.CallbackContext context);
        void OnRotateCam(InputAction.CallbackContext context);
        void OnLookBack(InputAction.CallbackContext context);
        void OnItem(InputAction.CallbackContext context);
        void OnRespawn(InputAction.CallbackContext context);
        void OnMenuNav(InputAction.CallbackContext context);
        void OnOpenMenuInRace(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnMoveMenu(InputAction.CallbackContext context);
        void OnEnter(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
}
