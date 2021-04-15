using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace LocationRPG
{
    public class WorldUIManager : MonoBehaviour
    {
        [SerializeField] private CharacterOverlay characterOverlay;
        [SerializeField] private WorldOverlay worldOverlay;
        [SerializeField] private MenuOverlay menuOverlay;
        [SerializeField] private OptionsOverlay optionsOverlay;

        private bool _isInitilized;

        public bool IsInitilized => _isInitilized;

        private void OnEnable()
        {
            _isInitilized = false;
            Assert.IsNotNull(characterOverlay);
            Assert.IsNotNull(worldOverlay);
            Assert.IsNotNull(menuOverlay);
            Assert.IsNotNull(optionsOverlay);

            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            //https://forum.unity.com/threads/rootvisualelement-is-null-onenable-using-the-built-in-uitoolkit-in-2021-2.1068176/
            //wait until all roots have been initialized, bug in UI Toolkit
            yield return new WaitUntil(() => worldOverlay.IsInitialized);
            WorldOverlayInit();

            yield return new WaitUntil(() => characterOverlay.IsInitialized);
            CharacterOverlayInit();

            yield return new WaitUntil(() => menuOverlay.IsInitialized);
            MenuOverlayInit();

            yield return new WaitUntil(() => optionsOverlay.IsInitialized);
            OptionsOverlayInit();

            Debug.Log("Initilized all");
            _isInitilized = true;
        }

        //GeometryChangedEvent evt
        private void WorldOverlayInit()
        {
            ButtonInit(worldOverlay.CharacterButton, EnableCharacterScreen);
            ButtonInit(worldOverlay.MenuButton, EnableMenuScreen);
        }

        private void CharacterOverlayInit()
        {
            ButtonInit(characterOverlay.CloseButton, EnableUIOverlay);
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

        private delegate void EnableScreen();

        private void ButtonInit(Button button, EnableScreen enableScreen)
        {
            button.clickable.activators.Clear();

            button.RegisterCallback<MouseDownEvent>(ev => { LockInteractions(); });
            button.RegisterCallback<MouseUpEvent>(ev =>
            {
                UnlockInteractions();
                enableScreen();
            });
        }

        void LockInteractions()
        {
            InteractionManager.Instance.Lock();
        }

        void UnlockInteractions()
        {
            InteractionManager.Instance.Unlock();
        }

        void EnableUIOverlay()
        {
            UnlockInteractions();

            worldOverlay.ShowOverlay();
            optionsOverlay.HideOverlay();
            menuOverlay.HideOverlay();
            characterOverlay.HideOverlay();
        }

        void EnableMenuScreen()
        {
            LockInteractions();

            menuOverlay.ShowOverlay();
            worldOverlay.ShowOverlay();
            characterOverlay.HideOverlay();
        }

        void EnableCharacterScreen()
        {
            LockInteractions();
            characterOverlay.ShowOverlay();
            worldOverlay.HideOverlay();
            menuOverlay.HideOverlay();
            optionsOverlay.HideOverlay();
        }

        void EnableOptionsScreen()
        {
            LockInteractions();

            optionsOverlay.ShowOverlay();
            worldOverlay.ShowOverlay();
            menuOverlay.HideOverlay();
            characterOverlay.HideOverlay();
        }
    }
}