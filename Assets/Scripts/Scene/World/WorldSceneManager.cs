using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class WorldSceneManager : Singleton<WorldSceneManager>
    {
        [SerializeField] private LocationRPG.PositionWithLocationProvider position;
        private bool changingScene;

        //called when object that has this component is enabled
        private void OnEnable()
        {
            Assert.IsNotNull(position);
            //when scene loaded
            SceneManager.sceneLoaded += SceneInit;
        }

        //initializes scene
        private void SceneInit(Scene scene, LoadSceneMode mode)
        {
            PlayerController playerController = GameManager.Instance.CurrentPlayer;
            
            //initialize player
            playerController.LoadPlayer();
            // position.LoadDistance();
        }

        
        public void MonsterInteract(GameObject monster)
        {
            if(changingScene) return;
            changingScene = true;
            Player player = GameManager.Instance.CurrentPlayer.Unit;
            List<GameObject> objectsToMove = new List<GameObject>();
            objectsToMove.Add(monster);
            
            player.Save();
            position.SaveDistance();
            
            SceneSwitchManager.Instance.SwitchScene(SceneNames.COMBAT_SCENE, objectsToMove);
        }
    }
}