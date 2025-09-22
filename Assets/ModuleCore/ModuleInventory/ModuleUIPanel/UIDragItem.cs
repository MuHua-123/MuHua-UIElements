using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// UI拖拽物品
/// </summary>
public class UIDragItem : ModuleUIPanel, UIControl {
	/// <summary> 画布 </summary>
	private VisualElement canvas;
	/// <summary> 是否启用 </summary>
	private bool isEnable;
	/// <summary> 鼠标位置 </summary>
	private Vector2 mousePosition;
	/// <summary> 偏移位置 </summary>
	private Vector2 offsetPosition;
	/// <summary> 物品容器 </summary>
	private DragContainer container;
	/// <summary> 目标容器 </summary>
	private DragContainer targetContainer;

	public Label Count => Q<Label>("Count");
	public VisualElement Image => Q<VisualElement>("Image");
	public VisualElement Preview => Q<VisualElement>("Preview");

	public UIDragItem(VisualElement element, VisualElement canvas) : base(element) {
		this.canvas = canvas;
		ModuleUI.AddControl(this);
		canvas.RegisterCallback<MouseUpEvent>(MouseUpEvent);
		canvas.RegisterCallback<MouseMoveEvent>(FloatingFunc);
	}
	private void MouseUpEvent(MouseUpEvent evt) {
		if (!isEnable) { return; }
		isEnable = false;
		container?.Cancel();
		element.EnableInClassList("document-page-hide", !isEnable);
		if (container == null || targetContainer == null) { return; }
		container.Exchange(targetContainer);
	}
	private void FloatingFunc(MouseMoveEvent evt) {
		mousePosition = evt.mousePosition;
	}
	public void Update() {
		Preview.transform.position = mousePosition + offsetPosition;
		// if (targetContainer == null) { return; }
		// Preview.transform.position = targetContainer.Anchor.worldBound.position;
	}
	public void Dispose() {
		canvas.UnregisterCallback<MouseUpEvent>(MouseUpEvent);
		canvas.UnregisterCallback<MouseMoveEvent>(FloatingFunc);
	}

	/// <summary> 设置容器 </summary>
	public void Settings(DragContainer container) {
		this.container = container;

		isEnable = container.Item != null;
		element.EnableInClassList("document-page-hide", !isEnable);
		if (isEnable) { UpdatePreview(); }
	}
	/// <summary> 进入容器 </summary>
	public void EnterContainer(DragContainer container) {
		targetContainer = container;
	}
	/// <summary> 退出容器 </summary>
	public void ExitContainer(DragContainer container) {
		if (targetContainer == container) { targetContainer = null; }
	}

	private void UpdatePreview() {
		offsetPosition = container.Anchor.worldBound.position - mousePosition;

		Count.text = container.Count.ToString();
		Count.EnableInClassList("dragitem-count-hide", container.Count <= 1);
		Image.style.backgroundImage = new StyleBackground(container.Item.sprite);
	}
}
/// <summary>
/// 项目拖拽
/// </summary>
public interface DragContainer {
	/// <summary> 数量 </summary>
	public int Count { get; }
	/// <summary> 物品 </summary>
	public DataItem Item { get; }
	/// <summary> 锚点 </summary>
	public VisualElement Anchor { get; }

	/// <summary> 取消拖拽 </summary>
	public void Cancel();
	/// <summary> 交换物品 </summary>
	public void Exchange(DragContainer container);
}