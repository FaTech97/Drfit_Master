using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
	public float power = 0.005f;
	void Update()
	{
		var cashedTransform = transform;
		var position = cashedTransform.position;
		position = new Vector3(position.x, position.y + (Mathf.Sin(Time.time) * power), position.z);
		cashedTransform.position = position;
	}
}
