using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class WorldSceneManager : RPGSceneManager
    {
        public override void monsterInterract(GameObject monster)
        {
            List<GameObject> objectsToMove = new List<GameObject>();
            objectsToMove.Add(monster);
            // objectsToMove.Add(GameManager.Instance.CurrentPlayer);
            SceneSwitchManager.Instance.SwitchScene(SceneNameConstants.FIGHT_SCREEN, objectsToMove);
        }
    }
}