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
            SceneManager.LoadScene(SceneTypes.FIGHT_SCREEN);
        }
    }
}