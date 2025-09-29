using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MuHuaEditor {
	/// <summary>
	/// 武器 - 自定义编辑器
	/// </summary>
	[CustomEditor(typeof(WeaponConst))]
	public class WeaponConstEditor : Editor {
		public VisualTreeAsset ButtonTemplate;
	}
}