using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroierItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.name);
        if (other.collider.TryGetComponent(out DriftCarMove driftCarMove))
        {
            driftCarMove.DestroyCar(other.GetContact(0).point);
        }
    }
}
