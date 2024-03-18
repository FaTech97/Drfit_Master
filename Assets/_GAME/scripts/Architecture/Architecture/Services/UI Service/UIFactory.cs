using System.Collections.Generic;
using System.Linq;
using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using _GAME.scripts.Architecture.types;
using UnityEngine;
using Zenject;

public class UIFactory
{
    private Transform Root;
    private DiContainer _diContainer;
    private StaticDataService _staticDataService;

    public UIFactory(){
    }

    [Inject]
    private void Construct(DiContainer _diContainer,StaticDataService staticDataService)
    {
        this._staticDataService = staticDataService;
        this._diContainer = _diContainer;
    }

    public WindowBase CreateWindow(WindowId id)
    {
        GameObject prefab = _staticDataService.Windows.Get(id).prefab;
        WindowBase uiWindow  =  _diContainer.InstantiatePrefabForComponent<WindowBase>(prefab);
        uiWindow.transform.SetParent(Root, false);
        return uiWindow;
    }

    public void SetRoot(Transform rootTransform)
    {
        Root = rootTransform;
    }
}