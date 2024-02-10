using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager : MonoBehaviour
{

    public GameObject map;
    public static bool mapEnabled;

    private void Start()
    {
        MapDisable();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapEnabled)
            {
                MapDisable();
                
            }
            else
            {
                MapEnable();
               
            }
        }
    }

    void MapEnable()
    {
        map.SetActive(true);
        mapEnabled = true;

    }

    void MapDisable()
    {
        map.SetActive(false);
        mapEnabled = false;

    }

}