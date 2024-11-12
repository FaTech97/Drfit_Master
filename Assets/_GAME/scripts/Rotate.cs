using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public bool RotateX = true;
	public bool RotateY = false;
	public bool RotateZ = false;
	public float rotationSpeed = 120f;
	void Update()
	{
		// Вращаем объект вокруг оси Z
		transform.Rotate(RotateX ? rotationSpeed * Time.deltaTime : 0, RotateY ? rotationSpeed * Time.deltaTime : 0, RotateZ ? rotationSpeed * Time.deltaTime : 0);
	}
}
