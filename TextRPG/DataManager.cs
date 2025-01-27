using System;
using System.Collections.Generic;
 

public class DataManager
{
	public static DataManager Instance;

	PlayerData playerData;
	StoreData storeData;
	InventoryData inventoryData;

	public DataManager()
	{
		if (Instance == null)
			Instance = this;
	}

	public void CreateNewCharacter(string name, EJobType job)
	{
		playerData = new PlayerData();
		playerData.name = name;
		playerData.job = job;
		PlayerStatus s = new PlayerStatus();
	}
}

public struct PlayerData
{
	public string name;
	public int level;
	public EJobType job;
	public PlayerStatus status;
	public int gold;
}

public struct PlayerStatus
{
	public int attack;
	public int armor;
	public int hp;

	public PlayerStatus(int att, int arm, int h)
	{
		attack = att;
		armor = arm;
		hp = h;
	}
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

