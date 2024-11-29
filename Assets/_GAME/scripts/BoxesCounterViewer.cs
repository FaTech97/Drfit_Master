using UnityEngine;

public class BoxesCounterViewer : MonoBehaviour
{
	public void SetCount(int newCount, int offset)
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(newCount >= (i * offset));
		}
	}
}
