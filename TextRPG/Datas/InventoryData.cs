using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class InventoryData
{
	public HashSet<string> ownedItems = new HashSet<string>();

	Item _weaponItem;
	Item _armorItem;

	public Item weapon
	{
		get => _weaponItem;
		set
		{
			if (_weaponItem == value)
				_weaponItem = null;
			else 
				_weaponItem = value;
		}
	}
	public Item armor 
	{
		get => _armorItem;
		set
		{
			if (_armorItem == value)
				_armorItem = null;
			else _armorItem = value;
		}
	}

	public bool EquipItem(Item item)
	{
		if (item is Weapon)
		{
			// 프로퍼티를 통해 같은 Item이라면 null이 들어가도록 설계
			weapon = item;
			return weapon != item;
		}
		else
		{
			armor = item; 
			return armor != item;
		}
	}

	public void RemoveItem(Item item)
	{
		// 소지한 아이템 목록에서 삭제 후
		// 장착한 아이템이라면 장착 해제
		ownedItems.Remove(item.name);
		if (weapon == item)
			weapon = null;

		else if (_armorItem == item)
			_armorItem = null;
	}
	public bool IsEquippedItem(Item item)
	{
		return weapon == item || _armorItem == item;
	}

	public List<Item> GetPlayerItem()
	{
		// HashSet으로 보관중인 아이템 정보를 List로 담아서 반환
		List<Item> ret = new List<Item>();
		foreach (var item in ownedItems)
			ret.Add(DataManager.Instance.items[item]);
		return ret;
	}

	public bool isOwnedItem(Item item)
	{
		return ownedItems.Contains(item.name);
	}
}


