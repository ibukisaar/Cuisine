using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 蓝色玛格丽特 : Food {
		public override string Name => "蓝色玛格丽特";

		public override FoodType Type => FoodType.汤饮;

		public override int Rare => 3;

		public override int SkillCost => 6;

		public 蓝色玛格丽特() {
			ComboEffect = new ComboEffect(this, FoodType.主菜, (ComboEffectType.费用, -1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 1 && foods[0].Type == FoodType.主菜;
		}
	}
}
