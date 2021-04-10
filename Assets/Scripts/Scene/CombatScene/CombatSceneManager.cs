
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG

{
    public class CombatSceneManager: MonoBehaviour
    {
        [SerializeField] private GameObject playerParent;
        [SerializeField] private GameObject monsterParent;

        private Player _player;
        private Monster _monster;

        public Player Player => _player;
        public Monster Monster => _monster;
        
        //called when object that has this component is enabled
        public void OnEnable()
        {
            //when scene loaded
            SceneManager.sceneLoaded += SceneInit;
        }

        //initializes scene
        private void SceneInit(Scene scene, LoadSceneMode mode)
        {
            //initialize enemy
            GameObject monster = GameObject.Find("monster-interacted");
            _monster = monster.GetComponent<Monster>();
            
            monster.transform.position =  Vector3.zero;
            monster.transform.rotation =  new Quaternion(0,0,0,0);;
            monster.transform.SetParent(monsterParent.transform, false);
            monster.name = "Monster";
            
            //initialize player
            PlayerController player = GameManager.Instance.CurrentPlayer;
            _player = player.Player;
            
            playerParent.GetComponent<PlayerController>().Player = player.Player;
        }
    }
}