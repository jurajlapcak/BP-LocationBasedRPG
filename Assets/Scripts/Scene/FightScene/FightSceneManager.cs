
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG

{
    public class FightSceneManager: MonoBehaviour
    {
        public void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameObject monster = GameObject.Find("monster-interacted");
            GameObject monsterParent = GameObject.Find("Monster Parent");
            
            monster.transform.position =  Vector3.zero;
            monster.transform.rotation =  new Quaternion(0,0,0,0);;
            monster.transform.SetParent(monsterParent.transform, false);
            monster.name = "Monster";
        }
    }
}