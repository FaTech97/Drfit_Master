using System;
using System.Collections;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME.scripts.Architecture.Architecture.Services.ScenesService
{
	public class SceneLoader : MonoBehaviour
	{
		public GameObject canvas;

		public void Load(string name, Action onLoaded = null) => StartCoroutine(LoadScene(name, onLoaded));

		private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
		{
			canvas.SetActive(true);
			// if (SceneManager.GetActiveScene().name == nextScene)
			// {
			// 	Debug.Log("111111111");
			// 	onLoaded?.Invoke();
			// 	yield break;
			// }

			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

			while (!waitNextScene.isDone)
			{
				yield return null;
			}

			onLoaded?.Invoke();
			CustomEvent myEvent = new CustomEvent("load_level")
			{
				{ "level_name", nextScene }
			};
			AnalyticsService.Instance.RecordEvent(myEvent);
			canvas.SetActive(false);
		}
	}
}
