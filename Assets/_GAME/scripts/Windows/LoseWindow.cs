using System;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using _GAME.scripts.Architecture.Architecture.Services.AdService;
using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using Assets.SimpleLocalization.Scripts;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoseWindow : WindowBase
{
	[SerializeField] private Text lostHPTextBox;
	[SerializeField] private Button ContinueForMonneyButton;
	[SerializeField] private Button ContinueForRVButton;
	[SerializeField] private Button FreeContinoeButton;
	[SerializeField] private Text repairPriceText;
	private IPersistanseDataService _persistanseDataService;
	private LevelManager _levelManager;
	private IAdService _adService;
	private WindowService _windowService;
	private SoundsService _soundsService;

	[Inject]
	private void Construct(IPersistanseDataService persistanseDataService, LevelManager levelManager, IAdService adService,WindowService windowService,SoundsService soundsService)
	{
		_soundsService = soundsService;
		_windowService = windowService;
		_levelManager = levelManager;
		_adService = adService;
		_persistanseDataService = persistanseDataService;
		_soundsService.Play(SoundID.Lose);
	}

	protected override void Initialize()
	{
		var HP = _persistanseDataService.Data.Player.PlayerHP;
		SetButtonVisible(HP <= 0);
		ContinueForMonneyButton.interactable = _persistanseDataService.Data.Player.RepairPrice <=
		                                       _persistanseDataService.Data.Player.Coins;
		if (HP <= 0)
		{
			lostHPTextBox.text = LocalizationManager.Localize("Windows.Lose.YouAreBroken");
			repairPriceText.text =  LocalizationManager.Localize("Windows.Lose.RepairPrice", _persistanseDataService.Data.Player.RepairPrice);
		}
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
		base.Cleanup();
	}

	private void SetButtonVisible(bool isHpZero)
	{
		ContinueForMonneyButton.gameObject.SetActive(isHpZero);
		ContinueForRVButton.gameObject.SetActive(isHpZero);
		FreeContinoeButton.gameObject.SetActive(!isHpZero);
	}

	private void ContinueForFree()
	{
		RestartLevel();
	}

	private void ContinueForRV()
	{
		CustomEvent selectItem = new CustomEvent("lose_video_watch_repair")
		{
			{ "money_count", _persistanseDataService.Data.Player.Coins},
			{ "active_car", _persistanseDataService.Data.Player.CurrectItemId.ToString()},
		};
		AnalyticsService.Instance.RecordEvent(selectItem);
		_adService.ShawReward(1234, AddLivesAndRestart);
	}

	private void AddLivesAndRestart()
	{
		_persistanseDataService.RefreshHP();
		RestartLevel();
	}

	private void ContinueForMonney()
	{
		_persistanseDataService.RefreshHP();
		_persistanseDataService.SpendMoney(30);
		CustomEvent selectItem = new CustomEvent("lose_pay_money_repair")
		{
			{ "money_count", _persistanseDataService.Data.Player.Coins},
			{ "active_car", _persistanseDataService.Data.Player.CurrectItemId.ToString()},
		};
		AnalyticsService.Instance.RecordEvent(selectItem);
		RestartLevel();
	}

	private void RestartLevel()
	{
		_soundsService.Stop(SoundID.Lose);
		_levelManager.RestartCurrentLevel();
		_windowService.Close(WindowId.Fail);
	}
}
