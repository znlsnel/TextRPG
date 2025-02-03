using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;



public abstract class Item
{
	public EClassType equipableBy = EClassType.NONE;
	public string name;
	public string description;

	public int value;
	public int price;

	public Item(string n, int v, string d, int p, EClassType equipable = EClassType.NONE)
	{
		equipableBy = equipable;
		name = n;
		value = v;
		description = d;
		price = p;
	}
	public abstract string GetStatType();

	// 아이템의 정보를 반환하는 함수
	public string GetItemInfo()
	{
		string jobName = equipableBy != EClassType.NONE ? $"[{DataManager.Instance.classNames[equipableBy]}] " : "";
		string itemName = name;
		string descriptionText = description;
		string statType = GetStatType(); 

		// 한글과 영어가 섞여도 맞춰진 칸 수로 정렬
		int nameWidth = 22;
		int descriptionWidth = 50;

		string formattedName = PadRightWithFullWidth(jobName + itemName, nameWidth);
		string formattedDescription = PadRightWithFullWidth(descriptionText, descriptionWidth);

		return $"{formattedName} \t| {statType} + {value} \t| {formattedDescription}\t";
	}

	public bool CanEquip(EClassType type)
	{
		return (equipableBy & type) != 0;
	}

	// 인자로 받은 Text의 길이를 출력 이 때, 한글은 *2로 계산
	static int GetTextWidth(string text)
	{
		return text.Sum(c => c >= 0xAC00 && c <= 0xD7A3 ? 2 : 1); // 한글(가~힣)은 2칸, 나머지는 1칸
	}

	// 일정한 간격에 맞게 text를 배치하기 위한 함수
	static string PadRightWithFullWidth(string text, int totalWidth)
	{
		int currentWidth = GetTextWidth(text);
		int padding = totalWidth - currentWidth;
		return text + new string(' ', Math.Max(0, padding));
	}
}

public class Weapon : Item
{
	public Weapon(string n, int v, string d, int p, EClassType type = EClassType.NONE) : base(n, v, d, p, type) {}
	public override string GetStatType() => "공격력";
}

public class Armor : Item
{
	public Armor(string n, int v, string d, int p, EClassType type = EClassType.NONE) : base(n, v, d, p, type) {}
	public override string GetStatType() => "방어력";
}

