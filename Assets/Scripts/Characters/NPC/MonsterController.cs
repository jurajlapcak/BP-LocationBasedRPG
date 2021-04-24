using UnityEngine;

namespace LocationRPG
{
    public class MonsterController : UnitController<Monster>
    {
        public void MoveToPlayer(GameObject player)
        {
            float z = player.GetComponent<Collider>().bounds.size.z;

            float offset = (z + 0.4f);

            MoveToGameObject(player, offset);
        }

        public void MoveToPlace()
        {
            MoveToPosition(new Vector3(0,0,4));
        }
    }
}