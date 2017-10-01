using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	public static class Ex {
		public static IEnumerable<T[]> Combination<T>(this IReadOnlyList<T> list, int count) {
			if (count > list.Count) throw new ArgumentOutOfRangeException(nameof(count));

			var result = new T[count];
			var indexes = Enumerable.Range(0, count).ToArray();
			var length = list.Count;

			if (count == 0) {
				yield return result;
				yield break;
			}

			while (true) {
				for (int i = 0; i < count; i++) result[i] = list[indexes[i]];
				yield return result;

				for (int i = count - 1, sub = 0; i >= 0; i--, sub++) {
					indexes[i]++;
					if (indexes[i] >= length - sub) {
						if (i == 0) {
							yield break;
						}
					} else {
						for (int j = i + 1; j < count; j++) {
							indexes[j] = indexes[j - 1] + 1;
						}
						break;
					}
				}
			}
		}

		private static void Swap<T>(ref T x, ref T y) {
			T t = x;
			x = y;
			y = t;
		}

		private static IEnumerable<T[]> Permutations<T>(T[] array, int i) {
			var length = array.Length;
			if (i == length) {
				yield return array;
			} else {
				foreach (var r in Permutations(array, i + 1)) yield return r;
				for (int j = i + 1; j < length; j++) {
					Swap(ref array[i], ref array[j]);
					foreach (var r in Permutations(array, i + 1)) yield return r;
					Swap(ref array[i], ref array[j]);
				}
			}
		}

		public static IEnumerable<T[]> Permutations<T>(this IEnumerable<T> list) {
			return Permutations(list.ToArray(), 0);
		}
	}
}
