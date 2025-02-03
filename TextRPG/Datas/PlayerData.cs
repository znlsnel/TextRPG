using Aspose.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public class PlayerData 
{
	public string name;
	public int level;
	public EClassType classType;
	public int attack;
	public int armor;
	public int maxHp;
	public int hp;
	public int gold;

	public List<string> myItems = new List<string>();
	public string weapon = "";
	public string equipment = "";

	public PlayerData(string name, int level, EClassType type, int attack, int armor, int maxHp, int gold)
	{
		this.name = name;
		this.level = level;
		this.classType = type;
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

public enum EClassType 
{
	WARRIOR = 1 << 0,
	ROGUE = 1 << 1,
	MAGE = 1 << 2,
	ARCHER = 1 << 3,
	PALADIN = 1 << 4, 
	NONE = WARRIOR | MAGE | ROGUE | ARCHER | PALADIN,
}

public class PlayerClass
{
	public EClassType classType;
	public  int attack;
	public int armor;
	public int health;

	public PlayerClass(EClassType type, int att, int arm, int hp)
	{
		this.classType = type;
		this.attack = att;
		this.armor = arm;
		this.health = hp;
	}
}