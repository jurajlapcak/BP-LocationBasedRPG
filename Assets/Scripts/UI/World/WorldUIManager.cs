using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    //Manages buttons that manipulate with UI elements
    public class WorldUIManager : VisualElement
    {
        private VisualElement _UIOverlay;
        private VisualElement _UICharacter;
        private VisualElement _UIMenuManager;
        private VisualElement _UIMenu;
        private VisualElement _UIOptionsMenu;
        public new class UxmlFactory : UxmlFactory<WorldUIManager, UxmlTraits> { }

        public WorldUIManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            _UIOverlay = this.Q("UIOverlay");
            _UICharacter = this.Q("UICharacter");
            _UIMenuManager = this.Q("UIMenuManager");
            _UIMenu = _UIMenuManager.Q("UIMenu");
            _UIOptionsMenu = _UIMenuManager.Q("UIOptionsMenu");

            _UIOverlay.Q("menuButton").RegisterCallback<ClickEvent>(ev => EnableMenuScreen());
            _UIOverlay.Q("characterButton").RegisterCallback<ClickEvent>(ev => EnableCharacterScreen());
            _UIMenu.Q("optionsButton").RegisterCallback<ClickEvent>(ev => EnableOptionsScreen());
            
            _UICharacter.Q("closeButton").RegisterCallback<ClickEvent>(ev => EnableUIOverlay());
            _UIMenu.Q("closeButton").RegisterCallback<ClickEvent>(ev => EnableUIOverlay());
            _UIOptionsMenu.Q("closeButton").RegisterCallback<ClickEvent>(ev => EnableUIOverlay());
            
        }
        
        void EnableUIOverlay()
        {
            _UIOverlay.style.display = DisplayStyle.Flex;
            
            _UIOptionsMenu.style.display = DisplayStyle.None;
            _UIMenuManager.style.display = DisplayStyle.None;
            _UICharacter.style.display = DisplayStyle.None;
        }
        
        void EnableMenuScreen()
        {
            _UIMenuManager.style.display = DisplayStyle.Flex;
            _UIMenu.style.display = DisplayStyle.Flex;
            _UIOverlay.style.display = DisplayStyle.Flex;
            _UICharacter.style.display = DisplayStyle.None;
        }

        void EnableCharacterScreen()
        {
            _UICharacter.style.display = DisplayStyle.Flex;
            _UIOverlay.style.display = DisplayStyle.None;
            _UIMenuManager.style.display = DisplayStyle.None;
        }
        
        //TODO
        void EnableOptionsScreen()
        {
            _UIOptionsMenu.style.display = DisplayStyle.Flex;
            _UIMenu.style.display = DisplayStyle.None;
        }
    }
}