using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    [RequireComponent(typeof(UIDocument))]
    public class WorldOverlay : AbstractOverlay
    {
        private Button _menuButton;
        private Button _characterButton;

        private VisualElement _experienceBarFilling;
        private VisualElement _healthBarFilling;

        public Button MenuButton => _menuButton;
        public Button CharacterButton => _characterButton;

        private void OnEnable()
        {
            _experienceBarFilling = null;
            _healthBarFilling = null;

            _root = GetComponent<UIDocument>().rootVisualElement;
            _root.RegisterCallback<GeometryChangedEvent>(Init);
        }
        
        public override void Init(GeometryChangedEvent evt)
        {
            _screen = _root.Q("screen");

            _menuButton = _root.Q<Button>("menuButton");
            _characterButton = _root.Q<Button>("characterButton");

            _experienceBarFilling = _root.Q("experienceBarFilling");
            _healthBarFilling = _root.Q("healthBarFilling");

            _isInitialized = true;
        }

        private void Update()
        {
            if (!(_experienceBarFilling is null))
            {
                float currentHp = GameManager.Instance.CurrentPlayer.Player.CurrentHp;
                float maxHp = GameManager.Instance.CurrentPlayer.Player.Hp;
                UpdateBar(_experienceBarFilling, currentHp, maxHp);
            }

            if (!(_healthBarFilling is null))
            {
                float currentExp = GameManager.Instance.CurrentPlayer.Player.Xp;
                float maxExp = GameManager.Instance.CurrentPlayer.Player.RequiredXp;
                UpdateBar(_healthBarFilling, currentExp, maxExp);
            }
        }

        private void UpdateBar(VisualElement barFilling, float currentValue, float maxValue)
        {
            float ratio = currentValue > 0f ? currentValue / maxValue : 0.001f;

            barFilling.transform.scale = new Vector3(ratio, 1, 1);
        }
        
        public void Test()
        {
            Debug.Log("WorldOverlay");
        }
    }
}