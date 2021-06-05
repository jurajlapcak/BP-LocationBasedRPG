using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG
{
    [RequireComponent(typeof(UIDocument))]
    public class CharacterOverlay : AbstractOverlay
    {
        [SerializeField] private LocationRPG.PositionWithLocationProvider position;
        [SerializeField] private PlayerController player;

        private Button _closeButton;

        public Button CloseButton => _closeButton;

        private Label _level;
        private Label _xp;
        private Label _xpSummary;
        private Label _xpRequired;
        private Label _health;
        private Label _attack;
        private Label _distanceCovered;

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }

        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");
            _closeButton = _root.Q<Button>("closeButton");

            _level = _root.Q<Label>("level");
            _xp = _root.Q<Label>("xp");
            _xpSummary = _root.Q<Label>("xpSummary");
            _xpRequired = _root.Q<Label>("xpRequired");
            _health = _root.Q<Label>("health");
            _attack = _root.Q<Label>("attack");
            _distanceCovered = _root.Q<Label>("distanceCovered");

            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _level.text = ReduceBigNumber(player.Unit.Level);
                _xp.text = ReduceBigNumber(player.Unit.Xp);
                _xpSummary.text = ReduceBigNumber(player.Unit.XpSum);
                _xpRequired.text = ReduceBigNumber(player.Unit.RequiredXp);
                _health.text = ReduceBigNumber(player.Unit.Hp);
                _attack.text = ReduceBigNumber(player.Unit.Attack);
                
                if(position.IsInitialized){
                    _distanceCovered.text = ReduceBigNumber(position.DistanceController.DistanceCovered);
                }
            }
        }
    }
}