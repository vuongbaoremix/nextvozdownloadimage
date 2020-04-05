using System;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x0200000F RID: 15
	public abstract class EXListViewSubItemAB : ListViewItem.ListViewSubItem
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003B06 File Offset: 0x00001D06
		public EXListViewSubItemAB()
		{
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003B1C File Offset: 0x00001D1C
		public EXListViewSubItemAB(string text)
		{
			base.Text = text;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003B3C File Offset: 0x00001D3C
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003B54 File Offset: 0x00001D54
		public string MyValue
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06000057 RID: 87
		public abstract int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch);

		// Token: 0x04000023 RID: 35
		private string _value = "";
	}
}
