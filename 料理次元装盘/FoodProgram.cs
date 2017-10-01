using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	public sealed class FoodProgram : IReadOnlyList<FoodStatus>, IComparable<FoodProgram> {
		private IReadOnlyList<FoodStatus> foodStatuses;

		public FoodProgram(IReadOnlyList<FoodStatus> foodStatuses) {
			this.foodStatuses = foodStatuses.OrderBy(fs => fs).ToArray();
			for (int i = 0; i < this.foodStatuses.Count; i++) {
				this.foodStatuses[i].Index = i + 1;
			}
			ActiveCount = foodStatuses.Count(fs => fs.Active);
			//ActiveComboEffectCount = foodStatuses.Sum(fs => fs.ActiveSkillCount);

			GlobalComboEffects = foodStatuses.Where(fs => fs.Active && fs.Food.GlobalComboEffect.Items.Length > 0).Select(fs => fs.Food.GlobalComboEffect).ToArray();
			GlobalComboEffectCount = GlobalComboEffects.Sum(gce => gce.Items.Length);

			var cd = 15f;
			cd *= 1 + GlobalComboEffects.Sum(ce => ce.Items.Where(i => i.Type == ComboEffectType.CD_百分比).Sum(i => i.Value)) / 100f;
			cd += GlobalComboEffects.Sum(ce => ce.Items.Where(i => i.Type == ComboEffectType.CD_秒).Sum(i => i.Value));

			Score = foodStatuses.Sum(fs => fs.Food.CalcScore(fs.ComboEffects));
			Score = (int) (Score * (15f + 30) / (Math.Max(5, cd) + 30));
		}

		public FoodStatus this[int index] => foodStatuses[index];

		public int Count => foodStatuses.Count;

		public int ActiveCount { get; }

		//public int ActiveComboEffectCount { get; }

		public int Score { get; }

		public IReadOnlyList<ComboEffect> GlobalComboEffects { get; }

		public int GlobalComboEffectCount { get; }

		public int[] CellIndexes { get; set; }

		public int CompareTo(FoodProgram other) {
			return other.Score - Score;
		}

		public IEnumerator<FoodStatus> GetEnumerator() {
			return foodStatuses.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
