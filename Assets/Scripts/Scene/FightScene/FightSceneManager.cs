
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
            GameObject monster = GameObject.Find("skeleton-interacted");
            Debug.Log(monster.name);
            monster.name = "Skeleton";

            monster.transform.position = new Vector3(0,0,1);
        }
    }
}