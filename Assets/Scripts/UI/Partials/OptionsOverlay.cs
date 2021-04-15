using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG
{
    
    [RequireComponent(typeof(UIDocument))]
    public class OptionsOverlay : AbstractOverlay
    {
        private Button _closeButton;

        public Button CloseButton => _closeButton;

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");
            _closeButton = _root.Q<Button>("closeButton");
            
            _isInitialized = true;
        }
        
        public void Test()
        {
            Debug.Log("OptionsOverlay");
        }
    }
}