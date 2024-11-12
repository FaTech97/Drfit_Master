using System;
using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using UnityEngine;
using Zenject;

public class DestroyWhenTouch : MonoBehaviour
{
	[SerializeField] private ParticleSystem _particles;
	[SerializeField] private GameObject _gui;
	private bool _wasDestroyed = false;
	private IPersistanseDataService _persistanseDataService;
	public event Action<DestroyWhenTouch> OnWasDestroy;

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out DriftCarMove move) && !_wasDestroyed)
		{
			if (move.GetSpeed() > 10)
			{
				_wasDestroyed = true;
				if (_particles)
				{
					_particles.Play();
				}
				_gui.SetActive(false);
				OnWasDestroy?.Invoke(this);
				Destroy(this.gameObject, 150);
			}
		}
	}
}
