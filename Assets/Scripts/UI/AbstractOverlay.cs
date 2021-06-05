using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG
{    
    public abstract class AbstractOverlay : MonoBehaviour
    {
        protected VisualElement _root;
        protected VisualElement _screen;

        protected bool _isInitialized = false;
        
        public VisualElement Screen => _screen;
        public VisualElement Root => _root;
        public bool IsInitialized => _isInitialized;
        
        public abstract void Init(GeometryChangedEvent evt);

        public void ShowOverlay()
        {
            _screen.style.display = DisplayStyle.Flex;
        }

        public void HideOverlay()
        {
            _screen.style.display = DisplayStyle.None;
        }
        
        protected string ReduceBigNumber(double number)
        {
            string thousands = "";

            while (number > 100000)
            {
                number /= 1000;
                thousands += "K";
            }
            return Math.Round(number, 3) + thousands;
        }
    }
}