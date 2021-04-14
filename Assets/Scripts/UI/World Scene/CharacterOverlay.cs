using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG
{    
    [RequireComponent(typeof(UIDocument))]
    public class CharacterOverlay : AbstractOverlay
    {
        private Button _closeButton;
        
        public Button CloseButton => _closeButton;

        protected override void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }
        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");
            _closeButton = _root.Q<Button>("closeButton");
        }
        
    }
}