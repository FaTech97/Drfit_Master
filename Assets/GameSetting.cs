using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Persistanse_Service;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class GameSetting : MonoBehaviour
{
    [SerializeField] private Toggle audioMuteToggle;
    [SerializeField] private LanguageWindowController langController;
    private IPersistanseDataService _persistanseDataService;

    [Inject]
    private void Construct(IPersistanseDataService persistanseDataService)
    {
        _persistanseDataService = persistanseDataService;
    }

    private void Start()
    {
        SetupIsLang();
        RestoreIsMuteState();
    }

    private void SetupIsLang()
    {
        Langs lang = _persistanseDataService.Data.Settings.Language;
        langController.ChooseLang(lang);
        audioMuteToggle.onValueChanged.AddListener(AudioToggleChanged);
        langController.OnLangChanged += LangChange;
    }

    private void RestoreIsMuteState()
    {
        bool isMute = _persistanseDataService.Data.Settings.IsAudioMute;
        audioMuteToggle.isOn = !isMute;
        AudioToggleChanged(isMute);
    }

    private void LangChange(Langs newLang) => _persistanseDataService.ChangeLanguage(newLang);

    private void AudioToggleChanged(bool isTrue)
    {
        _persistanseDataService.ChangeIsMute(!isTrue);
        AudioListener.pause = !isTrue;
    }
}