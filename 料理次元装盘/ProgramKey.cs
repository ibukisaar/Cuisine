using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	unsafe public sealed class ProgramKey {
		const ulong Primes = 0x110D0B070503;

		private sealed class Ref : IComparable<Ref> {
			public sealed class Comparer : IComparer<Ref> {
				public static readonly Comparer Default = new Comparer();

				public int Compare(Ref x, Ref y) {
					return x.Value - y.Value;
				}
			}

			public int Value;

			public Ref(int value) => Value = value;

			public int CompareTo(Ref other) {
				return Value - other.Value;
			}

			public override string ToString() {
				return Value.ToString();
			}
		}

		public sealed class Comparer : IEqualityComparer<ProgramKey> {
			public static readonly Comparer Default = new Comparer();

			public bool Equals(ProgramKey x, ProgramKey y) {
				if (x.GetPrimesHashCode() != y.GetPrimesHashCode()) return false;

				bool result = false;
				var other = Clone(y.rimIndexes);
				int count = other.Length;
				var sortedOther = new Ref[count][];
				foreach (var indexes in Enumerable.Range(0, count).Permutations()) {
					for (int i = 0; i < count; i++) {
						y.template[i].Value = indexes[i];
					}
					for (int i = 0; i < count; i++) {
						sortedOther[other[i][0].Value] = other[i];
					}

					for (int i = 0; i < count; i++) {
						int subLength = sortedOther[i].Length;
						if (x.rimIndexes[i].Count != subLength) goto False;
						Array.Sort(sortedOther[i], 1, subLength - 1);
						for (int j = 1; j < subLength; j++) {
							if (x.rimIndexes[i][j].Value != sortedOther[i][j].Value) goto False;
						}
					}

					result = true;
					goto Return;
					False:;
				}

				Return:
				for (int i = 0; i < count; i++) {
					y.template[i].Value = i;
				}
				return result;
			}

			public int GetHashCode(ProgramKey obj) {
				ulong primes = Primes;
				int count = obj.rimIndexes.Length;
				byte* counts = stackalloc byte[count];
				foreach (var indexes in obj.rimIndexes) {
					for (int i = 1; i < indexes.Count; i++) {
						counts[indexes[i].Value]++;
					}
				}
				int hash = 1;
				for (int i = 0; i < count; i++) {
					hash *= ((byte*) &primes)[counts[i]];
				}
				return hash * obj.GetPrimesHashCode();
			}
		}

		private Ref[] template;
		private List<Ref>[] rimIndexes;

		public ProgramKey(int count) {
			template = new Ref[count];
			rimIndexes = new List<Ref>[count];
			for (int i = 0; i < count; i++) {
				template[i] = new Ref(i);
				rimIndexes[i] = new List<Ref>(count) { template[i] };
			}
		}

		public void Add(int self, int other) {
			rimIndexes[self].Add(template[other]);
		}

		public void Sort() {
			foreach (var indexes in rimIndexes) {
				indexes.Sort(1, indexes.Count - 1, Ref.Comparer.Default);
			}
		}

		public IEnumerable<int> this[int self]
			=> rimIndexes[self].Skip(1).Select(r => r.Value);

		private static Ref[][] Clone(List<Ref>[] obj) {
			var result = new Ref[obj.Length][];
			for (int i = 0; i < result.Length; i++) {
				result[i] = new Ref[obj[i].Count];
				obj[i].CopyTo(result[i]);
			}
			return result;
		}

		private int GetPrimesHashCode() {
			ulong primes = Primes;
			int hash = 1;
			for (int i = 0; i < rimIndexes.Length; i++) {
				hash *= ((byte*) &primes)[rimIndexes[i].Count - 1];
			}
			return hash;
		}
	}
}
