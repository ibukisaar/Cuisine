using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 墨西哥卷饼 : Food {
		public override string Name => "墨西哥卷饼";

		public override FoodType Type => FoodType.主食;

		public override int Rare => 4;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.甜点, "料理技的费用-2。");

		public 墨西哥卷饼() {
			ComboEffect = new ComboEffect(this, FoodType.甜点, (ComboEffectType.费用, -2));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 3;
		}
	}
}
