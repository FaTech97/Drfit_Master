using System;
using _GAME.scripts.Architecture.Architecture.Helpers.ReadOnlyAttributes;
using _GAME.scripts.Architecture.Architecture.Helpers.SceneChooseAttributes;
using UnityEngine;

namespace _GAME.scripts.Architecture.Architecture.Services.ScenesService
{
    [Serializable]
    [CreateAssetMenu(fileName = "Level", menuName = "Qwino configs/Create level")]
    public class Level:ScriptableObject
    {
        public string LevelID = Guid.NewGuid().ToString();
        public LevelState LevelState = LevelState.Close;
        [Scene] public string LevelName;
    }
}