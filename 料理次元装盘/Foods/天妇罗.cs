using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 天妇罗 : Food {
		public override string Name => "天妇罗";

		public override FoodType Type => FoodType.副菜;

		public override int Rare => 4;

		public override int SkillCost => 3;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.汤饮, "料理技的次数+1。");

		public 天妇罗() {
			ComboEffect = new ComboEffect(this, FoodType.汤饮, (ComboEffectType.次数, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 2 && foods.Any(f => f.Type == FoodType.汤饮);
		}
	}
}
