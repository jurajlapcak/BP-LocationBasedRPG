using System;
using UnityEditorInternal;
using UnityEngine.UIElements;
using UnityEngine;

namespace LocationRPG
{
    public class CombatUIManager : VisualElement
    {
        private CombatSystem _combatSystem;
        
        private Button _attackButton;
        private Button _defendButton;

        public new class UxmlFactory : UxmlFactory<CombatUIManager, UxmlTraits>
        {
        }

        public CombatUIManager()
        {
            RegisterCallback<GeometryChangedEvent>(Init);
        }

        void Init(GeometryChangedEvent evt)
        {
            _combatSystem = CombatSystem.Instance;
            
            _attackButton = this.Q<Button>("attackButton");
            _defendButton = this.Q<Button>("defendButton");
            
            //AttackButton initialization
            ButtonInit(_attackButton, _combatSystem.OnAttackButton);
            //DefendButton initialization
            ButtonInit(_defendButton, _combatSystem.OnDefendButton);
        }
        
        private delegate void ButtonEvent();
        
        void ButtonInit(Button button, ButtonEvent buttonEvent)
        {

            button.clickable.activators.Clear();

            button.RegisterCallback<MouseDownEvent>(ev =>
            {
                LockInteractions();
            });
            button.RegisterCallback<MouseUpEvent>(ev =>
            {
                UnlockInteractions();
                buttonEvent();
            });
        }
        
        void LockInteractions()
        {
            InteractionManager.Instance.Lock();
#if UNITY_EDITOR
            Debug.Log("lock");
#endif
        }

        void UnlockInteractions()
        {
            InteractionManager.Instance.Unlock();
#if UNITY_EDITOR
            Debug.Log("unlock");
#endif
        }
    }
}