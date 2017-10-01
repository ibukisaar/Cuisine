using System;
using System.Collections.Generic;
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

namespace 料理次元装盘 {
	/// <summary>
	/// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
	///
	/// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
	/// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
	/// 元素中: 
	///
	///     xmlns:MyNamespace="clr-namespace:料理次元装盘"
	///
	///
	/// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
	/// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
	/// 元素中: 
	///
	///     xmlns:MyNamespace="clr-namespace:料理次元装盘;assembly=料理次元装盘"
	///
	/// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
	/// 并重新生成以避免编译错误: 
	///
	///     在解决方案资源管理器中右击目标项目，然后依次单击
	///     “添加引用”->“项目”->[浏览查找并选择此项目]
	///
	///
	/// 步骤 2)
	/// 继续操作并在 XAML 文件中使用控件。
	///
	///     <MyNamespace:单元/>
	///
	/// </summary>
	public class 单元 : Control {
		static 单元() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(单元), new FrameworkPropertyMetadata(typeof(单元)));
		}


		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(单元), new PropertyMetadata("", TextChanged), ValidateText);

		public string Text {
			get => GetValue(TextProperty) as string;
			set => SetValue(TextProperty, value);
		}

		private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (e.OldValue as string != e.NewValue as string) {
				if (d is 单元 @this) {
					@this.InvalidateVisual();
				}
			}
		}

		private static bool ValidateText(object value) {
			return value is string s && (string.IsNullOrWhiteSpace(s) || int.TryParse(s, out _));
		}


		public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register("CellSize", typeof(double), typeof(单元), new PropertyMetadata(20d, CellSizeChanged), VaildateCellSize);

		public double CellSize {
			get => (double) GetValue(CellSizeProperty);
			set => SetValue(CellSizeProperty, value);
		}

		private static void CellSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if ((double) e.OldValue != (double) e.NewValue) {
				if (d is 单元 @this) {
					@this.UpdateGeometry();
					@this.InvalidateVisual();
				}
			}
		}

		public static readonly DependencyProperty CellBrushProperty = DependencyProperty.Register("CellBrush", typeof(Brush), typeof(单元), new PropertyMetadata(Brushes.Pink, CellBrushChanged));

		public Brush CellBrush {
			get => (Brush) GetValue(CellBrushProperty);
			set => SetValue(CellBrushProperty, value);
		}

		private static void CellBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (!Equals(e.NewValue, e.OldValue)) {
				if (d is 单元 @this) {
					@this.InvalidateVisual();
				}
			}
		}

		private static bool VaildateCellSize(object value) {
			return (double) value >= 0;
		}


		public static readonly DependencyProperty ActiveProperty = DependencyProperty.Register("Active", typeof(bool), typeof(单元), new PropertyMetadata(false));

		public bool Active {
			get => (bool) GetValue(ActiveProperty);
			set => SetValue(ActiveProperty, value);
		}


		private StreamGeometry borderGeometry = null;

		private void UpdateGeometry() {
			var scale = CellSize;
			var height = scale * Math.Sqrt(3) / 2;
			var sg = new StreamGeometry();
			var sgc = sg.Open();
			sgc.BeginFigure(new Point(scale / 2, 0), true, true);
			sgc.PolyLineTo(new[] {
				new Point(scale * 3 / 2, 0),
				new Point(scale * 2, height),
				new Point(scale * 3 / 2, height * 2),
				new Point(scale / 2, height * 2),
				new Point(0, height),
			}, true, true);
			sgc.Close();
			sg.Freeze();
			borderGeometry = sg;
		}

		protected override void OnRender(DrawingContext drawingContext) {
			base.OnRender(drawingContext);
			if (borderGeometry == null) UpdateGeometry();
			drawingContext.DrawGeometry(null, new Pen(CellBrush, 2), borderGeometry);

			if (!string.IsNullOrWhiteSpace(Text)) {
				var scale = CellSize;
				var formattedText = new FormattedText(Text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Consola"), scale, Brushes.LightGreen, new NumberSubstitution(), TextFormattingMode.Ideal);
				var point = new Point(scale, scale * Math.Sqrt(3) / 2);
				point.Offset(-formattedText.Width / 2, -formattedText.Height / 2);
				drawingContext.DrawText(formattedText, point);
			}
		}

		protected override Size MeasureOverride(Size constraint) {
			return Size.Empty;
		}

		protected override Size ArrangeOverride(Size arrangeBounds) {
			var scale = CellSize;
			return new Size(scale * 2, scale * Math.Sqrt(3));
		}
	}
}
