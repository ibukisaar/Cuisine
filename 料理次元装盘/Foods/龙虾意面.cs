using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 龙虾意面 : Food {
		public override string Name => "龙虾意面";

		public override FoodType Type => FoodType.主食;

		public override int Rare => 4;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.甜点, "料理技的次数+1，费用-1。");

		public 龙虾意面() {
			ComboEffect = new ComboEffect(this, FoodType.甜点, (ComboEffectType.次数, 1), (ComboEffectType.费用, -1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Any(f => f.Type == FoodType.甜点);
		}
	}
}
