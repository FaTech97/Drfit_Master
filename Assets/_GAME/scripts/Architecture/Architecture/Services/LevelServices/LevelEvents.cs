using System;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services.LevelServices
{
	class LevelEvents
	{

		// For example events
		public event Action OnAllItemsGets;
		public void AllItemsWasGet()
		{
			OnAllItemsGets?.Invoke();
		}
	}
}
