using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 莎朗牛排 : Food {
		public override string Name => "莎朗牛排";

		public override FoodType Type => FoodType.主菜;

		public override int Rare => 3;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.副菜, "料理技的次数+1，费用+1。");

		public 莎朗牛排() {
			ComboEffect = new ComboEffect(this, FoodType.副菜, (ComboEffectType.次数, 1), (ComboEffectType.费用, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Any(f => f.Type == FoodType.副菜) && foods.Any(f => f.Type == FoodType.汤饮);
		}
	}
}
