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
			InstantiateRootCanvasForWindows();
		}

		public override void InstallBindings()
		{
			BindLevelEvents();
		}

		private void BindLevelEvents()
		{
			Container.Bind<LevelEvents>().AsSingle();
		}

		private void InstantiateRootCanvasForWindows()
		{
			GameObject go = Instantiate(new GameObject("UI Root"));
			Canvas canvas = go.AddComponent<Canvas>();
			go.AddComponent<GraphicRaycaster>();
			canvas.renderMode = RenderMode.ScreenSpaceOverlay;
			RectTransform rectTransform = go.GetComponent<RectTransform>();
			rectTransform.localPosition = Vector3.zero;
			_windowService.SetRootObject(go.transform);
		}
	}
}
