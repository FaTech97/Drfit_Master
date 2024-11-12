using UnityEngine;
using UnityEngine.UI;

public abstract class WindowBase : MonoBehaviour
{
	[SerializeField] public Button CloseButton;

	private void Awake() =>
		OnAwake();

	private void Start()
	{
		Initialize();
		SubscribeUpdates();
	}

	public void Close()
	{
		OnDestroy();
	}

	private void OnDestroy() =>
		Cleanup();

	protected virtual void OnAwake()
	{
		CloseButton?.onClick.AddListener(() =>
		{
			Close();
		});
	}

	protected virtual void Initialize() { }

	protected virtual void SubscribeUpdates() { }

	protected virtual void Cleanup()
	{
		Destroy(gameObject);
	}
}
