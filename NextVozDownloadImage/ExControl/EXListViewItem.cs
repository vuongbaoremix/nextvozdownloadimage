using System;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000015 RID: 21
	public class EXListViewItem : ListViewItem
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003F17 File Offset: 0x00002117
		public EXListViewItem()
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003F22 File Offset: 0x00002122
		public EXListViewItem(string text)
		{
			base.Text = text;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003F38 File Offset: 0x00002138
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003F50 File Offset: 0x00002150
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

		// Token: 0x04000028 RID: 40
		private string _value;
	}
}
