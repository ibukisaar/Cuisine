using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 焦糖布丁 : Food {
		public override string Name => "焦糖布丁";

		public override FoodType Type => FoodType.甜点;

		public override int Rare => 3;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.主食, "料理技的费用-2。重装料理技时，CD+15%。");

		public 焦糖布丁() {
			ComboEffect = new ComboEffect(this, FoodType.主食, (ComboEffectType.费用, -2));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, 15));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count <= 3 && foods.Any(f => f.Type == FoodType.主食);
		}
	}
}
