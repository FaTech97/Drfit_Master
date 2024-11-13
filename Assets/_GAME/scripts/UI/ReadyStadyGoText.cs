using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ReadyStadyGoText : MonoBehaviour
{
	[SerializeField] private Text[] texts;
	[SerializeField] private float timeBecomeText = 0.5f;
	[SerializeField] private DriftCarMove driftCarMove;

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
			texts[i].gameObject.SetActive(true);
			DOTween.Sequence().Append(texts[i].transform.DOScale(texts[i].transform.localScale * 1.5f, timeBecomeText / 2f)).Append(texts[i].transform.DOScale(texts[i].transform.localScale / 1.5f, timeBecomeText / 2f));
			yield return new WaitForSecondsRealtime(timeBecomeText);
			texts[i].gameObject.SetActive(false);
		}
		driftCarMove.StartCar();
	}

}
