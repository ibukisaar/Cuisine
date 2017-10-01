using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 海洋之星酒冻 : Food {
		public override string Name => "海洋之星酒冻";

		public override FoodType Type => FoodType.汤饮;

		public override int Rare => 4;

		public override int SkillCost => 6;

		public 海洋之星酒冻() {
			ComboEffect = new ComboEffect(this, FoodType.主菜, (ComboEffectType.次数, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count <= 2 && foods.Any(f => f.Type == FoodType.主菜);
		}
	}
}
