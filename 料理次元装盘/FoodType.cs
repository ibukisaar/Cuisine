using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 料理次元装盘 {
	[Flags]
	public enum FoodType {
		None = 0, 主食 = 1, 主菜 = 2, 副菜 = 4, 甜点 = 8, 头盘 = 16, 汤饮 = 32,
		All = 主食 | 主菜 | 副菜 | 甜点 | 头盘 | 汤饮
	}
}
