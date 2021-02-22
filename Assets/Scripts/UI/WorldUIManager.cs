using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    //Manages buttons that manipulate with UI elements
    public class WorldUIManager : VisualElement
    {
        private VisualElement m_UIOverlay;
        private VisualElement m_UICharacter;
        private VisualElement m_UIMenuManager;
        private VisualElement m_UIMenu;
        private VisualElement m_UIOptionsMenu;
        public new class UxmlFactory : UxmlFactory<WorldUIManager, UxmlTraits> { }

        public WorldUIManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            m_UIOverlay = this.Q("UIOverlay");
            m_UICharacter = this.Q("UICharacter");
            m_UIMenuManager = this.Q("UIMenuManager");
            m_UIMenu = m_UIMenuManager.Q("UIMenu");
            m_UIOptionsMenu = m_UIMenuManager.Q("UIOptionsMenu");

            m_UIOverlay.Q("menuButton").RegisterCallback<ClickEvent>(ev => EnableMenuScreen());
            m_UIOverlay.Q("characterButton").RegisterCallback<ClickEvent>(ev => EnableCharacterScreen());
            m_UIMenu.Q("optionsButton").RegisterCallback<ClickEvent>(ev => EnableOptionsScreen());
            
            m_UICharacter.Q("closeButton").RegisterCallback<ClickEvent>(ev => EnableUIOverlay());
            m_UIMenu.Q("closeButton").RegisterCallback<ClickEvent>(ev => EnableUIOverlay());
            m_UIOptionsMenu.Q("closeButton").RegisterCallback<ClickEvent>(ev => EnableUIOverlay());
            
        }
        
        void EnableUIOverlay()
        {
            m_UIOverlay.style.display = DisplayStyle.Flex;
            
            m_UIOptionsMenu.style.display = DisplayStyle.None;
            m_UIMenuManager.style.display = DisplayStyle.None;
            m_UICharacter.style.display = DisplayStyle.None;
        }
        
        void EnableMenuScreen()
        {
            m_UIMenuManager.style.display = DisplayStyle.Flex;
            m_UIOverlay.style.display = DisplayStyle.Flex;
            m_UICharacter.style.display = DisplayStyle.None;
        }

        void EnableCharacterScreen()
        {
            m_UICharacter.style.display = DisplayStyle.Flex;
            m_UIOverlay.style.display = DisplayStyle.None;
            m_UIMenuManager.style.display = DisplayStyle.None;
        }
        
        //TODO
        void EnableOptionsScreen()
        {
            Debug.Log("EnableOptions");

            m_UIOptionsMenu.style.display = DisplayStyle.Flex;
            m_UIMenu.style.display = DisplayStyle.None;
        }
        

    }
}