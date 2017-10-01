using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	public sealed class FoodStatus : IComparable<FoodStatus> {
		public int Index { get; set; }
		public int CellIndex { get; set; }
		public Food Food { get; }
		public bool Active { get; }
		public ComboEffect[] ComboEffects { get; }
		public int ActiveComboEffectCount => ComboEffects.Length;

		public FoodStatus(Food Food, bool Active, IEnumerable<Food> ActiveFoods) {
			this.Food = Food;
			this.Active = Active;
			var comboEffects = ActiveFoods.Where(f => f.ComboEffect.TargetType.HasFlag(Food.Type)).Select(f => f.ComboEffect).ToList();
			this.ComboEffects = comboEffects.ToArray();
		}

		public override string ToString() {
			return $"{Index}. {Food.FullName} [{ActiveComboEffectCount}]";
		}

		public int CompareTo(FoodStatus other) {
			int cmp = (Active ? 0 : 1) - (other.Active ? 0 : 1);
			if (cmp != 0) return cmp;
			return other.Food.Rare - Food.Rare;
		}
	}
}
