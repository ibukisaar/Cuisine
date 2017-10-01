using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 香菜戚风蛋糕 : Food {
		public override string Name => "香菜戚风蛋糕";

		public override FoodType Type => FoodType.甜点;

		public override int Rare => 5;

		public override int SkillCost => 5;

		//public override ComboEffect ComboEffect { get; } = new ComboEffect(FoodType.主食, "料理技的次数+1，抽取几率-60%。");

		public 香菜戚风蛋糕() {
			ComboEffect = new ComboEffect(this, FoodType.主食, (ComboEffectType.次数, 1), (ComboEffectType.抽取几率, -60));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 1 && foods[0].Type == FoodType.主食;
		}
	}
}
