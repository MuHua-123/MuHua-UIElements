using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI弹出管理器
/// </summary>
public class UIPopupManager : ModuleUISingle<UIPopupManager> {

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement PopupDialog => Q<VisualElement>("PopupDialog");

	protected override void Awake() => NoReplace(false);
}
