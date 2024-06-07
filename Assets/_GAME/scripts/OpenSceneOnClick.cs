using _GAME.scripts.Architecture.Architecture.Services.ScenesService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class OpenSceneOnClick : MonoBehaviour
{
    private Button _button;
    private SceneLoader _sceneLoader;
    [Scene] public string ScemeName;

    [Inject]
    private void Construct(SceneLoader loader)
    {
        _sceneLoader = loader;
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        _sceneLoader.Load(ScemeName);
    }
}
