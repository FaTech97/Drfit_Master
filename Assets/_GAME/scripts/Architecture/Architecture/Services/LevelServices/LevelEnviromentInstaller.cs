using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services.LevelServices
{
	public class LevelEnviromentInstaller : MonoInstaller
	{
		private WindowService _windowService;

		[Inject]
		private void Construct(WindowService windowService)
		{
			_windowService = windowService;
		}

		public override void InstallBindings()
		{
			BindLevelEvents();
		}

		private void BindLevelEvents()
		{
			Container.Bind<LevelEvents>().AsSingle();
		}
	}
}
