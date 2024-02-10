namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service.PrefsPersistanseDataService
{
    public class YandexPersistentDataService : IPersistanseDataService
    {
        protected override void Initialization()
        {
            // YandexGame.GetDataEvent += GetLoad;
        }

        public override GameData LoadProgress()
        {
            throw new System.NotImplementedException();
        }

        public override void SaveProgress(GameData gameData)
        {
            throw new System.NotImplementedException();
        }

        public override void SetData(GameData newValue)
        {
            throw new System.NotImplementedException();
        }

        public override GameData GetData()
        {
            throw new System.NotImplementedException();
        }
    }
}