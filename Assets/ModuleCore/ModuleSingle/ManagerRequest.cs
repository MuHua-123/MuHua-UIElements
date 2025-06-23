using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 请求 - 管理器
/// </summary>
public class ManagerRequest : ModuleSingle<ManagerRequest> {

	/// <summary> 域名 </summary>
	public static string Address => "http://localhost:5086";

	protected override void Awake() => NoReplace(false);

	#region 用户
	/// <summary> 登录 </summary>
	public void Login(string username, string password, Action<string> callback) {
		string url = Address + "/api/user/login";
		DataLoginRequest login = new DataLoginRequest { username = username, password = password };
		PostForm(url, login, callback);
	}
	#endregion

	#region 请求类型
	public void PostForm<T>(string url, T data, Action<string> callback) {
		string json = JsonTool.ToJson(data);
		Debug.Log(json);
		DataRequestPost request = new DataRequestPost(url, json);
		request.OnError = (json) => { ErrorHandle(url, json); };
		request.OnCallback = callback;
		NetworkRequestAsync.Execute(request);
	}
	#endregion

	#region 结果处理
	/// <summary> 错误处理 </summary>
	private void ErrorHandle(string url, string json) {
		Debug.LogError($"{url} \n {json}");
	}
	#endregion
}
