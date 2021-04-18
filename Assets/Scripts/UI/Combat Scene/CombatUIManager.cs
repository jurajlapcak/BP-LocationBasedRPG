using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace LocationRPG
{
    public class CombatUIManager : AbstractUIManager
    {
        [SerializeField] private CombatOverlay combatOverlay;
        [SerializeField] private MenuOverlay menuOverlay;
        [SerializeField] private OptionsOverlay optionsOverlay;

        private CombatSystem _combatSystem;


        private void OnEnable()
        {
            _isInitilized = false;
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
            _isInitilized = true;
        }

        private void CombatOverlayInit()
        {
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
            throw new System.NotImplementedException();
        }

        private void EnableOptionsScreen()
        {
            throw new System.NotImplementedException();
        }
    }
}