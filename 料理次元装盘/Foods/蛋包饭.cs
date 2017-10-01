using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 蛋包饭 : Food {
		public override string Name => "蛋包饭";

		public override FoodType Type => FoodType.主食;

		public override int Rare => 3;

		public override int SkillCost => 2;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.甜点, "料理技的抽取几率×2。");

		public 蛋包饭() {
			ComboEffect = new ComboEffect(this, FoodType.甜点, (ComboEffectType.抽取几率, 100));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count <= 2;
		}
	}
}
