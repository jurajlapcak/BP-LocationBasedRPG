using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    [RequireComponent(typeof(UIDocument))]
    public class CombatOverlay : AbstractOverlay
    {
        private Button _attackButton;
        private Button _defendButton;

        public Button AttackButton => _attackButton;

        public Button DefendButton => _defendButton;
        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }
        
        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");

            _attackButton = _root.Q<Button>("attackButton");
            _defendButton = _root.Q<Button>("defendButton");

            _isInitialized = true;
        }
    }
}