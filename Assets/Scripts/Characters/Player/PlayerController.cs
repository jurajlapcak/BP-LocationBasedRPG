using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class PlayerController : UnitController<Player>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            _beforeAttackTime = 0.1f;
            _afterAttackTime = 0.5f;
        }

        protected override void Awake()
        {
            base.Awake();
            unit = new Player();
        }

        public void SavePlayer()
        {
            unit.Save();
        }

        public void LoadPlayer()
        {
            unit.Load();
        }

        public override void MoveToEnemy(GameObject enemy)
        {
            float z = enemy.GetComponent<Collider>().bounds.size.z;

            float offset = -(z + 0.3f);

            MoveToGameObject(enemy, offset);
        }

        public override void MoveToPlace()
        {
            MoveToPosition(Vector3.zero);
        }
    }
}