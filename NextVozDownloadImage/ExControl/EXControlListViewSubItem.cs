using System;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000011 RID: 17
	public class EXControlListViewSubItem : EXListViewSubItemAB
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003B9C File Offset: 0x00001D9C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public Control MyControl
		{
			get
			{
				return this._control;
			}
			set
			{
				this._control = value;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
		{
			return x;
		}

		// Token: 0x04000024 RID: 36
		private Control _control;
	}
}
