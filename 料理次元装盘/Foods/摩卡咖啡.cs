using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 摩卡咖啡 : Food {
		public override string Name => "摩卡咖啡";

		public override FoodType Type => FoodType.汤饮;

		public override int Rare => 3;

		public override int SkillCost => 5;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.主菜, "料理技的费用-2。重装料理技时，CD+15%。");

		public 摩卡咖啡() {
			ComboEffect = new ComboEffect(this, FoodType.主菜, (ComboEffectType.费用, -2));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, 15));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 0;
		}
	}
}
