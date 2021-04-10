using System;
using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    //Manages buttons that manipulate with UI elements
    public class WorldUIManager : VisualElement
    {
        private VisualElement _UIOverlay;
        private VisualElement _UICharacter;
        private VisualElement _UIMenu;
        private VisualElement _UIOptionsMenu;

        private VisualElement _UIMenuManager;

        private delegate void EnableScreen();

        //buttons from Assets/UI/Uxml/World/UIOverlay.uxml
        private Button _menuButton;
        private Button _characterButton;

        //buttons from Assets/UI/Uxml/World/UIMenu.uxml
        private Button _optionsButton;
        private Button _menuCloseButton;

        //buttons from Assets/UI/Uxml/World/UICharacter.uxml
        private Button _characterCloseButton;

        //buttons from Assets/UI/Uxml/World/UIOptionsMenu.uxml
        private Button _optionsCloseButton;

        public new class UxmlFactory : UxmlFactory<WorldUIManager, UxmlTraits>
        {
        }

        public WorldUIManager()
        {
            //GeometryChangedEvent fires when UI is rendered to screen
            RegisterCallback<GeometryChangedEvent>(Init);
        }

        void Init(GeometryChangedEvent evt)
        {
            _UIOverlay = this.Q("UIOverlay");
            _UICharacter = this.Q("UICharacter");
            _UIMenuManager = this.Q("UIMenuManager");

            _UIMenu = _UIMenuManager.Q("UIMenu");
            _UIOptionsMenu = _UIMenuManager.Q("UIOptionsMenu");


            //buttons from Assets/UI/Uxml/World/UIOverlay.uxml
            _menuButton = _UIOverlay.Q<Button>("menuButton");
            _characterButton = _UIOverlay.Q<Button>("characterButton");

            //buttons from Assets/UI/Uxml/World/UIMenu.uxml
            _optionsButton = _UIMenu.Q<Button>("optionsButton");
            _menuCloseButton = _UIMenu.Q<Button>("closeButton");

            //buttons from Assets/UI/Uxml/World/UICharacter.uxml
            _characterCloseButton = _UICharacter.Q<Button>("closeButton");

            //buttons from Assets/UI/Uxml/World/UIOptionsMenu.uxml
            _optionsCloseButton = _UIOptionsMenu.Q<Button>("closeButton");


            //initialize button and give the functions
            ButtonInit(_menuButton, EnableMenuScreen);
            ButtonInit(_characterButton, EnableCharacterScreen);
            ButtonInit(_optionsButton, EnableOptionsScreen);
            ButtonInit(_menuCloseButton, EnableUIOverlay);
            ButtonInit(_characterCloseButton, EnableUIOverlay);
            ButtonInit(_optionsCloseButton, EnableUIOverlay);
        }

        //clear callbacks and assign custom event for press down and up
        void ButtonInit(Button button, EnableScreen enableScreen)
        {

            button.clickable.activators.Clear();

            button.RegisterCallback<MouseDownEvent>(ev =>
            {
                LockInteractions();
                enableScreen();
            });
            button.RegisterCallback<MouseUpEvent>(ev =>
            {
                UnlockInteractions();
            });
        }

        void LockInteractions()
        {
            InteractionManager.Instance.Lock();
#if UNITY_EDITOR
            Debug.Log("panel-on");
#endif
        }

        void UnlockInteractions()
        {
            InteractionManager.Instance.Unlock();
#if UNITY_EDITOR
            Debug.Log("panel-off");
#endif
        }

        void EnableUIOverlay()
        {
            UnlockInteractions();

            _UIOverlay.style.display = DisplayStyle.Flex;
            _UIOptionsMenu.style.display = DisplayStyle.None;
            _UIMenuManager.style.display = DisplayStyle.None;
            _UICharacter.style.display = DisplayStyle.None;
        }

        void EnableMenuScreen()
        {
            LockInteractions();

            _UIMenuManager.style.display = DisplayStyle.Flex;
            _UIMenu.style.display = DisplayStyle.Flex;
            _UIOverlay.style.display = DisplayStyle.Flex;
            _UICharacter.style.display = DisplayStyle.None;
        }

        void EnableCharacterScreen()
        {
            LockInteractions();
            _UICharacter.style.display = DisplayStyle.Flex;
            _UIOverlay.style.display = DisplayStyle.None;
            _UIMenuManager.style.display = DisplayStyle.None;
        }

        //TODO
        void EnableOptionsScreen()
        {
            LockInteractions();
            _UIOptionsMenu.style.display = DisplayStyle.Flex;
            _UIMenu.style.display = DisplayStyle.None;
        }
    }
}