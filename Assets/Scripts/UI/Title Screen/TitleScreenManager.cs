using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace LocationRPG
{
    // manages title screen
    public class TitleScreenManager : VisualElement
    {
        private VisualElement _playButton;
        private VisualElement _optionsButton;
        private VisualElement _exitButton;

        public new class UxmlFactory : UxmlFactory<TitleScreenManager, UxmlTraits>
        {
        }

        public TitleScreenManager()
        {
            RegisterCallback<GeometryChangedEvent>(OnGeometryChange);
        }

        void OnGeometryChange(GeometryChangedEvent evt)
        {
            _playButton = this.Q("playButton");
            _optionsButton = this.Q("optionsButton");
            _exitButton = this.Q("exitButton");

            _playButton.RegisterCallback<ClickEvent>(ev => Play());
            _optionsButton.RegisterCallback<ClickEvent>(ev => ShowOptions());
            _exitButton.RegisterCallback<ClickEvent>(ev => Application.Quit());
        }

        void Play()
        {
            SceneManager.LoadScene(SceneNameConstants.WORLD);
        }

        void ShowOptions()
        {
        }
    }
}