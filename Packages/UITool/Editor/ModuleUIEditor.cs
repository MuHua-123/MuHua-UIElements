using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using MuHua;

namespace MuHuaEditor {
	/// <summary>
	/// 编辑器
	/// </summary>
	public abstract class ModuleUIEditor<T> : Editor where T : Object {
		/// <summary> UXML 资源 </summary>
		public VisualTreeAsset VisualTreeAsset = default;

		/// <summary> 选中目标 </summary>
		protected T value;
		/// <summary> 绑定文档 </summary>
		public VisualElement Element = null;
		/// <summary> 控件列表 </summary>
		public List<UIControl> controls = new List<UIControl>();

		public virtual void Awake() => value = target as T;

		/// <summary> 在OnEnable中注册 </summary>
		private void OnEnable() => EditorApplication.update += Update;

		/// <summary> 在OnDisable中注销 </summary>
		private void OnDisable() => EditorApplication.update -= Update;

		public override VisualElement CreateInspectorGUI() {
			VisualElement root = new VisualElement();
			// 1. 添加 Script 字段（只读）
			var scriptProperty = serializedObject.FindProperty("m_Script");
			if (scriptProperty != null) {
				var scriptField = new PropertyField(scriptProperty);
				scriptField.SetEnabled(false); // 只读
				root.Add(scriptField);
			}
			// 实例化 UXML
			if (VisualTreeAsset != null) {
				Element = VisualTreeAsset.Instantiate();
				root.Add(Element);
				Initial();
			}
			// 如果没设置 UXML，返回默认 Inspector
			return root ?? new IMGUIContainer(() => DrawDefaultInspector());
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
		public VE Q<VE>(string name = null, string className = null) where VE : VisualElement => Element.Q<VE>(name, className);
	}
}