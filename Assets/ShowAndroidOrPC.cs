using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

public class ShowAndroidOrPC : MonoBehaviour
{
    [SerializeField] private GameObject showOnlyForAndroid;
    [SerializeField] private GameObject showOnlyForPC;
    
    void Start()
    {
        if (YandexGame.EnvironmentData.isDesktop) {
            showOnlyForPC.SetActive(true);
            showOnlyForAndroid.SetActive(false);
        }
        else
        {
            showOnlyForPC.SetActive(false);
            showOnlyForAndroid.SetActive(true);
        }
    }
}
