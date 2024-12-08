using System.Collections.Generic;
using System.Linq;
using Global;
using SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers;
using SalvationOfSouls.DeadMoroze.Runtime.AI.Controllers.COVID;
using SalvationOfSouls.DeadMoroze.Runtime.Core.FiniteStateMachine;
using SalvationOfSouls.DeadMoroze.Runtime.Core.MovementStuff;
using SalvationOfSouls.DeadMoroze.Runtime.Initializers;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Spawner
{
    internal class EnemySpawner : MonoBehaviour
    {
        [FormerlySerializedAs("position")] [SerializeField] private Vector3[] positions;
        [SerializeField] private EnemyEntityView[] views;
        [SerializeField] private Transform[] _spawnPoints = new Transform[20];

        [SerializeField] private List<int> _priority;

        private StateMachineFactory _stateMachineFactory;
        private ObjectPool<EnemyController> _enemyControllersPool;
        private List<int> _nonOccupiedPositions = new ();
        private SortedSet<int> _sortedPositions = new ();
        
        private readonly Dictionary<int, StateMachine> _enemyStateMachines = new ();
        private readonly Dictionary<int, EnemyController> _enemyActiveControllers = new ();
        private readonly Dictionary<int, int> _occupiedPositions = new ();

        public void Init(StateMachineFactory stateMachineFactory)
        {
            _nonOccupiedPositions = Enumerable.Range(0, positions.Length).ToList();
            _sortedPositions = new SortedSet<int>(Enumerable.Range(0, positions.Length).ToList());
            
            _stateMachineFactory = stateMachineFactory;
            
            _enemyControllersPool = new ObjectPool<EnemyController>(createFunc: Spawn,
                actionOnRelease: controller =>
                {
                    int index = _occupiedPositions[controller.Id];
                    _occupiedPositions.Remove(controller.Id);
                    
                    _sortedPositions.Add(index);
                    controller.Release();
                },
                actionOnGet: controller =>
                {
                    int index = _sortedPositions.Min;
                    _sortedPositions.Remove(index);
                    _occupiedPositions.Add(controller.Id, index);
                    
                    Vector3 newPosition = GetNearestPosition(positions[index]);
                    Debug.Log($"NewPosition: {newPosition}");
                    controller.EnemyEntityView.transform.localPosition = newPosition;
                    
                    controller.Prepare(positions[index]);
                });
            
            SpawnEnemy(10);
        }
        
        private void OnEnable() 
        {
            GlobalInformation.init.UnitsSpawnChanges += SpawnEnemy;
        }

        private void Update()
        {
            foreach (StateMachine stateMachine in _enemyStateMachines.Values)
            {
                stateMachine.Update(Time.deltaTime);
            }
        }

        private void SpawnEnemy(int value)
        {
            for (var i = 0; i < value; i++)
            {
                EnemyController controller = _enemyControllersPool.Get();
                _enemyActiveControllers.Add(controller.Id, controller);
            }
        }

        private EnemyController Spawn()
        {
            int point = Random.Range(0, 20);
            int id = _enemyControllersPool.CountAll + 1;
            
            EnemyEntityView enemy = Instantiate(views[GetEnemyNumber()], 
                new Vector3(_spawnPoints[point].position.x, 1.1f, _spawnPoints[point].position.z), Quaternion.identity);
            MoveWrapper moveWrapper = new ();
            EnemyController controller = new EnemyController(id, enemy, moveWrapper);
            
            StateMachine stateMachine = _stateMachineFactory.GetStateMachine(controller);
            _enemyStateMachines.Add(controller.Id, stateMachine);

            return controller;
        }

        private Vector3 GetNearestPosition(Vector3 position)
        {
            var minDistance = float.MaxValue;
            int index = 0;

            for (var i = 0; i < _spawnPoints.Length; i++)
            {
                Transform spawnPoint = _spawnPoints[i];
                if (Vector3.Distance(position, spawnPoint.position) < minDistance)
                {
                    minDistance = Vector3.Distance(position, spawnPoint.position);
                    index = i;
                }
            }
            
            return _spawnPoints[index].position;
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
