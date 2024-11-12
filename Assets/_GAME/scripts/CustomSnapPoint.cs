using System;
using UnityEngine;

public class CustomSnapPoint : MonoBehaviour
{
	public enum ConnectionType
	{
		road,
		Bridge,
		angleBridge
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere(transform.position, 1f);
	}

	public ConnectionType Type;
}
