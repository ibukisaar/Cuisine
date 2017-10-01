using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 紫菜蛋花汤 : Food {
		public override string Name => "紫菜蛋花汤";

		public override FoodType Type => FoodType.汤饮;

		public override int Rare => 3;

		public override int SkillCost => 4;

		//public override ComboEffect ComboEffect { get; } = new ComboEffect(FoodType.None, "重装料理技时，CD-15%（最低5秒）。");

		public 紫菜蛋花汤() {
			ComboEffect = new ComboEffect(this, FoodType.None);
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_百分比, -15));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 3;
		}
	}
}
