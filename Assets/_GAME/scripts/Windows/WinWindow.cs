using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
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

	[Inject]
	private void Construct(LevelManager levelManager,WindowService windowService)
	{
		_windowService = windowService;
		_levelManager = levelManager;
		winText.text = LocalizationManager.Localize("Windows.Win.LevelWasDone", _levelManager.GetLevelIndex() + 1);
		nextLevelButton.interactable = levelManager.isNextLevelAvailable();
		nextLevelButton.onClick.AddListener(OnNextLevelClickHandler);
	}

	private void OnNextLevelClickHandler()
	{
		_levelManager.GoToNextLevel();
		_windowService.Close(WindowId.Win);
	}

	protected override void Cleanup()
	{
		nextLevelButton.onClick.RemoveListener(OnNextLevelClickHandler);
		base.Cleanup();
	}
}
