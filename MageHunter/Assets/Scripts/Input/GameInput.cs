//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/Input/GameInput.inputactions
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

public partial class @GameInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""7938d478-7cb0-4c79-b7d0-a7bf5834528d"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""15be73e4-e9be-463d-88f2-b2e384647d24"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4f245337-4155-4dbb-9fae-edd275a28e86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LightSpell"",
                    ""type"": ""Button"",
                    ""id"": ""6777315c-0e00-406f-95c3-4468ff55e567"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HeavySpell"",
                    ""type"": ""Button"",
                    ""id"": ""1c0507b7-1037-43c9-9c7c-e345aa31c78f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""cc695c6e-451c-4d34-90e6-3220c7938ee6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""LightSpellScroll"",
                    ""type"": ""Value"",
                    ""id"": ""0c611b59-231d-4016-9d03-4badd41e57a0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""HeavySpellScroll"",
                    ""type"": ""Value"",
                    ""id"": ""8c70b980-b21c-4e29-92ba-5e1739525a51"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""QPressed"",
                    ""type"": ""Button"",
                    ""id"": ""deb12664-bb14-4d3c-ab1b-b32f1d2d55ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""7cbef775-d55a-4ddb-8084-2602bb98ee03"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""7f761535-13a2-4396-ad44-d3b5cb7e1ef4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""873f3d75-659e-4afc-82ca-6f4977943049"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""02f60f12-fa64-4775-bb87-d494c799f77b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""70be5ce9-bcdc-427b-97e1-45b9843c3307"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""50c7c8e4-4c37-490a-a335-36fa3d40fa65"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8656ecdb-4068-4b17-bbf7-2012c5bc9076"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20b12b6f-1256-489b-b40b-ead9d98988da"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d9fcb10-43e2-499b-b10f-a344b84e3866"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97168db7-f0a9-40c0-9efb-bf9ec0ea9d13"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LightSpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e4162be-73a3-47e1-a8a6-d667a291fd07"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""HeavySpell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b680d7f8-b1ae-4f51-b737-4b3f4361dde6"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""706f0603-831a-40b4-b90f-7716dbb24b9c"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LightSpellScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bee4769b-5a2e-402a-966f-82d364ec6d65"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""HeavySpellScroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0fd9df14-976c-4f26-8e33-a1a578f47419"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""QPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
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
        }
    ]
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_Move = m_InGame.FindAction("Move", throwIfNotFound: true);
        m_InGame_Jump = m_InGame.FindAction("Jump", throwIfNotFound: true);
        m_InGame_LightSpell = m_InGame.FindAction("LightSpell", throwIfNotFound: true);
        m_InGame_HeavySpell = m_InGame.FindAction("HeavySpell", throwIfNotFound: true);
        m_InGame_MousePos = m_InGame.FindAction("MousePos", throwIfNotFound: true);
        m_InGame_LightSpellScroll = m_InGame.FindAction("LightSpellScroll", throwIfNotFound: true);
        m_InGame_HeavySpellScroll = m_InGame.FindAction("HeavySpellScroll", throwIfNotFound: true);
        m_InGame_QPressed = m_InGame.FindAction("QPressed", throwIfNotFound: true);
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

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_Move;
    private readonly InputAction m_InGame_Jump;
    private readonly InputAction m_InGame_LightSpell;
    private readonly InputAction m_InGame_HeavySpell;
    private readonly InputAction m_InGame_MousePos;
    private readonly InputAction m_InGame_LightSpellScroll;
    private readonly InputAction m_InGame_HeavySpellScroll;
    private readonly InputAction m_InGame_QPressed;
    public struct InGameActions
    {
        private @GameInput m_Wrapper;
        public InGameActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_InGame_Move;
        public InputAction @Jump => m_Wrapper.m_InGame_Jump;
        public InputAction @LightSpell => m_Wrapper.m_InGame_LightSpell;
        public InputAction @HeavySpell => m_Wrapper.m_InGame_HeavySpell;
        public InputAction @MousePos => m_Wrapper.m_InGame_MousePos;
        public InputAction @LightSpellScroll => m_Wrapper.m_InGame_LightSpellScroll;
        public InputAction @HeavySpellScroll => m_Wrapper.m_InGame_HeavySpellScroll;
        public InputAction @QPressed => m_Wrapper.m_InGame_QPressed;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnJump;
                @LightSpell.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnLightSpell;
                @LightSpell.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnLightSpell;
                @LightSpell.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnLightSpell;
                @HeavySpell.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnHeavySpell;
                @HeavySpell.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnHeavySpell;
                @HeavySpell.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnHeavySpell;
                @MousePos.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMousePos;
                @LightSpellScroll.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnLightSpellScroll;
                @LightSpellScroll.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnLightSpellScroll;
                @LightSpellScroll.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnLightSpellScroll;
                @HeavySpellScroll.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnHeavySpellScroll;
                @HeavySpellScroll.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnHeavySpellScroll;
                @HeavySpellScroll.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnHeavySpellScroll;
                @QPressed.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnQPressed;
                @QPressed.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnQPressed;
                @QPressed.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnQPressed;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LightSpell.started += instance.OnLightSpell;
                @LightSpell.performed += instance.OnLightSpell;
                @LightSpell.canceled += instance.OnLightSpell;
                @HeavySpell.started += instance.OnHeavySpell;
                @HeavySpell.performed += instance.OnHeavySpell;
                @HeavySpell.canceled += instance.OnHeavySpell;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @LightSpellScroll.started += instance.OnLightSpellScroll;
                @LightSpellScroll.performed += instance.OnLightSpellScroll;
                @LightSpellScroll.canceled += instance.OnLightSpellScroll;
                @HeavySpellScroll.started += instance.OnHeavySpellScroll;
                @HeavySpellScroll.performed += instance.OnHeavySpellScroll;
                @HeavySpellScroll.canceled += instance.OnHeavySpellScroll;
                @QPressed.started += instance.OnQPressed;
                @QPressed.performed += instance.OnQPressed;
                @QPressed.canceled += instance.OnQPressed;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface IInGameActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLightSpell(InputAction.CallbackContext context);
        void OnHeavySpell(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnLightSpellScroll(InputAction.CallbackContext context);
        void OnHeavySpellScroll(InputAction.CallbackContext context);
        void OnQPressed(InputAction.CallbackContext context);
    }
}
