using System;
using LocationRPG.Partials;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace LocationRPG
{
    public class WorldUIManager : MonoBehaviour
    {
        [SerializeField] private WorldOverlay worldOverlay;
        [SerializeField] private CharacterOverlay characterOverlay;
        [SerializeField] private MenuOverlay menuOverlay;
        [SerializeField] private OptionsOverlay optionsOverlay;

        private void OnEnable()
        {
            Assert.IsNotNull(worldOverlay);
            Assert.IsNotNull(characterOverlay);
            Assert.IsNotNull(menuOverlay);
            Assert.IsNotNull(optionsOverlay);

            worldOverlay.Root.RegisterCallback<GeometryChangedEvent>(WorldOverlayInit);
            characterOverlay.Root.RegisterCallback<GeometryChangedEvent>(CharacterOverlayInit);
            menuOverlay.Root.RegisterCallback<GeometryChangedEvent>(MenuOverlayInit);
            optionsOverlay.Root.RegisterCallback<GeometryChangedEvent>(OptionsOverlayInit);
        }

        private void WorldOverlayInit(GeometryChangedEvent evt)
        {
            ButtonInit(worldOverlay.CharacterButton, EnableCharacterScreen);
            ButtonInit(worldOverlay.MenuButton, EnableMenuScreen);
        }

        private void CharacterOverlayInit(GeometryChangedEvent evt)
        {
            ButtonInit(characterOverlay.CloseButton, EnableUIOverlay);
        }

        private void MenuOverlayInit(GeometryChangedEvent evt)
        {
            ButtonInit(menuOverlay.CloseButton, EnableUIOverlay);
            ButtonInit(menuOverlay.OptionsButton, EnableOptionsScreen);
        }

        private void OptionsOverlayInit(GeometryChangedEvent evt)
        {
            ButtonInit(optionsOverlay.CloseButton, EnableUIOverlay);
        }

        private delegate void EnableScreen();

        void ButtonInit(Button button, EnableScreen enableScreen)
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