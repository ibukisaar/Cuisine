using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 爆米花 : Food {
		public override string Name => "爆米花";

		public override FoodType Type => FoodType.甜点;

		public override int Rare => 2;

		public override int SkillCost => 2;

		public 爆米花() {
			ComboEffect = new ComboEffect(this, FoodType.主食, (ComboEffectType.费用, -2));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, 20));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Any(f => f.Type == FoodType.主食)
				&& foods.Any(f => f.Type == FoodType.副菜)
				&& foods.Any(f => f.Type == FoodType.头盘);
		}
	}
}
