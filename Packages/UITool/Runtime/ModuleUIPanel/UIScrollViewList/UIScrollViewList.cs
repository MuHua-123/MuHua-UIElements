using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MuHua {
	/// <summary>
	/// 滚动列表
	/// </summary>
	public class UIScrollViewList<T, Data> : UIScrollView where T : ModuleUIItem<Data> {

		private ModuleUIItems<T, Data> Items;// UI项容器

		public UIScrollViewList(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset, Func<Data, VisualElement, T> generate) : base(element, canvas) {
			Items = new ModuleUIItems<T, Data>(Container, templateAsset, generate);
		}
		/// <summary> 释放资源 </summary>
		public override void Dispose() {
			base.Dispose();
			Items.Dispose();
		}
		/// <summary> 创建UI项 </summary>
		public void Create(List<Data> datas) => Items.Create(datas);
		/// <summary> 创建UI项 </summary>
		public void Create(Data data) => Items.Create(data);
	}
}
