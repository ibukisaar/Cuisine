using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 千层面 : Food {
		public override string Name => "千层面";

		public override FoodType Type => FoodType.主食;

		public override int Rare => 3;

		public override int SkillCost => 4;

		public 千层面() {
			ComboEffect = new ComboEffect(this, FoodType.甜点, (ComboEffectType.费用, -2));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, 25));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Any(f => f.Type == FoodType.主菜) && foods.Any(f => f.Type == FoodType.甜点);
		}
	}
}
