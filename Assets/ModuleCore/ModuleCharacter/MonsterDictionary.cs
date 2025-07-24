using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物 - 字典
/// </summary>
public static class MonsterDictionary {
	/// <summary> 哥布林战士 怪物 </summary>
	public static DataCharacter Monster001() {
		DataCharacter character = new DataCharacter();
		character.name = "哥布林战士";
		character.race = RaceTool.None();
		character.basis = AttributeTool.Random();
		character.profession = ProfessionTool.None();
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		return character;
	}
	/// <summary> 哥布林射手 怪物 </summary>
	public static DataCharacter Monster002() {
		DataCharacter character = new DataCharacter();
		character.name = "哥布林射手";
		character.race = RaceTool.None();
		character.basis = AttributeTool.Random();
		character.profession = ProfessionTool.None();
		character.profession.Initial(character);
		character.equipment = new DataEquipment();
		return character;
	}
}
