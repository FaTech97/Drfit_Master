using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WinWindow : WindowBase
{
	[SerializeField] private Button nextLevelButton;
	[SerializeField] private Text winText;
	private LevelManager _levelManager;


	[Inject]
	private void Construct(LevelManager levelManager)
	{
		_levelManager = levelManager;
		// TODO _levelManager.GetLevelIndex() + 1 => получать реальный номер уровня
		winText.text = LocalizationManager.Localize("Windows.Win.LevelWasDone", _levelManager.GetLevelIndex() + 1);
		nextLevelButton.interactable = levelManager.isNextLevelAvailable();
		nextLevelButton.onClick.AddListener(() =>
		{
			_levelManager.GoToNextLevel();
		});
	}

}
