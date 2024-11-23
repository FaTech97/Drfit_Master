using _GAME.scripts.Architecture.Architecture.Services.SoundService.types;
using _GAME.scripts.Architecture.Architecture.Services.StaticData;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Sound", menuName = "Create static data/Sound")]
public class SoundConfig : IConfig<SoundID>
{
	public AudioSource audioSource;
}
