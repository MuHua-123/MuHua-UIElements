using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 角色信息
/// </summary>
public class UICharacterInfo : ModuleUIPanel {

	private UIBasicInfo basicInfo;
	private UIAttribute attribute;

	public VisualElement BasicInfo => Q<VisualElement>("BasicInfo");
	public VisualElement Attribute => Q<VisualElement>("Attribute");

	public UICharacterInfo(VisualElement element) : base(element) {
		basicInfo = new UIBasicInfo(BasicInfo);
		attribute = new UIAttribute(Attribute);
	}

	public void Settings(DataCharacter character) {
		basicInfo.Settings(character);
		attribute.Settings(character);
	}

	/// <summary>
	/// 基本信息
	/// </summary>
	public class UIBasicInfo : ModuleUIPanel {

		public Label Name => Q<Label>("Name");
		public Label Race => Q<Label>("Race");
		public Label Level => Q<Label>("Level");
		public Label Profession => Q<Label>("Profession");

		public UIBasicInfo(VisualElement element) : base(element) { }

		public void Settings(DataCharacter character) {

		}
	}
	/// <summary>
	/// 角色属性
	/// </summary>
	public class UIAttribute : ModuleUIPanel {

		public Label Str => Q<Label>("Str");
		public Label Dex => Q<Label>("Dex");
		public Label Con => Q<Label>("Con");
		public Label Int => Q<Label>("Int");
		public Label Wis => Q<Label>("Wis");
		public Label Cha => Q<Label>("Cha");

		public UIAttribute(VisualElement element) : base(element) { }

		public void Settings(DataCharacter character) {

		}
	}
}
