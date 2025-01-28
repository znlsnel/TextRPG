using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Policy;


public class PlayerData
{
	public string name;
	public int level; 
	public EJobType job;
	public int attack;
	public int armor;
	public int maxHp; 
	public int hp; 
	public int gold;

	public PlayerData(string name, int level, EJobType job, int attack, int armor, int maxHp, int gold)
	{
		this.name = name;
		this.level = level;
		this.job = job;
		this.attack = attack;
		this.armor = armor;
		this.maxHp = maxHp;
		this.hp = maxHp;
		this.gold = gold;
	}
}



public class InventoryData
{
	public HashSet<string> ownedItems = new HashSet<string>();

	public Item weaponItem;
	public Item equipmentItem;

	public bool EquipItem(Item item)
	{
		if (item.type == EItemType.WEAPON)
		{
			if (weaponItem == item)
			{
				weaponItem = null;
				return false;
			}

			weaponItem = item;
			return true;
		}
		else
		{
			if (equipmentItem == item)
			{
				equipmentItem = null; 
				return false;
			}

			equipmentItem = item;
			return true;

		} 
	}

	public void RemoveItem(Item item)
	{
		ownedItems.Remove(item.name);
		if (weaponItem == item)
			weaponItem = null; 

		else if (equipmentItem == item)
			equipmentItem = null;
	}
	public bool IsEquippedItem(Item item)
	{ 
		return weaponItem == item || equipmentItem == item;
	}
}
 
public class DataManager
{
	public static DataManager Instance;

	public Dictionary<string, Item> items = new Dictionary<string, Item>();

	public PlayerData playerData; 
	public InventoryData inventoryData = new InventoryData();

	List<PlayerJob> _playerJobs = new List<PlayerJob>();

	public DataManager()
	{
		if (Instance == null)
			Instance = this;

		_playerJobs.Add(new PlayerJob(EJobType.WARRIOR, 10, 10, 30));
		_playerJobs.Add(new PlayerJob(EJobType.ROGUE, 12, 8, 20));
		_playerJobs.Add(new PlayerJob(EJobType.MAGE, 15, 5, 20));

		InitItems(); 
	}

	public void CreateNewCharacter(string name, int jobId)
	{
		int idx = jobId - 1;
		playerData = new PlayerData(name, 1, _playerJobs[idx].type, _playerJobs[idx].attack, 
											_playerJobs[idx].armor, _playerJobs[idx].health, 100000);
	}

	public void PrintJobInfos()
	{
		for (int i = 0; i < _playerJobs.Count; i++)
		{
			PlayerJob pj = _playerJobs[i];
			Console.WriteLine($"{i+1}. {pj.jobName} | 공격력 : {pj.attack} | 방어력 : {pj.armor} | 체력 : {pj.health}");
		}
	}

	void InitItems()
	{
		items.Add("수련자 갑옷", new Equipment("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000));
		items.Add("무쇠갑옷", new Equipment("무쇠갑옷", 7, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200));
		items.Add("스파르타의 갑옷", new Equipment("스파르타의 갑옷", 9, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));

		items.Add("낡은 검", new Weapon("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
		items.Add("청동 도끼", new Weapon("청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
		items.Add("스파르타의 창", new Weapon("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000));
	}


	public List<Item> GetPlayerItem()
	{
		List<Item> ret = new List<Item>();


		foreach (var item in inventoryData.ownedItems)
			ret.Add(items[item]);
		return ret; 
	}

	public bool isOwnItem(Item item)
	{
		return inventoryData.ownedItems.Contains(item.name);
	}
}



public enum EJobType
{
	WARRIOR,
	ROGUE,
	MAGE
}

public struct PlayerJob
{
	public string jobName;
	public EJobType type;
	public int attack;
	public int armor;
	public int health;

	public PlayerJob(EJobType job, int att, int arm, int hp)
	{
		this.type = job;

		if (job == EJobType.WARRIOR)
			this.jobName = "전사";
		else if (job == EJobType.ROGUE)
			this.jobName = "도적";
		else
			this.jobName = "마법사";

		this.attack = att;
		this.armor = arm;
		this.health = hp;
	}
}

