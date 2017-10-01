using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 玉子烧 : Food {
		public override string Name => "玉子烧";

		public override FoodType Type => FoodType.副菜;

		public override int Rare => 5;

		public override int SkillCost => 5;

		//public override ComboEffect ComboEffect { get; } = new ComboEffect(FoodType.汤饮, "料理技的次数+1。重装料理技时，CD-15%。");

		public 玉子烧() {
			ComboEffect = new ComboEffect(this, FoodType.汤饮, (ComboEffectType.次数, 1));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, -15));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count >= 3;
		}
	}
}
