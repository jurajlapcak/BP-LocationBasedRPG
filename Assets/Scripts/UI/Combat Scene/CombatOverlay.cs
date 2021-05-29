using System;
using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    public enum CombatBars
    {
        PlayerBar,
        MonsterBar
    }

    [RequireComponent(typeof(UIDocument))]
    public class CombatOverlay : AbstractOverlay
    {
        private Button _menuButton;

        private Button _attackButton;
        private Button _defendButton;

        private VisualElement _playerHealthBar;
        private VisualElement _monsterHealthBar;

        private Label _playerHp;
        private Label _monsterHp;

        public Button MenuButton => _menuButton;

        public VisualElement PlayerHealthBar => _playerHealthBar;
        public VisualElement MonsterHealthBar => _monsterHealthBar;

        public Button AttackButton => _attackButton;
        public Button DefendButton => _defendButton;

        public Label PlayerHp => _playerHp;
        public Label MonsterHp => _monsterHp;


        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");

            _menuButton = _root.Q<Button>("menuButton");

            _attackButton = _root.Q<Button>("attackButton");
            _defendButton = _root.Q<Button>("defendButton");

            _playerHealthBar = _root.Q("playerHealthBarBar");
            _monsterHealthBar = _root.Q("monsterHealthBarBar");

            _playerHp = _root.Q<Label>("playerHp");
            _monsterHp = _root.Q<Label>("monsterHp");

            _isInitialized = true;
        }

        public void UpdateBar(CombatBars bar, double currentValue, double maxValue)
        {
            
            double ratio = currentValue > 0f ? currentValue / maxValue : 0.001f;
            if (bar == CombatBars.PlayerBar)
            {
                _playerHp.text = Math.Round(currentValue, 3) + "/" + Math.Round(maxValue, 3);
                _playerHealthBar.transform.scale = new Vector3((float)ratio, 1, 1);
            }
            else
            {
                _monsterHp.text = Math.Round(currentValue, 3) + "/" + Math.Round(maxValue, 3);
                _monsterHealthBar.style.left = new StyleLength(Length.Percent((float) (100-(ratio * 100))));
                _monsterHealthBar.transform.scale = new Vector3((float)ratio, 1, 1);
            }
        }
    }
}