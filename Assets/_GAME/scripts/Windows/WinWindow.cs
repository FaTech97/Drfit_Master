using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class WinWindow : WindowBase
{
	[SerializeField] private Button nextLevelButton;
	[SerializeField] private Text winText;
	private LevelManager _levelManager;
	private WindowService _windowService;
	private SoundsService _soundsService;

	[Inject]
	private void Construct(LevelManager levelManager,WindowService windowService,SoundsService soundsService)
	{
		_soundsService = soundsService;
		_windowService = windowService;
		_levelManager = levelManager;
		winText.text = LocalizationManager.Localize("Windows.Win.LevelWasDone", _levelManager.GetLevelIndex() + 1);
		nextLevelButton.interactable = levelManager.isNextLevelAvailable();
		nextLevelButton.onClick.AddListener(OnNextLevelClickHandler);
		_soundsService.Play(SoundID.Win);
	}

	private void OnNextLevelClickHandler()
	{
		_levelManager.GoToNextLevel();
		_windowService.Close(WindowId.Win);
	}

	protected override void Cleanup()
	{
		_soundsService.Stop(SoundID.Win);
		nextLevelButton.onClick.RemoveListener(OnNextLevelClickHandler);
		base.Cleanup();
	}
}
