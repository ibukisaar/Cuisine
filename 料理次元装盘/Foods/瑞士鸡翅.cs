using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 瑞士鸡翅 : Food {
		public override string Name => "瑞士鸡翅";

		public override FoodType Type => FoodType.副菜;

		public override int Rare => 3;

		public override int SkillCost => 2;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.汤饮, "料理技的费用-2，次数-1。");

		public 瑞士鸡翅() {
			ComboEffect = new ComboEffect(this, FoodType.汤饮, (ComboEffectType.费用, -2), (ComboEffectType.次数, -1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count >= 4;
		}
	}
}
