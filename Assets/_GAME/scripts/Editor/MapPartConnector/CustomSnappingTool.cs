﻿using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace _GAME.scripts.Editor.MapPartConnector
{
	[EditorTool("Custom Snap Move", typeof(MapPart))]
	public class CustomSnappingTool : EditorTool
	{
		public Texture2D ToolIcon;

		private Transform oldTarget;
		private CustomSnapPoint[] allPoints;
		private CustomSnapPoint[] targetPoints;

		private void OnEnable()
		{
			Debug.Log("Enabled");
		}

		public override GUIContent toolbarIcon
		{
			get
			{
				return new GUIContent
				{
					image = ToolIcon,
					text = "Custom Snap Move Tool",
					tooltip = "Custom Snap Move Tool - best tool ever"
				};
			}
		}

		public override void OnToolGUI(EditorWindow window)
		{
			Transform targetTransform = ((MapPart)target).transform;

			if (targetTransform != oldTarget)
			{
				PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(targetTransform.gameObject);

				if (prefabStage != null)
					allPoints = prefabStage.prefabContentsRoot.GetComponentsInChildren<CustomSnapPoint>();
				else
					allPoints = FindObjectsOfType<CustomSnapPoint>();

				targetPoints = targetTransform.GetComponentsInChildren<CustomSnapPoint>();

				oldTarget = targetTransform;
			}

			EditorGUI.BeginChangeCheck();
			Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

			if (EditorGUI.EndChangeCheck())
			{
				Undo.RecordObject(targetTransform, "Move with custom snap tool");

				if (((MapPart)target).IsGrounded)
					newPosition.y = 0;

				MoveWithSnapping(targetTransform, newPosition);
			}
		}

		private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
		{
			Vector3 bestPosition = newPosition;
			float closestDistance = float.PositiveInfinity;

			foreach (CustomSnapPoint point in allPoints)
			{
				if (point.transform.parent == targetTransform)
					continue;

				foreach (CustomSnapPoint ownPoint in targetPoints)
				{
					if (ownPoint.Type != point.Type)
						continue;

					Vector3 targetPos = point.transform.position - (ownPoint.transform.position - targetTransform.position);
					float distance = Vector3.Distance(targetPos, newPosition);

					if (distance < closestDistance)
					{
						closestDistance = distance;
						bestPosition = targetPos;
					}
				}
			}

			if (closestDistance < 0.5f)
			{
				targetTransform.position = bestPosition;
			}
			else
			{
				targetTransform.position = newPosition;
			}
		}
	}
}
