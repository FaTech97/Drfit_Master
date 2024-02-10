using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowToPlayer : MonoBehaviour
{
    public Transform player;
    private Camera _camera;

    private void LateUpdate()
    {
        transform.position = player.position;
    }
}
