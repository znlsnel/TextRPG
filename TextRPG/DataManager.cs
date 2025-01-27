using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Policy;

public class DataManager
{
	public static DataManager Instance;

	public PlayerData playerData;
	public StoreData storeData;
	public InventoryData inventoryData;

	List<PlayerJob> _playerJobs = new List<PlayerJob>();

	public DataManager()
	{
		if (Instance == null)
			Instance = this;

		_playerJobs.Add(new PlayerJob(EJobType.WARRIOR, 10, 10, 30));
		_playerJobs.Add(new PlayerJob(EJobType.ROGUE, 12, 8, 20));
		_playerJobs.Add(new PlayerJob(EJobType.MAGE, 15, 5, 20)); 
	}

	public void CreateNewCharacter(string name, int jobId)
	{
		playerData = new PlayerData();
		playerData.name = name;
		playerData.level = 1;

		int idx = jobId - 1;
		playerData.job = _playerJobs[idx].type; 
		playerData.health = _playerJobs[idx].health;
		playerData.attack = _playerJobs[idx].attack;
		playerData.armor = _playerJobs[idx].armor;
	}

	public void PrintJobInfos()
	{
		for (int i = 0; i < _playerJobs.Count; i++)
		{
			PlayerJob pj = _playerJobs[i];
			Console.WriteLine($"{i+1}. {pj.jobName} | 공격력 : {pj.attack} | 방어력 : {pj.armor} | 체력 : {pj.health}");
		}
	}
}

public struct PlayerData
{
	public string name;
	public int level;
	public EJobType job;
	public int attack;
	public int armor;
	public int health;
	public int gold;
}

public struct StoreData
{
	public List<int> boughtItems;
	

}

public struct InventoryData
{
	public List<int> ownedItems;
	public List<int> activeItems;
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
