using System.Collections.Generic;
using Player;
using SalvationOfSouls.DeadMoroze.Runtime.ChristmasTree;
using SalvationOfSouls.DeadMoroze.Runtime.IO;
using SalvationOfSouls.IO;
using Spawner;
using UnityEngine;
using Weapon;

namespace SalvationOfSouls.DeadMoroze.Runtime.Initializers
{
	public class GameplayInitializer : MonoBehaviour
	{
		[SerializeField] private PlayerEntityView playerEntityView;
		[SerializeField] private EnemySpawner enemySpawner;

		[SerializeField] private Transform christmasTreeTransform;
		[SerializeField] private List<WeaponSample> sampleWeapons; 
		[SerializeField] private List<GameObject> weaponObjects;
	
		private PlayerInput _playerInput;
		private PlayerEntity _playerEntity;
		
		private InputComponent _inputComponent;
		private ChristmasTreeComponent _christmasTreeComponent;
		private StateMachineFactory _stateMachineFactory;

		private void Awake()
		{
			_playerInput = new PlayerInput();
			_playerInput.Enable();
			_inputComponent = new InputComponent(_playerInput);
			_christmasTreeComponent = new ChristmasTreeComponent(christmasTreeTransform);
			_stateMachineFactory = new StateMachineFactory(_christmasTreeComponent);

			_playerEntity = new PlayerEntity(_inputComponent, playerEntityView, sampleWeapons, weaponObjects);
			
			enemySpawner.Init(_stateMachineFactory);
		}

		private void Update()
		{
			_playerEntity.Update(Time.deltaTime);
		}
	}
}
