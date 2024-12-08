using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints = new Transform[20];

        [SerializeField] private List<string> _tagEnemy;
        [SerializeField] private List<int> _priority;
        [SerializeField] private AnimationCurve _valueEnemy;

        [SerializeField] private TimeController _timeController;

        private void OnEnable() 
        {
            GlobalInformation.init.UnitsSpawnChanges += SpawnEnemy;
        }

        private void Start() 
        {
            SpawnEnemy(10);
        }

        private void SpawnEnemy(int value)
        {
            Debug.Log("Spawn enemy = " + value);

            for (var i = 0; i < value; i++)
            {
                var point = Random.Range(0, 20);
                var enemyNumber = GetEnemyNumber();

                ObjectPooler.init.SpawnFromPool(_tagEnemy[enemyNumber], new Vector3(_spawnPoints[point].position.x, 1.1f, _spawnPoints[point].position.z), Quaternion.identity);
            }
        }

        private int GetEnemyNumber()
        {
            var number = 0;

            int itemWeight = _priority[0] + _priority[1] + _priority[1];

            int randomValue = Random.Range(0, itemWeight);

            for(int i = 0; i < _priority.Count; i++)
            {
                if(randomValue <= _priority[i])
                {
                    number = i;
                    return number;
                }
                randomValue -= _priority[i];
            }
            return number;
        }
    }
}
