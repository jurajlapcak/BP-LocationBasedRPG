using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class SceneSwitchManager : Singleton<SceneSwitchManager>
    {
        public void SwitchScene(string sceneName, List<GameObject> objectsToMove)
        {
            StartCoroutine(LoadScene(sceneName, objectsToMove));
        }

        private IEnumerator LoadScene(string sceneName, List<GameObject> objectsToMove)
        {
            // Set the current Scene to be able to unload it later
            Scene currentScene = SceneManager.GetActiveScene();

            // Load new scene in the background additively, which means current scene won't automatically unload
            AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);

            SceneManager.sceneLoaded += (scene, mode) => { SceneManager.SetActiveScene(sceneToLoad); };

            foreach (GameObject gameObj in objectsToMove)
            {
                SceneManager.MoveGameObjectToScene(gameObj, sceneToLoad);
            }

            while (!sceneAsync.isDone)
            {
                yield return null;
            }

            SceneManager.UnloadSceneAsync(currentScene);
        }
    }
}