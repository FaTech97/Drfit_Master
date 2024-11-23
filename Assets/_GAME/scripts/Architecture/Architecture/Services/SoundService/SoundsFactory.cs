using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using UnityEngine;
using Zenject;

namespace _GAME.scripts.Architecture.Architecture.Services.SoundService
{
	public class SoundsFactory
	{
		private Transform Root;
		private DiContainer _diContainer;
		private StaticDataService _staticDataService;

		[Inject]
		private void Construct(DiContainer _diContainer, StaticDataService staticDataService)
		{
			this._staticDataService = staticDataService;
			this._diContainer = _diContainer;
		}

		public AudioSource CreateSound(SoundID id)
		{
			if (Root == null)
			{
				GameObject gameObject = new GameObject("[SOUNDS]");
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
				SetRoot(gameObject.transform);
			}
			AudioSource prefab = _staticDataService.Sounds.Get(id).audioSource;
			AudioSource soundObject = _diContainer.InstantiatePrefabForComponent<AudioSource>(prefab);
			soundObject.transform.SetParent(Root, false);
			return soundObject;
		}

		private void SetRoot(Transform rootTransform)
		{
			Root = rootTransform;
		}
	}
}
