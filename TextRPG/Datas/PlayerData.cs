using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

	public List<string> myItems = new List<string>();
	public string weapon = "";
	public string equipment = "";

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

	public int GetAttack()
	{
		Item item = DataManager.Instance.inventory.weapon;
		if (item != null)
			return attack + item.value;
		return attack;
	}

	public int GetArmor()
	{
		Item item = DataManager.Instance.inventory.equipment;
		if (item != null)
			return armor + item.value;

		return armor;
	}
}

public enum EJobType 
{
	WARRIOR,
	ROGUE,
	MAGE,
	NONE,
}

public struct PlayerJob
{
	public EJobType job;
	public string jobName;
	public int attack;
	public int armor;
	public int health;

	public PlayerJob(EJobType job, int att, int arm, int hp)
	{
		this.job = job;
		jobName = DataManager.Instance.jobNames[job];
		this.attack = att;
		this.armor = arm;
		this.health = hp;
	}

}