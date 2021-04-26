namespace LocationRPG
{
    public class SkeletonController : MonsterController
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            _beforeAttackTime = 1f;
            _afterAttackTime = 0.1f;
        }
    }
}