using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToTg : MonoBehaviour
{
    private const string URL = "https://t.me/qwino_games";
    public void OpenTgGroup()
    {
        Application.OpenURL(URL);
    }
}
