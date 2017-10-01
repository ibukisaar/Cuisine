using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 料理次元装盘.Foods;

namespace 料理次元装盘 {
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window {
		private static readonly int[][] indexesMap = {
			new[] { 3, 4, 5, 8, 9, 10 },
			new[] { 3, 4 },
			new[] { 3, 5 },
			new[] { 0, 1, 2, 4, 5 },
			new[] { 0, 1, 3, 6, 8 },
			new[] { 0, 2, 3, 7, 9 },
			new[] { 4, 8 },
			new[] { 5, 9 },
			new[] { 0, 4, 6, 10, 11 },
			new[] { 0, 5, 7, 10, 12 },
			new[] { 0, 8, 9, 11, 12 },
			new[] { 8, 10 },
			new[] { 9, 10 }
		};

		private 单元[] cells = new 单元[13];
		private ObservableCollection<Food> templateFoods = new ObservableCollection<Food>(Food.Foods);
		private ObservableCollection<IndexFood> selectFoods = new ObservableCollection<IndexFood>();
		private ObservableCollection<FoodProgram> programFoods = new ObservableCollection<FoodProgram>();

		public MainWindow() {
			InitializeComponent();
		}

		private void Canvas_Loaded(object sender, RoutedEventArgs e) {
			for (int i = 0; i < 13; i++) {
				cells[i] = canvas.FindName($"c{i}") as 单元;
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e) {
			listFoods.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = templateFoods, Mode = BindingMode.OneWay });
			listSelectFoods.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = selectFoods, Mode = BindingMode.OneWay });
			listProgram.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = programFoods, Mode = BindingMode.OneWay });
		}

		private void RunButton_Click(object sender, RoutedEventArgs e) {
			var foodCount = int.Parse(txtFoodCount.Text);
			foodCount = Math.Min(foodCount, selectFoods.Count);
			if (foodCount < 1 || foodCount > 5) {
				MessageBox.Show("食灵数量必须是1到5", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			programFoods.Clear();

			var foodsArray = selectFoods.Select(f => f.Food).ToArray();
			var indexesTemplate = Enumerable.Range(1, 12).ToArray();
			var cellTemplate = new Food[13];
			var tempPeripheryFoods = new List<Food>[foodCount];
			for (int i = 0; i < foodCount; i++) tempPeripheryFoods[i] = new List<Food>();
			var tempResult = new FoodStatus[foodCount];
			var activeFoods = new List<Food>();
			var actives = new bool[foodCount];
			var foodIndexMap = new Dictionary<Food, int>();
			var partResult = new LinkedList<(int Flags, FoodProgram Result)>();

			/*
			 1. 遍历1~12号cell抽取4个cell的所有可能

			 */

			Dictionary<ProgramKey, int[]> indexDict = new Dictionary<ProgramKey, int[]>(ProgramKey.Comparer.Default);
			var indexHoles = new int[13];

			foreach (var indexes in indexesTemplate.Combination(foodCount - 1)) {
				ProgramKey key = new ProgramKey(foodCount);
				for (int i = 1; i < 13; i++) indexHoles[i] = -1;

				for (int i = 1; i < foodCount; i++) {
					indexHoles[indexes[i - 1]] = i;
				}

				foreach (var i in indexesMap[0]) {
					if (indexHoles[i] >= 0) key.Add(0, indexHoles[i]);
				}
				for (int j = 1; j < foodCount; j++) {
					foreach (var i in indexesMap[indexes[j - 1]]) {
						if (indexHoles[i] >= 0) key.Add(j, indexHoles[i]);
					}
				}
				key.Sort();
				// if (indexDict.ContainsKey(key)) continue;
				
				var value = new int[foodCount];
				for (int i = 1; i < foodCount; i++) {
					value[i] = indexes[i - 1];
				}

				try {
					indexDict.Add(key, value);
				} catch { }
			}

			//*
			foreach (var foodsTemplate in foodsArray.Combination(foodCount)) {
				bool best = false;
				partResult.Clear();
				foreach (var indexes in Enumerable.Range(0, foodCount).Permutations()) {
					foreach (var kv in indexDict) {
						int flags = 0;
						var (rimIndexes, cellIndexes) = (kv.Key, kv.Value);

						Array.Clear(actives, 0, foodCount);
						for (int i = 0; i < foodCount; i++) {
							tempPeripheryFoods[i].Clear();
							foreach (var j in rimIndexes[i]) tempPeripheryFoods[i].Add(foodsTemplate[indexes[j]]);
						}

						activeFoods.Clear();
						for (int i = 0; i < foodCount; i++) {
							actives[i] = foodsTemplate[indexes[i]].Test(tempPeripheryFoods[i]);
							if (actives[i]) {
								activeFoods.Add(foodsTemplate[indexes[i]]);
								flags |= 1 << indexes[i];
							}
						}

						for (var node = partResult.First; node != null;) {
							var f = node.Value.Flags;
							if ((f & flags) == flags) goto Bad;
							var next = node.Next;
							if ((f & flags) == f) partResult.Remove(node);
							node = next;
						}

						for (int i = 0; i < foodCount; i++) {
							tempResult[i] = new FoodStatus(foodsTemplate[indexes[i]], actives[i], activeFoods) {
								CellIndex = cellIndexes[i]
							};
						}

						partResult.AddLast((flags, new FoodProgram(tempResult)));
						if (best) break;
						if (flags == (1 << foodCount) - 1) { best = true; }

						Bad:;
					}
					if (best) break;
				}
				foreach (var (_, p) in partResult) programFoods.Add(p);
			}

			/*/
			foreach (var foodsTemplate in foodsArray.Combination(foodCount)) {
				partResult.Clear();
				foodIndexMap.Clear();

				for (int i = 0; i < foodsTemplate.Length; i++) {
					foodIndexMap.Add(foodsTemplate[i], i);
				}

				foreach (var foods in foodsTemplate.Permutations()) {
					cellTemplate[0] = foods[0];
					foreach (var indexes in indexesTemplate.Combination(foodCount - 1)) {
						int flags = 0;
						Array.Clear(cellTemplate, 1, 12);
						Array.Clear(actives, 0, foodCount);
						for (int i = 1; i < foodCount; i++) {
							cellTemplate[indexes[i - 1]] = foods[i];
						}

						for (int i = 0; i < foodCount; i++) tempPeripheryFoods[i].Clear();

						foreach (var i in indexesMap[0]) {
							if (cellTemplate[i] != null) tempPeripheryFoods[0].Add(cellTemplate[i]);
						}
						for (var j = 1; j < foodCount; j++) {
							foreach (var i in indexesMap[indexes[j - 1]]) {
								if (cellTemplate[i] != null) tempPeripheryFoods[j].Add(cellTemplate[i]);
							}
						}

						activeFoods.Clear();
						for (int i = 0; i < foodCount; i++) {
							actives[i] = foods[i].Test(tempPeripheryFoods[i]);
							if (actives[i]) {
								activeFoods.Add(foods[i]);
								flags |= 1 << foodIndexMap[foods[i]];
							}
						}


						for (var node = partResult.First; node != null;) {
							var f = node.Value.Flags;
							if ((f & flags) == flags) goto Bad;
							var next = node.Next;
							if ((f & flags) == f) partResult.Remove(node);
							node = next;
						}

						for (int i = 0; i < foodCount; i++) {
							tempResult[i] = new FoodStatus(foods[i], actives[i], activeFoods) {
								CellIndex = i == 0 ? 0 : indexes[i - 1]
							};
						}

						partResult.AddLast((flags, new FoodProgram(tempResult)));

						if (flags == (1 << foodCount) - 1) continue;

						Bad:;
					}
				}
				foreach (var (_, p) in partResult) programFoods.Add(p);
			}
			//*/

			List<FoodProgram> temp = new List<FoodProgram>(programFoods.ToList());
			programFoods.Clear();
			foreach (var p in temp.OrderByDescending(pr => pr.Score)) {
				programFoods.Add(p);
			}
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e) {
			var select = new List<IndexFood>(listSelectFoods.SelectedItems.Cast<IndexFood>());
			select.ForEach(f => {
				selectFoods.Remove(f);
				templateFoods.Add(f.Food);
			});
			for (int i = 0; i < selectFoods.Count; i++) {
				selectFoods[i].Index = i + 1;
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e) {
			var select = new List<Food>(listFoods.SelectedItems.Cast<Food>());
			select.ForEach(f => {
				selectFoods.Add(new IndexFood { Index = selectFoods.Count + 1, Food = f });
				templateFoods.Remove(f);
			});
		}

		private void ListProgram_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			var foodProgram = listProgram.SelectedItem as FoodProgram;

			for (int i = 0; i < 13; i++) {
				cells[i].ClearValue(单元.ActiveProperty);
				cells[i].ClearValue(单元.TextProperty);
			}
			listDescription.ItemsSource = null;
			globlaEffects.ItemsSource = null;

			if (foodProgram == null) return;
			foreach (var status in foodProgram) {
				var cell = cells[status.CellIndex];
				cell.Active = status.Active;
				cell.Text = status.Index.ToString();
			}
			globlaEffects.ItemsSource = foodProgram.GlobalComboEffects;
			listDescription.ItemsSource = foodProgram;
		}
	}
}
