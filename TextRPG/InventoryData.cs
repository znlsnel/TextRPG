using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	public class InventoryData
	{
		public HashSet<string> ownedItems = new HashSet<string>();

		Item weaponItem;
		Item equipmentItem;

		public Item weapon
		{
			get => weaponItem;
			set
			{
				if (weaponItem == value)
				weaponItem = null;
				else weaponItem = value;
			}
		}

		public Item equipment 
		{
			get => equipmentItem;
			set
			{
				if (equipmentItem == value)
					equipmentItem = null;
				else equipmentItem = value;
			}
		}

	public bool EquipItem(Item item)
		{
			if (item.ItemType == EItemType.WEAPON)
			{
				bool ret = weapon != item;

				weapon = item;
				return ret;
			}
			else
			{
				bool ret = equipment != item;

				equipment = item; 
				return ret;

			}
		}

		public void RemoveItem(Item item)
		{
			ownedItems.Remove(item.name);
			if (weapon == item)
				weapon = null;

			else if (equipmentItem == item)
				equipmentItem = null;
		}
		public bool IsEquippedItem(Item item)
		{
			return weapon == item || equipmentItem == item;
		}

		public List<Item> GetPlayerItem()
		{
			List<Item> ret = new List<Item>();
			foreach (var item in ownedItems)
				ret.Add(DataManager.Instance.items[item]);
			return ret;
		}

		public bool isOwnItem(Item item)
		{
			return ownedItems.Contains(item.name);
		}
	}

