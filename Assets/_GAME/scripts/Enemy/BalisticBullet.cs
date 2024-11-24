using System;
using _GAME.scripts.Player;
using UnityEngine;

namespace _GAME.scripts.Enemy
{
	public class BalisticBullet : MonoBehaviour, IBullet
	{
		[SerializeField] private GameObject _gui;
		[SerializeField] private GameObject _effect;
		

		private void OnCollisionEnter(Collision other)
		{
			MakePoof(other);
		}

		private void MakePoof(Collision collision)
		{
			if (collision.collider.TryGetComponent(out PlayerHealth playerHealth))
			{
				playerHealth.takeDamage();
			}

			_gui.SetActive(false);
			_effect.SetActive(true);
			Destroy(this.gameObject, 100);
		}
	}
}
