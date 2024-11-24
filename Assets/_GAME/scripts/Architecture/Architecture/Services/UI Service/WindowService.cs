using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

public class WindowService: IInitializable
{
	private UIFactory _uiFactory;
	private readonly Dictionary<WindowId, WindowBase> _activeWindows = new Dictionary<WindowId, WindowBase>();

	[Inject]
	private void Construct(UIFactory uiFactory)
	{
		_uiFactory = uiFactory;
	}

	public void Initialize()
	{
		GameObject go = new GameObject("[UI Root]");
		Object.DontDestroyOnLoad(go);
		Canvas canvas = go.AddComponent<Canvas>();
		canvas.sortingOrder = 10;
		go.AddComponent<GraphicRaycaster>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		rectTransform.localPosition = Vector3.zero;
		_uiFactory.SetRoot(go.transform);
	}

	public void Open(WindowId id)
	{
		if (!IsWindowOpen(id))
		{
			CreateUI(id);
		}
	}

	public T Open<T>(WindowId id) where T : WindowBase => CreateUI(id) as T;

	private WindowBase CreateUI(WindowId id)
	{
		WindowBase activeUI = _uiFactory.CreateWindow(id);
		if (activeUI == null)
			throw new Exception($"[QWINO ERRROR] Could found UI with {id}. It doesn't found");
		SetWindow(id, activeUI);
		return activeUI;
	}

	private void SetWindow(WindowId id, WindowBase activeUI)
	{
		if (activeUI == null && IsWindowOpen(id))
		{
			Debug.Log("[QWINO] Close window: " + id);
			_activeWindows[id].Close();
		}
		_activeWindows[id] = activeUI;
	}

	private bool IsWindowOpen(WindowId id)
	{
		WindowBase windowBase;
		_activeWindows.TryGetValue(id, out windowBase);
		return windowBase != null;
	}

	public void Close(WindowId id) => SetWindow(id, null);
}
