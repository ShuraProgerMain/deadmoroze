using System;
using UnityEngine;

namespace Player
{
	[Serializable]
	public sealed class EntityView
	{
		public Transform Transform;
		public Animator Animator;
	}
	
	public sealed class PlayerEntityView : MonoBehaviour
	{
		public EntityView EntityView;
		public Transform Transform;
		public CharacterController CharacterController;
		public Animator Animator;
	}
}