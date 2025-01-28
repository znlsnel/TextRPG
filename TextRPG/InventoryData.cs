using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


	public class InventoryData
	{
		public HashSet<string> ownedItems = new HashSet<string>();

		public Item weaponItem;
		public Item equipmentItem;

		public bool EquipItem(Item item)
		{
			if (item.type == EItemType.WEAPON)
			{
				if (weaponItem == item)
				{
					weaponItem = null;
					return false;
				}

				weaponItem = item;
				return true;
			}
			else
			{
				if (equipmentItem == item)
				{
					equipmentItem = null;
					return false;
				}

				equipmentItem = item;
				return true;

			}
		}

		public void RemoveItem(Item item)
		{
			ownedItems.Remove(item.name);
			if (weaponItem == item)
				weaponItem = null;

			else if (equipmentItem == item)
				equipmentItem = null;
		}
		public bool IsEquippedItem(Item item)
		{
			return weaponItem == item || equipmentItem == item;
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

