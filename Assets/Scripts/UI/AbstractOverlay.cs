using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG
{    
    public abstract class AbstractOverlay : MonoBehaviour
    {
        protected VisualElement _root;
        protected VisualElement _screen;
        
        public VisualElement Screen => _screen;
        public VisualElement Root => _root;

        protected abstract void OnEnable();

        public abstract void Init(GeometryChangedEvent evt);

        public void ShowOverlay()
        {
            _screen.style.display = DisplayStyle.Flex;
        }

        public void HideOverlay()
        {
            _screen.style.display = DisplayStyle.None;
        }
    }
}