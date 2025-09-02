using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 相机 - 输入器
/// </summary>
// public class InputCamera : InputControl {

// 	public bool isMove;
// 	public bool isEnable;
// 	public bool isRotating = false;
// 	public Vector2 moveInput;
// 	public Vector2 moveDirection;
// 	public Vector2 screenPosition;

// 	private Vector3 eulerAngles;

// 	private CameraController CurrentCamera => ModuleCamera.CurrentCamera;

// 	protected override void ModuleInput_OnInputMode(InputMode mode) {
// 		// isMove = mode == EnumInputMode.FashionDisplay;
// 		isEnable = mode == InputMode.FashionDesign ||
// 		 			mode == InputMode.OrnamentDesign ||
// 		  			mode == InputMode.FashionDisplay;

// 		// isEnable = mode == EnumInputMode.FashionDesign || mode == EnumInputMode.OrnamentDesign;
// 	}

// 	private void Update() {
// 		if (!isEnable) { return; }
// 		// 计算移动方向
// 		moveDirection = Utilities.TransferDirection(CurrentCamera.Forward, CurrentCamera.Right, moveInput);
// 		// 移动当前对象
// 		CurrentCamera.Position += new Vector3(moveDirection.x, 0, moveDirection.y) * Time.deltaTime;
// 		if (!isRotating) { return; }
// 		// 计算屏幕坐标差
// 		Vector2 delta = ModuleInput.mousePosition - screenPosition;

// 		// 计算旋转角度
// 		float rotationX = delta.x / Screen.width * 360f;
// 		float rotationY = delta.y / Screen.height * 360f;

// 		// 旋转当前对象
// 		Vector3 newEulerAngles = eulerAngles + new Vector3(-rotationY, rotationX, 0);
// 		if (Utilities.FindObject(out MonoSceneSettings settings)) {
// 			newEulerAngles.x = Mathf.Clamp(newEulerAngles.x, settings.limitX.x, settings.limitX.y);
// 			newEulerAngles.y = Mathf.Clamp(newEulerAngles.y, settings.limitY.x, settings.limitY.y);
// 			newEulerAngles.z = Mathf.Clamp(newEulerAngles.z, settings.limitZ.x, settings.limitZ.y);
// 		}
// 		CurrentCamera.EulerAngles = newEulerAngles;
// 	}

// 	#region 输入系统
// 	public void OnMove(InputValue inputValue) {
// 		if (!isEnable || !isMove) { return; }
// 		// 获取移动输入
// 		moveInput = inputValue.Get<Vector2>();
// 	}
// 	public void OnEnableRotate(InputValue inputValue) {
// 		if (!isEnable) { return; }
// 		isRotating = inputValue.isPressed;
// 		// 保存当前旋转角度
// 		eulerAngles = CurrentCamera.EulerAngles;
// 		if (Utilities.FindObject(out MonoSceneSettings settings)) {
// 			if (eulerAngles.x > settings.limitX.y + 1) { eulerAngles.x -= 360; }
// 			if (eulerAngles.y > settings.limitY.y + 1) { eulerAngles.y -= 360; }
// 			if (eulerAngles.z > settings.limitZ.y + 1) { eulerAngles.z -= 360; }
// 		}

// 		// 开始旋转逻辑，保存初始屏幕坐标
// 		screenPosition = ModuleInput.mousePosition;
// 	}
// 	public void OnZoomView(InputValue inputValue) {
// 		Vector2 scroll = inputValue.Get<Vector2>();
// 		if (!isEnable) { return; }
// 		if (ModuleInput.IsPointerOverUIObject) { return; }

// 		// 计算缩放距离
// 		float zoomSpeed = 0.2f;
// 		float zoomDistance = CurrentCamera.VisualField - scroll.y * zoomSpeed * Time.deltaTime;
// 		if (Utilities.FindObject(out MonoSceneSettings settings)) {
// 			Vector2 limit = settings.limitVisualField;
// 			zoomDistance = Mathf.Clamp(zoomDistance, limit.x, limit.y);
// 		}
// 		CurrentCamera.VisualField = zoomDistance;
// 	}
// 	#endregion
// }
