using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 烧麦 : Food {
		public override string Name => "烧麦";

		public override FoodType Type => FoodType.副菜;

		public override int Rare => 3;

		public override int SkillCost => 3;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.汤饮, "料理技的次数+1，抽取几率-20%。");

		public 烧麦() {
			ComboEffect = new ComboEffect(this, FoodType.汤饮, (ComboEffectType.次数, 1), (ComboEffectType.抽取几率, -20));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count >= 3;
		}
	}
}
