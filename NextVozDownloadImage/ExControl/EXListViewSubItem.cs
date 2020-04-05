using System;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000010 RID: 16
	public class EXListViewSubItem : EXListViewSubItemAB
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003B5E File Offset: 0x00001D5E
		public EXListViewSubItem()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003B69 File Offset: 0x00001D69
		public EXListViewSubItem(string text)
		{
			base.Text = text;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003B7C File Offset: 0x00001D7C
		public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
		{
			return x;
		}
	}
}
