using UnityEngine;

namespace LocationRPG
{
    public class MonsterController : UnitController<Monster>
    {
        public override void MoveToEnemy(GameObject enemy)
        {
            float z = enemy.GetComponent<Collider>().bounds.size.z;

            float offset = (z + 0.4f);

            MoveToGameObject(enemy, offset);
        }

        public override void MoveToPlace()
        {
            //Monster position in combat scene is (0, 0, 4)
            MoveToPosition(new Vector3(0,0,4));
        }
    }
}