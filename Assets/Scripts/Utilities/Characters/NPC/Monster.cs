using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LocationRPG
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private float hp = 100;
        [SerializeField] private float attack = 1;
        [SerializeField] private float deffense = 1;
        [SerializeField] private float despawnTime = 180;

        private void Start()
        {
            // Destroy(gameObject, despawnTime);
        }


        public float Hp => hp;

        public float Attack => attack;

        public float Deffense => deffense;

        private void OnMouseDown()
        {
            WorldSceneManager worldSceneManager = FindObjectOfType<WorldSceneManager>();
            Scene activeScene = SceneManager.GetActiveScene();
            if (activeScene.name.Equals(SceneNameConstants.WORLD))
            {
                worldSceneManager.monsterInterract(gameObject);
            }
        }
    }
}