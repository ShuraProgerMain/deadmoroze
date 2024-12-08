using System.Collections.Generic;
using Player;
using SalvationOfSouls.IO;
using UnityEngine;
using UnityEngine.Serialization;

namespace SalvationOfSouls.Initializers
{
	public class GameplayInitializer : MonoBehaviour
	{
		[FormerlySerializedAs("playerEntityView")] [SerializeField] private EntityView entityView;
		
		[SerializeField] private List<WeaponSample> sampleWeapons; 
		[SerializeField] private List<GameObject> weaponObjects;
	
		private PlayerInput _playerInput;
		private InputComponent _inputComponent;
		private PlayerEntity _playerEntity;

		private void Awake()
		{
			_playerInput = new PlayerInput();
			_playerInput.Enable();
			_inputComponent = new InputComponent(_playerInput);

			_playerEntity = new PlayerEntity(_inputComponent, entityView, sampleWeapons, weaponObjects);
		}

		private void Update()
		{
			_playerEntity.Update(Time.deltaTime);
		}
	}
}
