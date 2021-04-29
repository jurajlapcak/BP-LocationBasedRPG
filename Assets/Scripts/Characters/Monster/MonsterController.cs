using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class MonsterController : UnitController<Monster>
    {
        public void OnMouseUp()
        {
            if (!InteractionManager.Instance.InteractionLock)
            {
                WorldSceneManager worldSceneManager = WorldSceneManager.Instance;
                Scene activeScene = SceneManager.GetActiveScene();
                if (activeScene.name.Equals(SceneNames.WORLD_SCENE))
                {
                    worldSceneManager.MonsterInteract(gameObject);
                    gameObject.name = "monster-interacted";
                }
            }
        }
        
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