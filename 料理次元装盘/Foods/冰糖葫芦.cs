using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 冰糖葫芦 : Food {
		public override string Name => "冰糖葫芦";

		public override FoodType Type => FoodType.甜点;

		public override int Rare => 2;

		public override int SkillCost => 2;

		//public override ComboEffect ComboEffect { get; }//= new ComboEffect(this, FoodType.主食, "料理技的次数+1，费用+1。");

		public 冰糖葫芦() {
			ComboEffect = new ComboEffect(this, FoodType.主食, (ComboEffectType.次数, 1), (ComboEffectType.费用, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count >= 2;
		}
	}
}
