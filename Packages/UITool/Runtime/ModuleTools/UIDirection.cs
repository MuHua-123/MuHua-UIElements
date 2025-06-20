using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuHua {
	/// <summary>
	/// 方向
	/// </summary>
	[Obsolete("使用类内置的 UIDirection")]
	public enum UIDirection {
		FromLeftToRight = 0,
		FromRightToLeft = 1,
		FromTopToBottom = 2,
		FromBottomToTop = 3,

		HorizontalAndVertical = 4,
		Horizontal = 5,
		Vertical = 6,
	}
}