using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 菜单 - 输入
/// </summary>
public class InputMenu : InputControl {

	protected override void ModuleInput_OnInputMode(InputMode mode) {
		// throw new System.NotImplementedException();
	}

	#region 输入系统
	/// <summary> 鼠标右键 </summary>
	public void OnMouseRight(InputValue inputValue) {
		UIMenuManager.I.Open();
	}
	#endregion
}
