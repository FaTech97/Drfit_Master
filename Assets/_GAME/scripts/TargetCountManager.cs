using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.LevelServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TargetCountManager : MonoBehaviour
{
	private List<DestroyWhenTouch> _targets;
	private LevelEvents _levelEvents;
	private WindowService _windowService;
	private IPersistanseDataService _persistanseDataService;

	[Inject]
	private void Construct(LevelEvents levelEvents, WindowService windowService, IPersistanseDataService persistanseDataService)
	{
		_windowService = windowService;
		_levelEvents = levelEvents;
		_persistanseDataService = persistanseDataService;
	}

	private void Start()
	{
		_targets = FindObjectsOfType<DestroyWhenTouch>().ToList();
		foreach (DestroyWhenTouch destroyWhenTouch in _targets)
		{
			destroyWhenTouch.OnWasDestroy += SubstractCount;
		}
	}

	private void SubstractCount(DestroyWhenTouch item)
	{
		item.OnWasDestroy -= SubstractCount;
		_targets.Remove(item);
		_persistanseDataService.AddMoney(10);
		if (_targets.Count == 0)
		{
			_levelEvents.AllItemsWasGet();
			_windowService.Open(WindowId.Win);
		}
	}

}
