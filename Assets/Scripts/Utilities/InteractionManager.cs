namespace LocationRPG
{
    public class InteractionManager : SingletonDontDestroy<InteractionManager>
    {
        //https://forum.unity.com/threads/is-there-a-way-to-block-raycasts.943963/
        //https://forum.unity.com/threads/ui-toolkit-and-raycast-block.1034938/

        private bool interactionLock;


        //true = locked interactions
        //false = unlocked interactions
        public bool InteractionLock
        {
            get { return interactionLock; }
        }

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