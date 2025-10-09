using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 全局管理器
/// </summary>
public class SingleManager : ModuleSingle<SingleManager> {

	public Inventory inventory;
	public EquipmentColumn equipment;

	public ItemReward materialReward;
	public ItemReward equipmentReward;

	protected override void Awake() => NoReplace();

	private void Start() {
		inventory = new Inventory(40);
		equipment = new EquipmentColumn();

		materialReward = new ItemReward();
		materialReward.Settings(ManagerItem.I.materials);
		equipmentReward = new ItemReward();
		equipmentReward.Settings(ManagerItem.I.equipments);

		UIShortcutMenu shortcutMenu = UIPopupManager.I.shortcutMenu;
		shortcutMenu.Add("背包", () => { ModuleUI.Settings(Page.Backpack); });
		shortcutMenu.Add("奖励/材料", RewardMaterial);
		shortcutMenu.Add("奖励/装备", RewardEquipment);

		ValueSystem.I.Initial();
	}

	private void RewardMaterial() {
		(InventoryItem item, int count) = materialReward.Get(10);
		inventory.AddItem(item, count);
	}
	private void RewardEquipment() {
		(InventoryItem item, int count) = equipmentReward.Get(1);
		inventory.AddItem(item, count);
	}
}
