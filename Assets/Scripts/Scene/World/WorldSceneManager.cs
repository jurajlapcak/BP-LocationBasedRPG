using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class WorldSceneManager : Singleton<WorldSceneManager>
    {
        
        //called when object that has this component is enabled
        private void OnEnable()
        {
            //when scene loaded
            SceneManager.sceneLoaded += SceneInit;
        }

        //initializes scene
        private void SceneInit(Scene scene, LoadSceneMode mode)
        {
            PlayerController playerController = GameManager.Instance.CurrentPlayer;
            
            //initialize player
            playerController.LoadPlayer();
        }

        
        public void MonsterInteract(GameObject monster)
        {
            Player player = GameManager.Instance.CurrentPlayer.Unit;
            List<GameObject> objectsToMove = new List<GameObject>();
            objectsToMove.Add(monster);
            
            player.Save();
            
            SceneSwitchManager.Instance.SwitchScene(SceneNames.COMBAT_SCENE, objectsToMove);
        }
    }
}