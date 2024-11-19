using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WindowService
{
	private UIFactory _uiFactory;
	private Dictionary<WindowId, WindowBase> activeWindows = new Dictionary<WindowId, WindowBase>();
	private bool isFirstSetRoot = true;

	[Inject]
	private void Construct(UIFactory uiFactory)
	{
		_uiFactory = uiFactory;
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
		Debug.Log("Closing " + id);
		if (activeUI == null && IsWindowOpen(id))
		{
			Debug.Log("Closing " + id);
			activeWindows[id].Close();
		}
		activeWindows[id] = activeUI;
	}

	private bool IsWindowOpen(WindowId id)
	{
		WindowBase windowBase;
		activeWindows.TryGetValue(id, out windowBase);
		return windowBase != null;
	}

	public void Close(WindowId id) => SetWindow(id, null);

	public void SetRootObject(Transform go)
	{
		_uiFactory.SetRoot(go);
	}
}
