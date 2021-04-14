using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    [RequireComponent(typeof(UIDocument))]
    public class WorldOverlay : AbstractOverlay
    {
        private Button _menuButton;
        private Button _characterButton;

        public Button MenuButton => _menuButton;
        public Button CharacterButton => _characterButton;

        protected override void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");

            _menuButton = _root.Q<Button>("menuButton");
            _characterButton = _root.Q<Button>("characterButton");
        }
    }
}