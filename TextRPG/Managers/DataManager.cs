using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Policy;


public class DataManager
{
	public static DataManager Instance;

	public InventoryData inventory = new InventoryData();
	public PlayerData playerData;

	List<PlayerClass> playerClasses = new List<PlayerClass>()
	{
		new PlayerClass(EClassType.WARRIOR, 10, 12, 100),
		new PlayerClass(EClassType.ARCHER, 12, 10, 80),
		new PlayerClass(EClassType.ROGUE, 15, 8, 70),
		new PlayerClass(EClassType.MAGE, 20, 5, 60),
		new PlayerClass(EClassType.PALADIN, 7, 15, 150),
	};
	ItemManager itemManager = new ItemManager();

	public Dictionary<string, Item> items => itemManager.items;
	public Dictionary<EClassType, string> classNames = new Dictionary<EClassType, string>() { 
		{ EClassType.NONE, "-" },
		{ EClassType.WARRIOR, "전사" },
		{ EClassType.ROGUE, "도적" },
		{ EClassType.MAGE, "법사" },
		{ EClassType.ARCHER, "궁수" },
		{ EClassType.PALADIN, "성기사" },
	};

	public DataManager()
	{
		if (Instance == null)
			Instance = this;
	}

	
	public void CreateCharacter(string name, int classId)
	{
		int idx = classId - 1;
		playerData = new PlayerData(name, 1, playerClasses[idx].classType, playerClasses[idx].attack,
											playerClasses[idx].armor, playerClasses[idx].health, 100000);
	}
	public int PrintClassInfos()
	{
		// 클래스(게임직업) 정보를 띄우는 함수입니다.
		for (int i = 0; i < playerClasses.Count; i++)
		{
			PlayerClass pj = playerClasses[i];
			Console.WriteLine($"{i + 1}. {DataManager.Instance.classNames[pj.classType]} \t| 공격력 : {pj.attack} \t| 방어력 : {pj.armor} \t| 체력 : {pj.health}");
		}

		return playerClasses.Count;
	}
	
	public string GetSavePath(string name) => $"textRPG_{name}.json";

	// 해당 이름으로 저장된 파일이 있는지 체크
	public bool DoesIdExists(string name) => File.Exists(GetSavePath(name));
	public void SaveData()
	{
		// 게임 데이터를 저장, 아이템의 경우 아이템 이름을 저장합니다.
		playerData.myItems = new List<string>();
		foreach (var item in inventory.ownedItems)
			playerData.myItems.Add(item);

		playerData.weapon = inventory.weapon != null ? inventory.weapon.name : "";
		playerData.armor = inventory.armor != null ? inventory.armor.name : "";

		string json = JsonConvert.SerializeObject(playerData);
		File.WriteAllText(GetSavePath(playerData.name), json);
	}

	public bool LoadData(string name)
	{
		string path = GetSavePath(name);
		if (File.Exists(path))
		{
			string jsonData = File.ReadAllText(path);
			playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

			foreach (var item in playerData.myItems)
				inventory.ownedItems.Add(item);

			// 저장된 아이템 이름을 Dictionary를 통해 불러옵니다
			if (items.ContainsKey(playerData.weapon))
				inventory.weapon = items[playerData.weapon];

			if (items.ContainsKey(playerData.armor)) 
				inventory.armor = items[playerData.armor];

			return true;
		}
		return false;
	}

}


