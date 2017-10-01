using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 东坡肉 : Food {
		public override string Name => "东坡肉";

		public override FoodType Type => FoodType.主菜;

		public override int Rare => 4;

		public override int SkillTimes => 2;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.副菜, "料理技的次数+1。");

		public 东坡肉() {
			ComboEffect = new ComboEffect(this, FoodType.副菜, (ComboEffectType.次数, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 2 && foods.Any(f => f.Type == FoodType.副菜);
		}
	}
}
