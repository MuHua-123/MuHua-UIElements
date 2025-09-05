using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移轴 - 相机
/// </summary>
public class CameraAxis : CameraController {

	public Camera mainCamera;
	public Camera Screenshot;

	[HideInInspector]
	public RenderTexture renderTexture;

	private float originalVisualField;
	private Vector3 originalPosition;
	private Vector3 originalEulerAngles;

	public override Vector3 Position {
		get => transform.position;
		set => transform.position = value;
	}
	public override Vector3 Forward {
		get => mainCamera.transform.forward;
		set => mainCamera.transform.forward = value;
	}
	public override Vector3 Right {
		get => mainCamera.transform.right;
		set => mainCamera.transform.right = value;
	}
	public override Vector3 EulerAngles {
		get => transform.eulerAngles;
		set => transform.eulerAngles = value;
	}
	public override float VisualField {
		get => mainCamera.transform.localPosition.z;
		set => mainCamera.transform.localPosition = new Vector3(0, 0, value);
	}

	public override void Initial() {
		base.Initial();
		originalPosition = Position;
		originalEulerAngles = EulerAngles;
		originalVisualField = VisualField;

		renderTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
		Screenshot.targetTexture = renderTexture;
	}
	public override void ModuleCamera_OnCameraMode(EnumCameraMode mode) {
		gameObject.SetActive(mode == EnumCameraMode.Axis);
		if (mode == EnumCameraMode.Axis) { ModuleCamera.CurrentCamera = this; }
	}

	public override void ResetCamera() {
		// if (!Utilities.FindObject(out MonoSceneSettings settings)) { return; }
		// Position = settings.initialPosition.position;
		// EulerAngles = settings.initialPosition.eulerAngles;
		// VisualField = settings.visualField.localPosition.z;
	}
}