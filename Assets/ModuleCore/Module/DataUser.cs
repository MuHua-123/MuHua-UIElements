using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用户
/// </summary>
[Serializable]
public class DataUser {
	public string id;
	public string username;
	public string passwordhash;
}
/// <summary>
/// 注册请求
/// </summary>
[Serializable]
public class DataRegisterRequest {
	public string username;
	public string password;
}
/// <summary>
/// 登录请求
/// </summary>
[Serializable]
public class DataLoginRequest {
	public string username;
	public string password;
}
/// <summary>
/// 登录响应，返回JWT。
/// </summary>
[Serializable]
public class DataLoginResponse {
	public string token;
}
/// <summary>
/// 修改密码请求。
/// </summary>
[Serializable]
public class DataChangePasswordRequest {
	public string username;
	public string oldpassword;
	public string newpassword;
}