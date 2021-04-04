using System.Collections.Generic;
using UnityEngine;

namespace LocationRPG
{
    public class WorldSceneManager : RPGSceneManager
    {
        public override void monsterInterract(GameObject monster)
        {
            List<GameObject> objectsToMove = new List<GameObject>();
            objectsToMove.Add(monster);
            
            SceneSwitchManager.Instance.SwitchScene(SceneNameConstants.COMBAT_SCENE, objectsToMove);
        }
    }
}