using System;
using UnityEngine;

namespace _GAME.scripts.Enemy
{
	public class ArtGun : MonoBehaviour, IWeapon
	{
		public GameObject bullet;
		public Transform _bulletSpawn;
		float g = Physics.gravity.y;
		private float AngleInDegrees = 60;
		private float _timer = 2f;
		private bool isAttack = false;
		private Transform playerPosition;

		private void Update()
		{
			_timer += Time.deltaTime;
			if (_timer > 1f)
			{
				Shot();
			}
		}

		public void StopAttack()
		{
			isAttack = false;
		}

		public void Shot()
		{
			// _bulletSpawn.localEulerAngles = new Vector3(-AngleInDegrees, 0, 0);

			_timer = 0;
			Vector3 fromToPlayer = playerPosition.position - _bulletSpawn.transform.position;
			Vector3 fromToPlayerXZ = new Vector3(fromToPlayer.x, 0f, fromToPlayer.z);

			transform.rotation = Quaternion.LookRotation(fromToPlayerXZ, Vector3.up);
			float yAngle = Vector3.Angle(_bulletSpawn.transform.forward, Vector3.right);
			Instantiate(bullet, _bulletSpawn.position, _bulletSpawn.rotation)
					.GetComponent<Rigidbody>().velocity = _bulletSpawn.forward * CalculateVelocity();
		}

		private float CalculateVelocity()
		{
			Vector3 fromToPlayer = playerPosition.transform.position - _bulletSpawn.transform.position;
			Vector3 fromToPlayerXZ = new Vector3(fromToPlayer.x, 0f, fromToPlayer.z);

			float x = fromToPlayerXZ.magnitude;
			float y = fromToPlayer.y;

			float angleInRadians = AngleInDegrees * Mathf.PI / 180;

			float v2 = (g * x * x) /
					   (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
			return Mathf.Sqrt(Mathf.Abs(v2));
		}

		public void Attack(Transform playerTransform)
		{
			isAttack = true;
			playerPosition = playerTransform;
		}
	}
}
