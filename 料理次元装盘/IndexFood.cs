using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	public class IndexFood : INotifyPropertyChanged {
		private int index;
		private Food food;

		public int Index {
			get => index;
			set {
				index = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Index)));
			}
		}
		public Food Food {
			get => food;
			set {
				food = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Food)));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public override int GetHashCode() {
			return Food.GetHashCode();
		}

		public override bool Equals(object obj) {
			return obj is IndexFood other && Food.Equals(other.Food);
		}
	}
}
