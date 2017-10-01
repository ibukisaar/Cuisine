using System;
using System.Collections;
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
	///     <MyNamespace:GroupView/>
	///
	/// </summary>
	public class LabelBorder : Border {
		static LabelBorder() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(LabelBorder), new FrameworkPropertyMetadata(typeof(LabelBorder)));
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(UIElement), typeof(LabelBorder), new PropertyMetadata(null, LabelChanged));

		private static void LabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			if (d is LabelBorder @this) {
				@this.InvalidateVisual();
			}
		}

		public UIElement Label {
			get => GetValue(LabelProperty) as UIElement;
			set => SetValue(LabelProperty, value);
		}

		private StreamGeometry borderGeometry = new StreamGeometry();

		protected override int VisualChildrenCount => (Label != null ? 1 : 0) + (Child != null ? 1 : 0);

		protected override Visual GetVisualChild(int index) {
			switch (index) {
				case 0: return Label ?? Child ?? throw new ArgumentOutOfRangeException();
				case 1: return Label != null ? Child ?? throw new ArgumentOutOfRangeException() : throw new ArgumentOutOfRangeException();
				default: throw new ArgumentOutOfRangeException();
			}
		}

		protected override IEnumerator LogicalChildren {
			get {
				if (Label != null) yield return Label;
				if (Child != null) yield return Child;
			}
		}

		protected override void OnRender(DrawingContext drawingContext) {
			if (Label == null) { base.OnRender(drawingContext); return; }

			var pen = new Pen {
				Thickness = 1,
				Brush = BorderBrush,
				LineJoin = PenLineJoin.Round,
			};
			drawingContext.DrawGeometry(null, pen, borderGeometry);
		}

		protected override Size MeasureOverride(Size constraint) {
			Size result = new Size();
			if (Label != null) {
				Label.Measure(constraint);
				constraint.Height = Math.Max(0, constraint.Height - Label.DesiredSize.Height);
				result = new Size(Label.DesiredSize.Width + Label.DesiredSize.Height / 2 + 10, Label.DesiredSize.Height);
			}
			if (Child != null) {
				Child.Measure(constraint);
				result.Height += Child.DesiredSize.Height;
				result.Width = Math.Max(result.Width, Child.DesiredSize.Width);
			}
			return result;
		}

		protected override Size ArrangeOverride(Size arrangeSize) {
			var label = Label;
			var child = Child;
			double width = arrangeSize.Width, height = 0;
			if (label != null) {
				label.Arrange(new Rect(new Point(0, height), label.DesiredSize));
				width = Math.Max(width, label.DesiredSize.Width);
				height += label.DesiredSize.Height;
			}
			if (child != null) {
				var childSize = new Size(width, Math.Max(arrangeSize.Height - height, 0));
				childSize.Width = Math.Max(childSize.Width, child.DesiredSize.Width);
				childSize.Height = Math.Max(childSize.Height, child.DesiredSize.Height);
				child.Arrange(new Rect(new Point(0, height), childSize));
				width = Math.Max(width, child.DesiredSize.Width);
				height += child.DesiredSize.Height;
			}
			UpdateBorderGeometry(new Size(width, Math.Max(arrangeSize.Height, height)));
			return arrangeSize;
		}

		private static double Round(double x) {
			var t = Math.Truncate(x);
			return x - t <= 0.5 ? t - 0.5 : t + 0.5;
		}

		private void UpdateBorderGeometry(Size size) {
			var snapsToDevicePixels = (bool)GetValue(SnapsToDevicePixelsProperty);
			using (var ctx = borderGeometry.Open()) {
				ctx.BeginFigure(snapsToDevicePixels ? new Point(Round(0), Round(0)) : new Point(), false, true);

				var points = new Point[5];
				var curr = new Point();
				curr.Offset(Label.DesiredSize.Width, 0); points[0] = curr;
				curr.Offset(Label.DesiredSize.Height / 2, Label.DesiredSize.Height); points[1] = curr;
				curr = new Point(size.Width, Label.DesiredSize.Height); points[2] = curr;
				curr = new Point(size.Width, size.Height); points[3] = curr;
				curr = new Point(0, size.Height); points[4] = curr;
				if (snapsToDevicePixels) {
					for (int i = 0; i < points.Length; i++) {
						points[i] = new Point(Round(points[i].X), Round(points[i].Y));
					}
				}
				ctx.PolyLineTo(points, true, false);
			}
		}
	}
}
