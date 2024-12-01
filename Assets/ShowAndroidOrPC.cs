using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ShowAndroidOrPC : MonoBehaviour
{
    [SerializeField] private GameObject showOnlyForAndroid;
    [SerializeField] private GameObject showOnlyForPC;
    
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
            showOnlyForPC.SetActive(false);
            showOnlyForAndroid.SetActive(true);
        }
        else
        {
            showOnlyForPC.SetActive(true);
            showOnlyForAndroid.SetActive(false);
        }
    }
}
