﻿using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG.Partials
{
    [RequireComponent(typeof(UIDocument))]
    public class MenuOverlay : AbstractOverlay
    {
        private Button _closeButton;
        private Button _optionsButton;
        private Button _exitButton;
        
        public Button CloseButton => _closeButton;
        public Button OptionsButton => _optionsButton;

        protected override void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");
            
            _closeButton = _root.Q<Button>("closeButton");
            _optionsButton = _root.Q<Button>("optionsButton");
            _exitButton = _root.Q<Button>("exitButton");
            
            _exitButton.RegisterCallback<ClickEvent>(ev => QuitGame());
        }
        
        void QuitGame()
        {
            Debug.Log("Game is exiting");
            
            GameManager.Instance.Save();
            
            Application.Quit();
        }
    }
}