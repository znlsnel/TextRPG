﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
	
	class Program
	{
		static void Main(string[] args)
		{
			DataManager dataManager = new DataManager();
			GameManager game = new GameManager();

			game.GameStart();
		}
	}
}
