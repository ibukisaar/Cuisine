using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘.Foods {
	public sealed class 春卷 : Food {
		public override string Name => "春卷";

		public override FoodType Type => FoodType.头盘;

		public override int Rare => 3;

		public override int SkillTimes => 2;

		public override int SkillCost => 2;

		//public override ComboEffect ComboEffect { get; }// = new ComboEffect(FoodType.All, "料理技的次数+1。重装料理技时，CD+1秒。");

		public 春卷() {
			ComboEffect = new ComboEffect(this, FoodType.All, (ComboEffectType.次数, 1));
			GlobalComboEffect = new ComboEffect(this, (ComboEffectType.CD_秒, 1));
		}

		public override bool Test(IReadOnlyList<Food> foods) {
			return foods.Count == 3 && foods.Any(f => f.Type == FoodType.主食) && foods.Any(f => f.Type == FoodType.主菜);
		}
	}
}
