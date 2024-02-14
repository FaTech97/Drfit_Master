using System;
using UnityEngine;
using Zenject;

public class WindowService
{
    private UIFactory _uiFactory;
    private WindowBase activeWindow;
    private bool isFirstSetRoot = true;
    
    [Inject]
    private void Construct(UIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }
    
    public void Open(WindowId id) => CreateUI(id);

    public T Open<T>(WindowId id) where T:WindowBase => CreateUI(id) as T;
   
    private WindowBase CreateUI(WindowId id)
    {
        WindowBase activeUI = _uiFactory.CreateWindow(id);
        if (activeUI == null)
            throw new Exception($"[QWINO ERRROR] Could found UI with {id}. It doesn't found");
        activeWindow = activeUI;
        Debug.Log(activeUI.name);
        return activeUI;
    }


    public void Close(WindowId id) => activeWindow.Close();

    public void SetRootObject(Transform go)
    {
        _uiFactory.SetRoot(go);
        // TODO удалить костыль
        if (isFirstSetRoot)
        {
            isFirstSetRoot = false;
            Open(WindowId.Main);
        }
    }
}