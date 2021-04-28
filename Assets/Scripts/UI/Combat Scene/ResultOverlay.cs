using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    [RequireComponent(typeof(UIDocument))]
    public class ResultOverlay : AbstractOverlay
    {
        private Label _winText;
        private Label _loseText;

        public Label WinText => _winText;

        public Label LoseText => _loseText;

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");

            _loseText = _root.Q<Label>("lose-text");
            _winText = _root.Q<Label>("win-text");

            _isInitialized = true;
        }

        public void ShowWin()
        {
            _screen.style.display = DisplayStyle.Flex;
            _winText.style.display = DisplayStyle.Flex;
            HideLose();
        }

        public void HideWin()
        {
            _winText.style.display = DisplayStyle.None;
        }

        public void ShowLose()
        {
            _screen.style.display = DisplayStyle.Flex;
            _loseText.style.display = DisplayStyle.Flex;
            HideWin();
        }

        public void HideLose()
        {
            _loseText.style.display = DisplayStyle.None;
        }
    }
}