﻿using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    [RequireComponent(typeof(UIDocument))]
    public class WorldOverlay : AbstractOverlay
    {
        private Button _menuButton;
        private Button _characterButton;

        private VisualElement _experienceBar;
        private VisualElement _healthBar;

        private Label _hp;
        private Label _xp;
        
        public Button MenuButton => _menuButton;
        public Button CharacterButton => _characterButton;

        public Label Hp => _hp;
        public Label Xp => _xp;
        private void OnEnable()
        {
            _experienceBar = null;
            _healthBar = null;

            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");

            _menuButton = _root.Q<Button>("menuButton");
            _characterButton = _root.Q<Button>("characterButton");

            _experienceBar = _root.Q("experienceBarBar");
            _healthBar = _root.Q("healthBarBar");

            _hp = _root.Q<Label>("hp");
            _xp = _root.Q<Label>("xp");
            
            _isInitialized = true;
        }

        private void Update()
        {
            if (!(_healthBar is null))
            {
                float currentHp = GameManager.Instance.CurrentPlayer.Unit.CurrentHp;
                float maxHp = GameManager.Instance.CurrentPlayer.Unit.Hp;
                
                UpdateBar(_healthBar, _hp, currentHp, maxHp);
            }

            if (!(_experienceBar is null))
            {
                float currentExp = GameManager.Instance.CurrentPlayer.Unit.Xp;
                float maxExp = GameManager.Instance.CurrentPlayer.Unit.RequiredXp;
                UpdateBar(_experienceBar, _xp, currentExp, maxExp);
            }
        }

        private void UpdateBar(VisualElement barFilling, Label label, float currentValue, float maxValue)
        {
            float ratio = currentValue > 0f ? currentValue / maxValue : 0.001f;

            label.text = currentValue + "/" + maxValue;
            barFilling.transform.scale = new Vector3(ratio, 1, 1);
        }
    }
}