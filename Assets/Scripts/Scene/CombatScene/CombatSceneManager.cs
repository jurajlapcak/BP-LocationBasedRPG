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
            //initialize player
            _playerController = playerParent.GetComponent<PlayerController>();
            _player = _playerController.Unit;
            
            //initialize enemy
            GameObject monsterGameObject = GameObject.Find("monster-interacted");
            _monsterController = monsterGameObject.GetComponent<MonsterController>();
            _monster = _monsterController.Unit;

            //set transform to zero value
            //so that game object is not offset of its parent
            monsterGameObject.transform.position = Vector3.zero;
            monsterGameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            
            //set monster a parent
            monsterGameObject.transform.SetParent(monsterParent.transform, false);
            monsterGameObject.name = "Monster";

            _isInitialized = true;
            SceneManager.sceneLoaded -= SceneInit;
        }
    }
}