using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 罗宋汤 : Food {
		public override string Name => "罗宋汤";

		public override FoodType Type => FoodType.汤饮;

		public override int Rare => 3;

		public override int SkillCost => 4;

		public 罗宋汤() {
			ComboEffect = new ComboEffect(this, FoodType.主菜, (ComboEffectType.次数, 1));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_秒, 0.5f));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Any(f => f.Type == FoodType.主菜);
		}
	}
}
