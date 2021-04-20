using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class CombatUIManager : AbstractUIManager
    {
        [SerializeField] private CombatOverlay combatOverlay;
        [SerializeField] private MenuOverlay menuOverlay;
        [SerializeField] private OptionsOverlay optionsOverlay;

        private CombatSystem _combatSystem;

        public CombatOverlay CombatOverlay => combatOverlay;
        public MenuOverlay MenuOverlay => menuOverlay;
        public OptionsOverlay OptionsOverlay => optionsOverlay;
        
        private void OnEnable()
        {
            _isInitialized = false;
            Assert.IsNotNull(combatOverlay);
            Assert.IsNotNull(menuOverlay);
            Assert.IsNotNull(optionsOverlay);

            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            _combatSystem = CombatSystem.Instance;

            //https://forum.unity.com/threads/rootvisualelement-is-null-onenable-using-the-built-in-uitoolkit-in-2021-2.1068176/
            //wait until all roots have been initialized, bug in UI Toolkit

            yield return new WaitUntil(() => combatOverlay.IsInitialized);
            CombatOverlayInit();

            yield return new WaitUntil(() => menuOverlay.IsInitialized);
            MenuOverlayInit();

            yield return new WaitUntil(() => optionsOverlay.IsInitialized);
            OptionsOverlayInit();

            Debug.Log("Initilized all");
            _isInitialized = true;
        }

        private void CombatOverlayInit()
        {
            //MenuButton initialization
            ButtonInit(combatOverlay.MenuButton, EnableMenuScreen);
            //AttackButton initialization
            ButtonInit(combatOverlay.AttackButton, _combatSystem.OnAttackButton);
            //DefendButton initialization
            ButtonInit(combatOverlay.DefendButton, _combatSystem.OnDefendButton);
        }

        private void MenuOverlayInit()
        {
            ButtonInit(menuOverlay.CloseButton, EnableUIOverlay);
            ButtonInit(menuOverlay.OptionsButton, EnableOptionsScreen);
        }

        private void OptionsOverlayInit()
        {
            ButtonInit(optionsOverlay.CloseButton, EnableUIOverlay);
        }


        private void EnableUIOverlay()
        {
            UnlockInteractions();
            combatOverlay.ShowOverlay();
            menuOverlay.HideOverlay();
            optionsOverlay.HideOverlay();
        }

        private void EnableMenuScreen()
        {
            LockInteractions();
            combatOverlay.ShowOverlay();
            menuOverlay.ShowOverlay();
            optionsOverlay.HideOverlay();
        }
        
        private void EnableOptionsScreen()
        {
            LockInteractions();
            combatOverlay.ShowOverlay();
            menuOverlay.HideOverlay();
            optionsOverlay.ShowOverlay();
        }
        
        
    }
}