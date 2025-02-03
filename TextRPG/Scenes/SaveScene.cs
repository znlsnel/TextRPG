using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class SaveScene : Scene
{
	public SaveScene(string name) : base(name) { }
	public override void EnterScene()
	{
		DataManager.Instance.SaveData();
		Console.Clear();
		Console.Write("게임을 저장합니다 ");

		// 0.5초 대기하는 코드
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(50);
			Console.Write(".  ");
		}

		Console.WriteLine();
		Console.WriteLine("게임 저장에 성공했습니다.");
		Console.WriteLine("\n0. 돌아가기");
		SpartaRPG.SelectOption(0, 0);
	}
}

