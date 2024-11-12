
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public GameData data = new GameData();
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public SavesYG()
        {
        }
    }
}
