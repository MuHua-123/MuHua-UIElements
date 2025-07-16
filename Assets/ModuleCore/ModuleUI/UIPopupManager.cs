using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI弹出管理器
/// </summary>
public class UIPopupManager : ModuleUIPage {

	// public UIPopup dialog;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement PopupDialog => Q<VisualElement>("PopupDialog");

	private void Awake() {
		// dialog = new UIPopup(PopupDialog);
	}
	private void OnDestroy() {
		// config.Release();
		// configMaterial.Release();
		// equipmentSelection.Release();
		// paramrInput.Release();
	}
	private void Update() {
		// config.Update();
		// configMaterial.Update();
	}
}
