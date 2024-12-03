using System.Collections;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ReadyStadyGoText : MonoBehaviour
{
	[SerializeField] private Text[] texts;
	[SerializeField] private float timeBecomeText = 0.5f;
	[SerializeField] private DriftCarMove driftCarMove;
	private SoundsService _soundsService;

	[Inject]
	private void Construct(SoundsService soundsService)
	{
		_soundsService = soundsService;
	}

	private void Start()
	{
		foreach (Text text in texts)
		{
			text.gameObject.SetActive(false);
		}
	}

	public void Show()
	{
		StartCoroutine(StartTimer());
	}

	private IEnumerator StartTimer()
	{
		for (int i = 0; i < texts.Length; i++)
		{
			_soundsService.Play(SoundID.Timer, true);
			texts[i].gameObject.SetActive(true);
			DOTween.Sequence().Append(texts[i].transform.DOScale(texts[i].transform.localScale * 1.5f, timeBecomeText / 2f)).Append(texts[i].transform.DOScale(texts[i].transform.localScale / 1.5f, timeBecomeText / 2f));
			yield return new WaitForSecondsRealtime(timeBecomeText);
			texts[i].gameObject.SetActive(false);
		}
		_soundsService.Stop(SoundID.Timer);
		driftCarMove.StartCar();
	}

}
