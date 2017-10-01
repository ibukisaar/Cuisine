using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	public abstract class Food {
		public static IReadOnlyList<Food> Foods { get; }

		static Food() {
			var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Food)));
			Foods = types.Select(t => t.GetConstructor(System.Type.EmptyTypes).Invoke(null) as Food).ToArray();
		}

		public abstract string Name { get; }
		public abstract FoodType Type { get; }
		public abstract int Rare { get; }
		public ComboEffect ComboEffect { get; protected set; }
		public ComboEffect GlobalComboEffect { get; protected set; }
		public virtual int BaseScore => 10 * Rare;
		public string FullName => $"{Name}({Type}){new string('★', Rare)}";
		public virtual int SkillTimes => 1;
		public abstract int SkillCost { get; }

		public Food() {
			ComboEffect = new ComboEffect(this, FoodType.None);
			GlobalComboEffect = new ComboEffect(this, FoodType.None);
		}

		public abstract bool Test(IReadOnlyList<Food> foods);

		public virtual int CalcScore(IReadOnlyList<ComboEffect> comboEffects) {
			var 次数 = comboEffects.Sum(ce => ce.Items.Where(i => i.Type == ComboEffectType.次数).Sum(i => i.Value));
			var 费用 = comboEffects.Sum(ce => ce.Items.Where(i => i.Type == ComboEffectType.费用).Sum(i => i.Value));
			var 抽取几率 = comboEffects.Sum(ce => ce.Items.Where(i => i.Type == ComboEffectType.抽取几率).Sum(i => i.Value));
			float score = BaseScore;
			score *= (Math.Max(1, 次数 + SkillTimes) + 1) / (SkillTimes + 1);
			score *= (SkillCost + 1) / (Math.Max(1, 费用 + SkillCost) + 1);
			score *= (float) Math.Sqrt(Math.Max(0, 100 + 抽取几率) / 100);
			return (int) score;
		}

		public override string ToString()
			=> $"{FullName} ->{ComboEffect.TargetType}";
	}
}
