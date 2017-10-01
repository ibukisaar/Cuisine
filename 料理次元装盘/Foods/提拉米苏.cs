using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 提拉米苏 : Food {
		public override string Name => "提拉米苏";

		public override FoodType Type => FoodType.甜点;

		public override int Rare => 4;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.主食, "料理技的次数+1。重装料理技时，CD-20%。");

		public 提拉米苏() {
			ComboEffect = new ComboEffect(this, FoodType.主食, (ComboEffectType.次数, 1));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, -20));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 2;
		}
	}
}
