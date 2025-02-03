using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ItemManager
{
	public Dictionary<string, Item> items = new Dictionary<string, Item>();

	public ItemManager()
	{
		CreateArmor("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000);
		CreateArmor("전사의 갑옷", 6, "전사들이 즐겨입는 갑옷입니다.", 1500, EClassType.WARRIOR);
		CreateArmor("도적의 옷", 6, "도적에게 알맞은 옷입니다.", 1500, EClassType.ROGUE);
		CreateArmor("법사의 로브", 6, "마법사의 로브입니다.", 1500, EClassType.MAGE);
		CreateArmor("무쇠갑옷", 7, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2200);
		CreateArmor("스파르타의 갑옷", 9, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
		CreateArmor("성기사의 갑옷", 12, "굉장히 튼튼한 성기사 전용 갑옷 입니다.", 4500, EClassType.PALADIN);
		CreateArmor("비키니", 15, "고인물들이 즐겨 입는다는 평범한 비키니", 7000);

		CreateWeapon("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600);
		CreateWeapon("청동 도끼", 5, "어디선가 사용됐던거 같은 도끼입니다.", 1500);
		CreateWeapon("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000);
		CreateWeapon("소총", 20, "누구나 한방에 보낼 수 있는 강력한 무기이다.", 8000);
		CreateWeapon("성기사의 검", 30, "강한 신성력이 깃든 성기사 전용 검입니다.", 12000, EClassType.PALADIN);
	}

	void CreateWeapon(string name, int value, string dsc, int price, EClassType type = EClassType.NONE) => 
		items.Add(name, new Weapon(name, value, dsc, price, type));

	void CreateArmor(string name, int value, string dsc, int price, EClassType type = EClassType.NONE) =>
		items.Add(name, new Armor(name, value, dsc, price, type));

}

