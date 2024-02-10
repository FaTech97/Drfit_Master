using System.Collections.Generic;
using System.Linq;
using _GAME.scripts.Architecture.types;
using UnityEngine;
using Zenject;

public class UIFactory
{
    private Transform Root;
    private DiContainer _diContainer;
    private Dictionary<WindowId, WindowConfig> _windowConfigs;
    
    public UIFactory()
    {
        LoadAllWindowsConfigs();
    }

    [Inject]
    private void Construct(DiContainer _diContainer)
    {
        this._diContainer = _diContainer;
    }

    public WindowBase CreateWindow(WindowId id)
    {
        GameObject prefab = GetWindowConfig(id).prefab;
        WindowBase uiWindow  =  _diContainer.InstantiatePrefabForComponent<WindowBase>(prefab);
        uiWindow.transform.SetParent(Root, false);
        return uiWindow;
    }

    public void SetRoot(Transform rootTransform)
    {
        Root = rootTransform;
    }

    private void LoadAllWindowsConfigs()
    {
        _windowConfigs = Resources
            .LoadAll<WindowConfig>("")
            .ToDictionary(x => x.id, x => x);
        Debug.Log($"[QWINO INFO] Founded {_windowConfigs.Count} UI element");
    }

    public WindowConfig GetWindowConfig(WindowId id) =>
        _windowConfigs.TryGetValue(id, out WindowConfig staticData)
            ? staticData
            : null;
}