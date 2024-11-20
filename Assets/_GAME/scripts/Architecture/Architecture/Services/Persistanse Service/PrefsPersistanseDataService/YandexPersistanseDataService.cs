using System;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using Assets.SimpleLocalization.Scripts;
using YG;

public class YandexPersistentDataService : IPersistanseDataService
{
	protected override void Initialization()
	{
		YandexGame.GetDataEvent += GetLoad;
	}

	private void GetLoad()
	{
		SetData(YandexGame.savesData.data);
		if (YandexGame.savesData.data.Settings.Language.ToString() != null)
		{
			LocalizationManager.Language = YandexGame.savesData.data.Settings.Language.ToString();
		}
		else if (YandexGame.savesData.language != null)
		{
			LocalizationManager.Language = YandexGame.savesData.language == "ru"
				? Langs.Russian.ToString()
				: Langs.English.ToString();
		}
		else
		{
			LocalizationManager.Language = Langs.English.ToString();
		}

		InvokeDate();
	}

	public override GameData LoadProgress()
	{
		if (YandexGame.Instance)
		{
			YandexGame.LoadProgress();
		}

		return Data;
	}

	public override void SaveProgress(GameData gameData)
	{
		SetData(gameData);
		YandexGame.SaveProgress();
	}

	public override void SetData(GameData newValue)
	{
		YandexGame.savesData.data = newValue;
	}

	public override GameData GetData()
	{
		return YandexGame.savesData.data;
	}
}
