using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

/// <summary>
/// 登录页面
/// </summary>
public class UILoginPage : ModuleUIPage {

	private UILoginPanel loginPanel;
	private UIRegisterPanel registerPanel;

	public override VisualElement Element => root.Q<VisualElement>("LoginPage");

	public VisualElement LoginPanel => Q<VisualElement>("LoginPanel");
	public VisualElement RegisterPanel => Q<VisualElement>("RegisterPanel");

	private void Awake() {
		loginPanel = new UILoginPanel(LoginPanel, this);
		registerPanel = new UIRegisterPanel(RegisterPanel, this);

		ModuleUI.OnJumpPage += ModuleUI_OnJumpPage;
	}

	private void ModuleUI_OnJumpPage(Page page) {
		Element.EnableInClassList("document-page-hide", page != Page.Login);
		if (page != Page.Login) { return; }
		OpenLoginPanel();
	}
	public void OpenLoginPanel() {
		loginPanel.Resetting(false);
		registerPanel.Resetting(true);
	}
	public void OpenRegisterPanel() {
		loginPanel.Resetting(true);
		registerPanel.Resetting(false);
	}
}
/// <summary>
/// 登录面板
/// </summary>
public class UILoginPanel : ModuleUIPanel {

	private string username;
	private string password;

	public Label Title => Q<Label>("Title");
	public UITextField InputField1 => Q<UITextField>("InputField1");
	public UITextField InputField2 => Q<UITextField>("InputField2");
	public Button Button1 => Q<Button>("Button1");
	public Button Button2 => Q<Button>("Button2");

	public UILoginPanel(VisualElement element, UILoginPage parent) : base(element) {
		InputField1.RegisterCallback<ChangeEvent<string>>(evt => { username = evt.newValue; });
		InputField2.RegisterCallback<ChangeEvent<string>>(evt => { password = evt.newValue; });
		Button1.clicked += () => parent.OpenRegisterPanel();
		Button2.clicked += () => ManagerRequest.I.Login(username, password, (s) => {
			ModuleUI.Settings(Page.None);
			Debug.Log(s);
		});
	}
	public void Resetting(bool isHide) {
		InputField1.value = username = "";
		InputField2.value = password = "";
		element.EnableInClassList("login-hide", isHide);
	}
}
/// <summary>
/// 注册面板
/// </summary>
public class UIRegisterPanel : ModuleUIPanel {

	private string username;
	private string password;

	public Label Title => Q<Label>("Title");
	public UITextField InputField1 => Q<UITextField>("InputField1");
	public UITextField InputField2 => Q<UITextField>("InputField2");
	public Button Button1 => Q<Button>("Button1");
	public Button Button2 => Q<Button>("Button2");

	public UIRegisterPanel(VisualElement element, UILoginPage parent) : base(element) {
		InputField1.RegisterCallback<ChangeEvent<string>>(evt => { username = evt.newValue; });
		InputField2.RegisterCallback<ChangeEvent<string>>(evt => { password = evt.newValue; });
		Button1.clicked += () => parent.OpenLoginPanel();
		Button2.clicked += () => { };
	}
	public void Resetting(bool isHide) {
		InputField1.value = "";
		InputField2.value = "";
		element.EnableInClassList("login-hide", isHide);
	}
}