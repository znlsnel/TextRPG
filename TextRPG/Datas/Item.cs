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
	public EJobType allowedJob = EJobType.NONE;
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
	int GetTextWidth(string text)
	{
		return text.Sum(c => c >= 0xAC00 && c <= 0xD7A3 ? 2 : 1); // 한글(가~힣)은 2칸, 나머지는 1칸
	}
	string PadRightWithFullWidth(string text, int totalWidth)
	{
		int currentWidth = GetTextWidth(text);
		int padding = totalWidth - currentWidth;
		return text + new string(' ', Math.Max(0, padding));
	}

	public string GetItemInfo(bool isEquip = false)
	{
		string jobName = allowedJob != EJobType.NONE ? $"[{DataManager.Instance.jobNames[allowedJob]}] " : "";
		string itemName = name;
		string descriptionText = description;
		string statType = ItemType == EItemType.WEAPON ? "공격력" : "방어력";

		// 한글과 영어가 섞여도 맞춰진 칸 수로 정렬
		int nameWidth = 20;
		int descriptionWidth = 50;

		string formattedName = PadRightWithFullWidth(jobName + itemName, nameWidth);
		string formattedDescription = PadRightWithFullWidth(descriptionText, descriptionWidth);

		return $"{formattedName} \t| {statType} + {value} \t| {formattedDescription}\t";
	}
}

public class Weapon : Item
{
	public Weapon(string n, int v, string d, int p, EJobType job = EJobType.NONE) : base(n, v, d, p)
	{
		ItemType = EItemType.WEAPON;
		allowedJob = job;
	}

}

public class Equipment : Item
{
	public Equipment(string n, int v, string d, int p, EJobType job = EJobType.NONE) : base(n, v, d, p)
	{
		ItemType = EItemType.EQUIPMENT;
		allowedJob = job;
	}

}
