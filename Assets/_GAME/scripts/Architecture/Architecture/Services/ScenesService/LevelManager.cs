using System;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using UnityEngine;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services.ScenesService
{
	[Serializable]
	public class LevelManager
	{
		private List<Level> levels;
		private SceneLoader _sceneLoader;
		private IPersistanseDataService _persistanseDataService;
		private int _currentLevelIndex = 0;

		public LevelManager(List<Level> levels)
		{
			this.levels = levels;
		}

		[Inject]
		public void Construct(SceneLoader sceneLoader, IPersistanseDataService persistanseDataService)
		{
			_sceneLoader = sceneLoader;
			_persistanseDataService = persistanseDataService;
		}

		public int GetLevelIndex()
		{
			return _currentLevelIndex;
		}

		public void SetLevelIndex(int index)
		{
			_currentLevelIndex = index;
		}

		public void RestartCurrentLevel()
		{
			_sceneLoader.Load(levels[_currentLevelIndex].LevelName);
		}

		public bool isNextLevelAvailable()
		{
			return _currentLevelIndex < levels.Count;
		}

		public void GoToNextLevel()
		{
			_currentLevelIndex++;
			_sceneLoader.Load(levels[_currentLevelIndex].LevelName);
			_persistanseDataService.ChangeLevel(_currentLevelIndex);
		}
	}
}
