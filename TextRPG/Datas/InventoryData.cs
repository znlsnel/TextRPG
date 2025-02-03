﻿using System;
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
			else _weaponItem = value;
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
			bool ret = weapon != item;

			weapon = item;
			return ret;
		}
		else
		{
			bool ret = armor != item;

			armor = item; 
			return ret;

		}
	}

	public void RemoveItem(Item item)
	{
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


