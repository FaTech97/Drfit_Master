using System.Collections;
using System.Collections.Generic;
using _GAME.scripts.Architecture.Architecture.Services.LevelServices;
using UnityEngine;
using Zenject;

public class DestroyChildrensWithChanse : MonoBehaviour
{
    public float deleteChanse = 0.6f;
    
    void Awake()
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++) 
        {
            if (Random.Range(0f, 1f) < deleteChanse)
            {
                list.Add(transform.GetChild(i).gameObject);
            }
        }

        foreach (GameObject VARIABLE in list)
        {
            Destroy(VARIABLE);
        }
    }
    
}
