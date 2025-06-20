using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI加载管理器
/// </summary>
public class UILoadManager : ModuleUIPage {

	public UIProgres progres;

	public override VisualElement Element => root.Q<VisualElement>("Popup");

	public VisualElement PopupDialog => Q<VisualElement>("PopupDialog");

	private void Awake() {
		progres = new UIProgres(PopupDialog);
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
