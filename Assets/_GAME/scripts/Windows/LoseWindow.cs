using System;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.AdService;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseWindow : WindowBase
{
	[SerializeField] private Text lostHPTextBox;
	[SerializeField] private Button ContinueForMonneyButton;
	[SerializeField] private Button ContinueForRVButton;
	[SerializeField] private Button FreeContinoeButton;
	private IPersistanseDataService _persistanseDataService;
	private LevelManager _levelManager;
	private IAdService _adService;

	[Inject]
	private void Construct(IPersistanseDataService persistanseDataService, LevelManager levelManager, IAdService adService)
	{
		_levelManager = levelManager;
		_adService = adService;
		_persistanseDataService = persistanseDataService;
	}

	protected override void Initialize()
	{
		var HP = _persistanseDataService.Data.Player.PlayerHP;
		SetButtonVisible(HP <= 0);
		if (HP <= 0)
			lostHPTextBox.text = LocalizationManager.Localize("Windows.Lose.YouAreBroken");
		else
			lostHPTextBox.text = LocalizationManager.Localize("Windows.Lose.YouLostLives", HP);
	}

	protected override void SubscribeUpdates()
	{
		ContinueForMonneyButton.onClick.AddListener(ContinueForMonney);
		ContinueForRVButton.onClick.AddListener(ContinueForRV);
		FreeContinoeButton.onClick.AddListener(ContinueForFree);
	}

	protected override void Cleanup()
	{
		ContinueForMonneyButton.onClick.RemoveListener(ContinueForMonney);
		ContinueForRVButton.onClick.RemoveListener(ContinueForRV);
		FreeContinoeButton.onClick.RemoveListener(ContinueForFree);
	}

	private void SetButtonVisible(bool isHpZero)
	{
		ContinueForMonneyButton.gameObject.SetActive(isHpZero);
		ContinueForRVButton.gameObject.SetActive(isHpZero);
		FreeContinoeButton.gameObject.SetActive(!isHpZero);
	}

	private void ContinueForFree()
	{
		_levelManager.RestartCurrentLevel();
	}

	private void ContinueForRV()
	{
		_adService.ShawReward(1234, AddLivesAndRestart);
	}

	private void AddLivesAndRestart()
	{
		_persistanseDataService.RefreshHP();
		_levelManager.RestartCurrentLevel();
	}

	private void ContinueForMonney()
	{
		_persistanseDataService.RefreshHP();
		_persistanseDataService.SpendMoney(30);
		_levelManager.RestartCurrentLevel();
	}
}
