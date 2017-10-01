using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 薯条 : Food {
		public override string Name => "薯条";

		public override FoodType Type => FoodType.头盘;

		public override int Rare => 2;

		public override int SkillCost => 3;

		public 薯条() {
			ComboEffect = new ComboEffect(this, FoodType.All, (ComboEffectType.费用, 1));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, -35));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 2;
		}
	}
}
