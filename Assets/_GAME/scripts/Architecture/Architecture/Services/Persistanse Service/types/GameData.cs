using System;
using System.Collections.Generic;

namespace _GAME.scripts.Architecture.Architecture.Persistanse_Service
{
    [Serializable]
    public class GameData
    {
        public SettingsData Settings = new SettingsData();
        public LevelsData Levels = new LevelsData();
        public PlayerData Player = new PlayerData();

        public class SettingsData
        {
            public bool IsAudioMute = false;
        }

        public class LevelsData
        {
            public int CurrentLevelIndex = 0;
        }

        public class PlayerData
        {
            public int Coins = 100;
            public int PlayerHP = 3;
        }
    }

}