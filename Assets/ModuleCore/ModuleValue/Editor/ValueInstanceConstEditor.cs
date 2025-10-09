using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MuHuaEditor {
	/// <summary>
	/// 数值实例 - 自定义编辑器
	/// </summary>
	[CustomEditor(typeof(ValueInstanceConst))]
	public class ValueInstanceConstEditor : ModuleUIEditor<ValueInstanceConst> {

		public UIValueInstanceConst valueInstance;

		public Button Delete => Q<Button>("Delete");

		public override void Initial() {
			valueInstance = new UIValueInstanceConst(value, Element);

			Delete.clicked += DeleteInstance;
		}

		/// <summary> 删除实例 </summary>
		private void DeleteInstance() {
			ValueContainerConst container = value.container;
			container?.instances.Remove(value);
			EditorUtility.SetDirty(container);
			Undo.DestroyObjectImmediate(value);
			AssetDatabase.SaveAssets();
		}
	}
}
