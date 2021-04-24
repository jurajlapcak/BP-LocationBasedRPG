using UnityEngine;
using UnityEngine.Assertions;

namespace LocationRPG
{
    public class PlayerController : UnitController<Player>
    {
        public void SavePlayer()
        {
            unit.Save();
        }

        public void LoadPlayer()
        {
            unit.Load();
        }

        public void MoveToMonster(GameObject monster)
        {
            float z = monster.GetComponent<Collider>().bounds.size.z;

            float offset = -(z + 0.3f);

            MoveToGameObject(monster, offset);
        }
        
        public void MoveToPlace()
        {
            MoveToPosition(Vector3.zero);
        }
    }
}