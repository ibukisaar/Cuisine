using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 馄饨 : Food {
		public override string Name => "馄饨";

		public override FoodType Type => FoodType.主食;

		public override int Rare => 4;

		public override int SkillCost => 3;

		public 馄饨() {
			ComboEffect = new ComboEffect(this, FoodType.甜点, (ComboEffectType.次数, -2));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_秒, 2));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 2 && foods.All(f => f.Type == FoodType.甜点);
		}
	}
}
