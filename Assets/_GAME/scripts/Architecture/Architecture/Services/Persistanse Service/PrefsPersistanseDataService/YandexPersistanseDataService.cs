using Assets.SimpleLocalization.Scripts;
using YG;

namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service.PrefsPersistanseDataService
{
	public class YandexPersistentDataService : IPersistanseDataService
	{
		protected override void Initialization()
		{
			YandexGame.GetDataEvent += GetLoad;
		}

		private void GetLoad()
		{
			SetData(YandexGame.savesData.data);
			LocalizationManager.Language = YandexGame.savesData.data.Settings.Language.ToString();
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
}
