using System;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using UnityEngine;
using YG;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services.AdService
{
	public class YandexAdService : IAdService
	{
		private Dictionary<int, Action> _actions = new Dictionary<int, Action>();
		private SoundsService _soundService;

		[Inject]
		private void Construct(SoundsService soundService)
		{
			_soundService = soundService;
		}

		YandexAdService()
		{
			YandexGame.RewardVideoEvent += Reward;
		}

		private void Reward(int id)
		{
			_actions[id]();
			_actions.Remove(id);
		}

		public override void ShawReward(int id, Action action)
		{
			_actions[id] = action;
			YandexGame.RewVideoShow(id);
		}
	}
}
