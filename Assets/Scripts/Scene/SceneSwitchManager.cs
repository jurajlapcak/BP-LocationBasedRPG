using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class SceneSwitchManager : Singleton<SceneSwitchManager>
    {
#if UNITY_EDITOR
        private int i;
        private void Awake()
        {
            i = 0;
        }
#endif

        public void SwitchScene(string sceneName, List<GameObject> objectsToMove)
        {
            StartCoroutine(LoadScene(sceneName, objectsToMove));
        }

        private IEnumerator LoadScene(string sceneName, List<GameObject> objectsToMove)
        {
            AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName);
            Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
            
            foreach (GameObject gameObj in objectsToMove)
            {
                SceneManager.MoveGameObjectToScene(gameObj, sceneToLoad);
            }
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                SceneManager.SetActiveScene(sceneToLoad);
            };
            
            while (!sceneAsync.isDone)
            {
#if UNITY_EDITOR
                Debug.Log((i++) + "Loading..");
#endif
                yield return null;
            }


        }
    }
}