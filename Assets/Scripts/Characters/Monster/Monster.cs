using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace LocationRPG
{
    public class Monster : Unit
    {
        public void OnMouseUp()
        {
            if (!InteractionManager.Instance.InteractionLock)
            {
                WorldSceneManager worldSceneManager = FindObjectOfType<WorldSceneManager>();
                Scene activeScene = SceneManager.GetActiveScene();
                if (activeScene.name.Equals(SceneNameConstants.WORLD_SCENE))
                {
                    worldSceneManager.monsterInterract(gameObject);
                    gameObject.name = "monster-interacted";
                }
            }
        }
    }
}