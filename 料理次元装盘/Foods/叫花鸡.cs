using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 叫花鸡 : Food {
		public override string Name => "叫花鸡";

		public override FoodType Type => FoodType.主菜;

		public override int Rare => 4;

		public override int SkillCost => 6;

		public 叫花鸡() {
			ComboEffect = new ComboEffect(this, FoodType.副菜, (ComboEffectType.次数, 2), (ComboEffectType.费用, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 1 && foods[0].Type == FoodType.副菜;
		}
	}
}
