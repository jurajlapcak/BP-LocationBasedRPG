using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    //Manages buttons for options and exiting game
    /*TODO: Controls options for audio and SAVING*/
          
    public class UIMenuManager : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<UIMenuManager, UxmlTraits> { }

        public UIMenuManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
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
