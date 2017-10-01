using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 可丽饼 : Food {
		public override string Name => "可丽饼";

		public override FoodType Type => FoodType.甜点;

		public override int Rare => 4;

		public override int SkillCost => 6;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.主食, "料理技的次数+1。");

		public 可丽饼() {
			ComboEffect = new ComboEffect(this, FoodType.主食, (ComboEffectType.次数, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count <= 2;
		}
	}
}
