using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerStatusScene : Scene
{
	public PlayerStatusScene(string name) : base(name)
	{

	} 


	public override void StartScene()
	{ 
		Console.Clear();
		Console.WriteLine("캐릭터의 정보입니다."); 
		Console.WriteLine();

		PlayerData pd = DataManager.Instance.playerData;
		Console.WriteLine($"Lv. {pd.level}");

		Item weapon = DataManager.Instance.inventory.weapon;
		Item equipMent = DataManager.Instance.inventory.equipment;

		int addAttack = weapon != null ? weapon.value : 0;
		int addArmor = equipMent != null ? equipMent.value : 0;

		string jobName = DataManager.Instance.jobNames[pd.job]; 
		Console.WriteLine($"{pd.name} ( {jobName} )");
		Console.WriteLine($"공격력 : {pd.GetAttack()} (+{addAttack})"); 
		Console.WriteLine($"방어력 : {pd.GetArmor()} (+{addArmor})");
		Console.WriteLine($"최대 체력 : {pd.maxHp}");
		Console.WriteLine($"현재 체력 : {pd.hp}");
		Console.WriteLine($"Gold : {pd.gold}G");
		Console.WriteLine();
		Console.WriteLine("0. 나가기");

		int value = SpartaRPG.SelectOption(0, 0);
		if (value == 0)
			return;

	}
}

