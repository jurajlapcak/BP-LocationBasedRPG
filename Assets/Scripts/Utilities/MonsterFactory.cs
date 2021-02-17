using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace LocationRPG
{
    public class MonsterFactory : Singleton<MonsterFactory>
    {
        [SerializeField] private Monster[] availableMonsters;
        [SerializeField] private Player player;
        [SerializeField] private float waitTime = 45.0f;
        [SerializeField] private int startingMonsters = 5;
        [SerializeField] private float minSpawnRange = 0.5f;
        [SerializeField] private float maxSpawnRange = 50.0f;

        private void Awake()
        {
            Assert.IsNotNull(availableMonsters);
            Assert.IsNotNull(player);
        }

        private void Start()
        {
            for (int i = 0; i < startingMonsters; i++)
            {
                InstatiateMonster();
            }

            StartCoroutine(GenerateMonster());
        }

        private IEnumerator GenerateMonster()
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                InstatiateMonster();
            }
        }

        private void InstatiateMonster()
        {
            var position = player.transform.position;
            float x = position.x + GenerateRandRange();
            float y = position.y;
            float z = position.z + GenerateRandRange();
            ;
            int index = Random.Range(0, availableMonsters.Length);

            Monster monster = Instantiate(availableMonsters[index], new Vector3(x, y, z), Quaternion.identity);
        }

        private float GenerateRandRange()
        {
            float range = Random.Range(minSpawnRange, maxSpawnRange);
            bool positive = Random.Range(0, 10) >= 5;
            return range * (positive ? 1 : -1);
        }
    }
}