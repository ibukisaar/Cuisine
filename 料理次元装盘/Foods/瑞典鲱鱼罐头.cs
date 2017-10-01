using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 瑞典鲱鱼罐头 : Food {
		public override string Name => "瑞典鲱鱼罐头";

		public override FoodType Type => FoodType.副菜;

		public override int Rare => 4;

		public override int SkillCost => 3;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.汤饮, "料理技的费用-1。");

		public 瑞典鲱鱼罐头() {
			ComboEffect = new ComboEffect(this, FoodType.汤饮, (ComboEffectType.费用, -1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Any(f => f.Type == FoodType.主菜) && foods.Any(f => f.Type == FoodType.汤饮);
		}
	}
}
