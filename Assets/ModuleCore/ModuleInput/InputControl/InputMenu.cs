using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MuHua;

/// <summary>
/// 菜单 - 输入
/// </summary>
public class InputMenu : InputControl {

	private UIMenuPanel menu;

	protected override void ModuleInput_OnInputMode(InputMode mode) {
		// throw new System.NotImplementedException();
	}

	#region 输入系统
	/// <summary> 鼠标左键 </summary>
	public void OnMouseLeft(InputValue inputValue) {
		if (inputValue.isPressed) return;
		UIShortcutMenu.I.Close();
	}
	/// <summary> 鼠标右键 </summary>
	public void OnMouseRight(InputValue inputValue) {
		UIShortcutMenu.I.Add("测试1/测试11", () => { Debug.Log("测试1/测试11"); });
		UIShortcutMenu.I.Add("测试1/测试12", () => { Debug.Log("测试1/测试12"); });

		UIShortcutMenu.I.Add("测试2", () => { Debug.Log("测试2"); });

		UIShortcutMenu.I.Open();
	}
	#endregion
}
