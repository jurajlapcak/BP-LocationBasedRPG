using UnityEngine;

namespace LocationRPG
{
    public class InteractionManager : Singleton<InteractionManager>
    {
        private bool interactionLock;

        //true = locked interactions
        //false = unlocked interactions
        public bool InteractionLock => interactionLock;

        public void Lock()
        {
            interactionLock = true;

        }

        public void Unlock()
        {
            interactionLock = false;

        }
    }
}