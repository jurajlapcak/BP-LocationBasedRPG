using UnityEngine;
using UnityEngine.UIElements;

namespace LocationRPG
{
    public abstract class AbstractUIManager : MonoBehaviour
    {
        protected bool _isInitilized;

        public bool IsInitilized => _isInitilized;
        
        protected delegate void EnableScreen();

        protected void ButtonInit(Button button, EnableScreen enableScreen)
        {
            button.clickable.activators.Clear();

            button.RegisterCallback<MouseDownEvent>(ev => { LockInteractions(); });
            button.RegisterCallback<MouseUpEvent>(ev =>
            {
                UnlockInteractions();
                enableScreen();
            });
        }

        protected void LockInteractions()
        {
            InteractionManager.Instance.Lock();
        }

        protected void UnlockInteractions()
        {
            InteractionManager.Instance.Unlock();
        }
    }
}