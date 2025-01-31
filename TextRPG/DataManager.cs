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

	public Dictionary<EJobType, string> jobNames = new Dictionary<EJobType, string>() { 
		{ EJobType.WARRIOR, "전사" },
		{ EJobType.ROGUE, "도적" },
		{ EJobType.MAGE, "법사" },
	};
	public Dictionary<string, Item> items = new Dictionary<string, Item>();

	public PlayerData playerData;
	public InventoryData inventory = new InventoryData();

	List<PlayerJob> playerJobs = new List<PlayerJob>();
	 
	public DataManager()
	{
		if (Instance == null)
			Instance = this;

		playerJobs.Add(new PlayerJob(EJobType.WARRIOR, 10, 10, 30));
		playerJobs.Add(new PlayerJob(EJobType.ROGUE, 12, 8, 20));
		playerJobs.Add(new PlayerJob(EJobType.MAGE, 15, 5, 20));

		InitItems();
	}

	public void CreateCharacter(string name, int jobId)
	{
		int idx = jobId - 1;
		playerData = new PlayerData(name, 1, playerJobs[idx].job, playerJobs[idx].attack,
											playerJobs[idx].armor, playerJobs[idx].health, 100000);
	}

	public void PrintJobInfos()
	{
		for (int i = 0; i < playerJobs.Count; i++)
		{
			PlayerJob pj = playerJobs[i];
			Console.WriteLine($"{i + 1}. {pj.jobName} \t| 공격력 : {pj.attack} \t| 방어력 : {pj.armor} \t| 체력 : {pj.health}");
		}
	}

	void InitItems()
	{
		Action<Item> func = (Item item) => {items.Add(item.name, item);};

		func(new Equipment("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000));
		func(new Equipment("전사의 갑옷", 6, "전사들이 즐겨입는 갑옷입니다.", 1500, EJobType.WARRIOR));
		func(new Equipment("도적의 옷", 6, "도적에게 알맞은 옷입니다.", 1500, EJobType.ROGUE));
		func(new Equipment("법사의 로브", 6, "마법사의 로브입니다.", 1500, EJobType.MAGE));
		func(new Equipment("무쇠갑옷", 7, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200));
		func(new Equipment("스파르타의 갑옷", 9, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
		func(new Equipment("비키니", 15, "고인물들이 즐겨 입는다는 평범한 비키니", 7000));

		func(new Weapon("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
		func(new Weapon("청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
		func(new Weapon("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000));
		func(new Weapon("소총", 20, "누구나 한방에 보낼 수 있는 강력한 무기이다.", 8000));
	}

	public void SaveData()
	{
		playerData.myItems = new List<string>();

		foreach (var item in inventory.ownedItems)
			playerData.myItems.Add(item);
		playerData.weapon = inventory.weapon != null ? inventory.weapon.name : "";
		playerData.equipment = inventory.equipment != null ? inventory.equipment.name : "";

		string json = JsonConvert.SerializeObject(playerData);
		File.WriteAllText($"textRPG_{playerData.name}.json", json);
	}

	public bool LoadData(string name)
	{
		string path = $"textRPG_{name}.json";
		if (File.Exists(path))
		{
			string jsonData = File.ReadAllText(path);
			playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

			foreach (var item in playerData.myItems)
				inventory.ownedItems.Add(item);

			inventory.weapon = items[playerData.weapon];
			inventory.equipment = items[playerData.equipment];

			return true;
		}
		return false;
	}

}


