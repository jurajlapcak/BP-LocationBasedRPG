using System.Collections.Generic;
using UnityEngine;

namespace LocationRPG
{
    public class WorldSceneManager : RPGSceneManager
    {
        public override void monsterInterract(GameObject monster)
        {
            GameObject player = GameObject.Find("Player Parent");
            List<GameObject> objectsToMove = new List<GameObject>();
            objectsToMove.Add(monster);
            // objectsToMove.Add(player);
            
            // Debug.Log(objectsToMove[0].GetComponent<Monster>().Hp);
            // Debug.Log(objectsToMove[1].GetComponent<Player>());
            SceneSwitchManager.Instance.SwitchScene(SceneNameConstants.FIGHT_SCREEN, objectsToMove);
        }
    }
}