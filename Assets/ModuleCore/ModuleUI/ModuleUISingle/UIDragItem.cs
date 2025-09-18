using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI拖拽物品
/// </summary>
public class UIDragItem : ModuleUIPanel, UIControl {
	/// <summary> 数量 </summary>
	private int count;
	/// <summary> 物品 </summary>
	private DataItem item;

	public Label Count => Q<Label>("Count");
	public VisualElement Image => Q<VisualElement>("Image");
	public VisualElement Preview => Q<VisualElement>("Preview");

	public UIDragItem(VisualElement element, VisualElement canvas) : base(element) {
		canvas.RegisterCallback<MouseMoveEvent>(FloatingFunc);
	}
	private void FloatingFunc(MouseMoveEvent evt) {
		Preview.transform.position = evt.mousePosition;
	}
	public void Update() {

	}
	public void Dispose() {
		// throw new System.NotImplementedException();
	}

	/// <summary> 打开菜单 </summary>
	public void Open(DataItem item, int count) {
		this.item = item;
		this.count = count;
		Count.text = count.ToString();
		Count.EnableInClassList("dragitem-count-hide", count <= 1);
		Image.style.backgroundImage = new StyleBackground(item.sprite);
		element.EnableInClassList("document-page-hide", item == null);
	}


}
