using UnityEngine;
using UnityEngine.UI;

public abstract class WindowBase : MonoBehaviour
{
    [SerializeField] private Button CloseButton;

    private void Awake() =>
        OnAwake();

    private void Start()
    {
        Initialize();
        SubscribeUpdates();
    }

    public void Close()
    {
        Time.timeScale = 1;
        Debug.Log("Closed");
        OnDestroy();
    } 

    private void OnDestroy() =>
        Cleanup();

    protected virtual void OnAwake()
    {
        CloseButton?.onClick.AddListener(() => { Destroy(gameObject); });
    }

    protected virtual void Initialize() {}

    protected virtual void SubscribeUpdates() {}

    protected virtual void Cleanup() {}
}