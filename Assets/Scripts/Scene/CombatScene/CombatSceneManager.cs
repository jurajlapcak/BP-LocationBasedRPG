using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG

{
    public class CombatSceneManager : Singleton<CombatSceneManager>
    {
        [SerializeField] private GameObject playerParent;
        [SerializeField] private GameObject monsterParent;

        private PlayerController _playerController;
        private MonsterController _monsterController;

        private Player _player;
        private Monster _monster;

        private bool _isInitialized;

        public GameObject PlayerParent => playerParent;
        public GameObject MonsterParent => monsterParent;

        public PlayerController PlayerController => _playerController;
        public MonsterController MonsterController => _monsterController;

        public Player Player => _player;
        public Monster Monster => _monster;


        public bool IsInitialized => _isInitialized;

        //called when object that has this component is enabled
        public void OnEnable()
        {
            //when scene loaded
            _isInitialized = false;
            SceneManager.sceneLoaded += SceneInit;
        }
        
        //initializes scene
        private void SceneInit(Scene scene, LoadSceneMode mode)
        {
            //initialize enemy
            GameObject monster = GameObject.Find("monster-interacted");
            _monster = monster.GetComponent<Monster>();
            _monsterController = monster.GetComponent<MonsterController>();

            //position monster to it's place
            monster.transform.position = Vector3.zero;
            monster.transform.rotation = new Quaternion(0, 0, 0, 0);
            ;
            monster.transform.SetParent(monsterParent.transform, false);
            monster.name = "Monster";

            //initialize player
            _playerController = playerParent.GetComponent<PlayerController>();
            _playerController.LoadPlayer();
            _player = _playerController.Unit;
            _isInitialized = true;
            SceneManager.sceneLoaded -= SceneInit;
        }
    }
}