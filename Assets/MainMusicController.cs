using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicController : MonoBehaviour
{
    private AudioSource _audioListener;
    // Start is called before the first frame update
    void Start()
    {
        _audioListener = GetComponent<AudioSource>();
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    public void Stop()
    {
        _audioListener.enabled = false;
    }

    public void Play()
    {
        _audioListener.enabled = true;
    }
}
