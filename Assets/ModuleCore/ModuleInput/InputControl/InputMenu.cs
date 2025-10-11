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

	private void Start() {
		ShortcutMenu.I.Add("测试1/测试11", () => { Debug.Log("测试1/测试11"); });
		ShortcutMenu.I.Add("测试1/测试12", () => { Debug.Log("测试1/测试12"); });

		ShortcutMenu.I.Add("删除测试5", () => {
			ShortcutMenu.I.Remove("测试5");
		});
		ShortcutMenu.I.Add("删除测试51", () => {
			ShortcutMenu.I.Remove("测试5/测试51");
		});
		ShortcutMenu.I.Add("删除测试52", () => {
			ShortcutMenu.I.Remove("测试5/测试52");
		});

		ShortcutMenu.I.Add("测试5/测试51", () => { Debug.Log("测试51"); });
		ShortcutMenu.I.Add("测试5/测试52", () => { Debug.Log("测试52"); });
	}

	protected override void ModuleInput_OnInputMode(InputMode mode) {
		// throw new System.NotImplementedException();
	}

	#region 输入系统
	/// <summary> 鼠标左键 </summary>
	public void OnMouseLeft(InputValue inputValue) {
		if (inputValue.isPressed) return;
		ShortcutMenu.I.Close();
	}
	/// <summary> 鼠标右键 </summary>
	public void OnMouseRight(InputValue inputValue) {
		ShortcutMenu.I.Open();
	}
	#endregion
}
