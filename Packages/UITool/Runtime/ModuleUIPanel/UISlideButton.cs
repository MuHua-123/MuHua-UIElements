using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MuHua;

namespace MuHua {
	/// <summary>
	/// 滑块按钮 - 数据
	/// </summary>
	public abstract class DataSlideButton {
		/// <summary> 绑定的元素 </summary>
		public VisualElement element;
	}
	/// <summary>
	/// 滑动按钮
	/// </summary>
	public class UISlideButton<T, Data> : ModuleUIPanel where T : ModuleUIItem<Data> where Data : DataSlideButton {
		/// <summary> 绑定的画布 </summary>
		public readonly VisualElement canvas;

		public Data data;
		public ModuleUIItems<T, Data> Items;// UI项容器

		/// <summary> 滑块 </summary>
		public VisualElement Slide => Q<VisualElement>("Slide");
		/// <summary> 容器 </summary>
		public VisualElement Container => Q<VisualElement>("Container");

		public UISlideButton(VisualElement element, VisualElement canvas, VisualTreeAsset templateAsset, Func<Data, VisualElement, T> generate) : base(element) {
			this.canvas = canvas;

			Items = new ModuleUIItems<T, Data>(Container, templateAsset, generate);

			ModuleUIItem<Data>.OnSelect += Settings;
		}
		public virtual void Dispose() {
			Items.Dispose();
		}
		public virtual void Update() {
			if (data == null || data.element == null) { return; }
			Vector3 offset = data.element.worldBound.position - Slide.worldBound.position;
			Slide.transform.position += offset * Time.deltaTime * 20;
		}

		/// <summary> 释放资源 </summary>
		public virtual void Release() => Items.Dispose();
		/// <summary> 创建UI项 </summary>
		public virtual void Create(List<Data> datas) => Items.Create(datas);
		/// <summary> 创建UI项 </summary>
		public virtual void Create(Data data) => Items.Create(data);
		/// <summary> 设置UI项 </summary>
		public virtual void Settings(Data data) => this.data = data;
	}
}