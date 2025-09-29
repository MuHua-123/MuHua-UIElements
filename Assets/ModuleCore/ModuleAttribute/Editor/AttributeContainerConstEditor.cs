using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

namespace MuHuaEditor {
	/// <summary>
	/// 属性容器 - 自定义编辑器
	/// </summary>
	[CustomEditor(typeof(AttributeContainerConst))]
	public class AttributeContainerConstEditor : ModuleUIEditor<AttributeContainerConst> {

		public VisualTreeAsset AttributeTemplate = default;

		private UIAttributeList attributeList;

		public VisualElement AttributeList => Q<VisualElement>("AttributeList");

		public override void Initial() {
			attributeList = new UIAttributeList(AttributeList, AttributeTemplate);
			attributeList.Initial(value.instances);
			AddControl(attributeList);
		}


		// public override void OnInspectorGUI() {
		// 	base.OnInspectorGUI();
		// 	if (GUILayout.Button("创建属性")) { GenerateAttribute(); }
		// 	if (GUILayout.Button("删除属性")) { DeleteInstance(); }
		// }

		/// <summary> 创建属性实例 </summary>
		// private void GenerateAttribute() {
		// 	AttributeInstanceConst instance = CreateInstance<AttributeInstanceConst>();
		// 	instance.container = container;
		// 	// 加入容器
		// 	container.instances.Add(instance);
		// 	AssetDatabase.AddObjectToAsset(instance, container);
		// 	EditorUtility.SetDirty(instance);
		// 	EditorUtility.SetDirty(container);
		// 	AssetDatabase.SaveAssets();
		// }
		/// <summary> 删除全部实例 </summary>
		// private void DeleteInstance() {
		// 	for (int i = container.instances.Count; i-- > 0;) {
		// 		AttributeInstanceConst instance = container.instances[i];
		// 		container.instances.Remove(instance);
		// 		Undo.DestroyObjectImmediate(instance);
		// 	}
		// 	EditorUtility.SetDirty(container);
		// 	AssetDatabase.SaveAssets();
		// }

	}
}