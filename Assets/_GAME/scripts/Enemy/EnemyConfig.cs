using UnityEngine;

namespace _GAME.scripts.Enemy
{
	[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Create configs/Enemy")]
	public class EnemyConfig : ScriptableObject
	{
		public float aggreRadius = 20;
		public float attackRadius = 5;
		public float speed = 10;
	}
}
