using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;
using System;
using System.Text.RegularExpressions;

public class Test : ModuleUIPage {

	private string skill;

	public override VisualElement Element => root;
	public Label label => Q<Label>();

	private void Awake() {
		string goblin = Settings("哥布林射手", Color.red);
		string skill = Settings("短弓射击(23)", Color.yellow);
		string elvira = Settings("艾薇拉(12)", Color.green);
		string damage = Settings("5穿刺伤害", Color.red);
		label.text = $"{goblin}使用{skill}对{elvira}造成了{damage}";
		label.RegisterCallback<MouseMoveEvent>(OnMouseMove);

		this.skill = Regex.Replace(goblin, "<.*?>", "");
	}

	private void OnMouseMove(MouseMoveEvent evt) {
		Vector2 localPosition = evt.localMousePosition;
		if (label.EnterString(skill, localPosition)) { Debug.Log($"鼠标位 {skill} 上"); }
	}

	private string Settings(string value, Color color) {
		value = UITool.RichTextUnderline(value);
		return UITool.RichTextColor(value, color);
	}
}
