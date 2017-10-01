using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 香槟 : Food {
		public override string Name => "香槟";

		public override FoodType Type => FoodType.汤饮;

		public override int Rare => 4;

		public override int SkillCost => 5;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.None, "重装料理技时，CD-25%。");

		public 香槟() {
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, -25));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count >= 2 && foods.Any(f => f.Type == FoodType.主菜);
		}
	}
}
