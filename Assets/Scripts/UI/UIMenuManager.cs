using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    //Manages buttons for options and exiting game
    /*TODO: Controls options for audio and SAVING*/
          
    public class UIMenuManager : VisualElement
    {
        private VisualElement m_UIMenu;
        private VisualElement m_UIOptionsMenu;

        public new class UxmlFactory : UxmlFactory<UIMenuManager, UxmlTraits> { }

        public UIMenuManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            m_UIOptionsMenu = this.Q("UIOptionsMenu");
            m_UIMenu = this.Q("UIMenu");

            this.Q("exitButton").RegisterCallback<ClickEvent>(ev => QuitGame());
        }
        void QuitGame()
        {
            Debug.Log("Game is exiting");
            //TODO save
            Application.Quit();
        }

    }
}
