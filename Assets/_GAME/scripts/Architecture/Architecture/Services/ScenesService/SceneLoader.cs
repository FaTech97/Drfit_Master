using System;
using System.Collections;
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
			if (SceneManager.GetActiveScene().name == nextScene)
			{
				onLoaded?.Invoke();
				yield break;
			}

			AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

			while (!waitNextScene.isDone)
			{
				yield return null;
			}

			onLoaded?.Invoke();
			canvas.SetActive(false);
		}
	}
}
