using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 白米饭 : Food {
		public override string Name => "白米饭";

		public override FoodType Type => FoodType.主食;

		public override int Rare => 1;

		public override int SkillCost => 2;

		public 白米饭() {
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_秒, -2));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count > 0;
		}
	}
}
