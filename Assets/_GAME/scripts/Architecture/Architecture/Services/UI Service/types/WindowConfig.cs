using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using UnityEngine;


[CreateAssetMenu(fileName = "WindowConfig", menuName = "Create static data/Window")]
public class WindowConfig : IConfig<WindowId>
{
	public GameObject prefab;
}
