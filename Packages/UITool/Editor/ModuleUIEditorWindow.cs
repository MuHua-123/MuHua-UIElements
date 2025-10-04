using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 编辑器窗口
	/// </summary>
	public abstract class ModuleUIEditorWindow : EditorWindow {
		/// <summary> UXML 资源 </summary>
		public VisualTreeAsset VisualTreeAsset = default;

		/// <summary> 绑定文档 </summary>
		public VisualElement Element;
		/// <summary> 控件列表 </summary>
		public List<UIControl> controls = new List<UIControl>();

		public virtual void CreateGUI() {
			// Instantiate UXML
			Element = VisualTreeAsset.Instantiate();
			Element.style.flexGrow = 1;
			rootVisualElement.Add(Element);
			Initial();
		}

		public virtual void Update() => controls.ForEach(control => control.Update());

		public virtual void OnDestroy() => controls.ForEach(control => control.Dispose());

		/// <summary> 初始化 </summary>
		public abstract void Initial();

		/// <summary> 添加控件 </summary>
		public void AddControl(UIControl control) => controls.Add(control);
		/// <summary> 移除控件 </summary>
		public void RemoveControl(UIControl control) => controls.Remove(control);
		/// <summary> 查询UI元素 </summary>
		public T Q<T>(string name = null, string className = null) where T : VisualElement => Element.Q<T>(name, className);
	}
}