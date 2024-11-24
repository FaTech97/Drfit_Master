using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Services.SoundService;
using UnityEngine;
using Zenject;

public class MuteAudioWhenOnNoFocus : MonoBehaviour
{
    private SoundsService _soundsService;

    [Inject]
    private void Construct(SoundsService soundsService)
    {
        _soundsService = soundsService;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        SetValuesForAll(hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        SetValuesForAll(!isPaused);
    }

    private void SetValuesForAll(bool isSilence)
    {
        if (!isSilence)
        {
            _soundsService.PlayAll();
        }
        else
        {
            _soundsService.PauseAll();
        }
    }
}