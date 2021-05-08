using UnityEngine;

namespace LocationRPG
{
    public class InteractionManager : Singleton<InteractionManager>
    {
        private bool _interactionLock;

        //true = locked interactions
        //false = unlocked interactions
        public bool InteractionLock => _interactionLock;

        public void Lock()
        {
            _interactionLock = true;
        }

        public void Unlock()
        {
            _interactionLock = false;

        }
    }
}