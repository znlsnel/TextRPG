using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


public enum EItemType
{
	WEAPON,
	EQUIPMENT,
}


public abstract class Item
{
	EItemType type;
	public string name;
	public string description;

	public int value;
	public int price;

	public Item(string n, int v, string d, int p)
	{
		name = n;
		value = v;
		description = d;
		price = p;
	}

	public EItemType ItemType
	{
		get => type;
		set => type = value;
	}

	public abstract string GetItemInfo();
}

public class Weapon : Item
{
	public Weapon(string n, int v, string d, int p) : base(n, v, d, p)
	{
		ItemType = EItemType.WEAPON;
	}

	public override string GetItemInfo()
	{
		string ret = name;

		int cnt = 8 - ret.Length;
		while (cnt-- > 0)
			ret = ret + "  ";

		string ds = description;
		cnt = 30 - ds.Length;
		while (cnt-- > 0)
			ds = ds + "  ";

		ret = ret + $"\t| 공격력 + {value} \t |  {ds} \t";

		return ret;
	}
}

public class Equipment : Item
{
	public Equipment(string n, int v, string d, int p) : base(n, v, d, p)
	{
		ItemType = EItemType.EQUIPMENT;

	}

	public override string GetItemInfo()
	{
		string ret = name;

		int cnt = 8 - ret.Length;
		while (cnt-- > 0)
			ret = ret + "  ";

		string ds = description;
		cnt = 30 - ds.Length;
		while (cnt-- > 0)
			ds = ds + "  "; 


		ret = ret + $"\t| 방어력 + {value} \t |  {ds} \t";

		return ret;
	}
}
