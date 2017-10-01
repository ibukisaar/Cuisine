using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	public class ComboEffect {
		public struct Item {
			public ComboEffectType Type { get; }
			public float Value { get; }

			public Item(ComboEffectType Type, float Value) {
				this.Type = Type;
				this.Value = Value;
			}

			public override string ToString() {
				switch (Type) {
					case ComboEffectType.次数: return $"次数{(Value > 0 ? $"+{Value}" : $"{Value}")}";
					case ComboEffectType.费用: return $"费用{(Value > 0 ? $"+{Value}" : $"{Value}")}";
					case ComboEffectType.抽取几率: return $"抽取几率{(Value > 0 ? $"+{Value}%" : $"{Value}%")}";
					case ComboEffectType.CD_秒: return $"CD{(Value > 0 ? $"+{Value}秒" : $"{Value}秒")}";
					case ComboEffectType.CD_百分比: return $"CD{(Value > 0 ? $"+{Value}%" : $"{Value}%")}";
					default: throw new NotImplementedException();
				}
			}
		}

		public Food Source { get; }
		public FoodType TargetType { get; }
		public Item[] Items { get; }
		public bool Global => TargetType == FoodType.None;

		public ComboEffect(Food source, FoodType targetType, params (ComboEffectType Type, float Value)[] values) {
			Source = source;
			TargetType = targetType;
			Items = new Item[values.Length];
			for (int i = 0; i < values.Length; i++) {
				Items[i] = new Item(values[i].Type, values[i].Value);
			}
		}

		public ComboEffect(Food source, params (ComboEffectType Type, float Value)[] values)
			: this(source, FoodType.None, values) { }

		public ComboEffect(Food source, FoodType targetType, IEnumerable<Item> items) {
			Source = source;
			TargetType = targetType;
			Items = items.ToArray();
		}

		public override string ToString() {
			return string.Join(", ", Items);
		}
	}
}
