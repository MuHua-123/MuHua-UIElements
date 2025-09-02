using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 菜单页面
/// </summary>
public class UIMenuPage : ModuleUIPage {

	public override VisualElement Element => root.Q<VisualElement>("MenuPage");

}
