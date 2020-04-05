using System;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x0200000C RID: 12
	public class EXColumnHeader : ColumnHeader
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000038F8 File Offset: 0x00001AF8
		public EXColumnHeader()
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003903 File Offset: 0x00001B03
		public EXColumnHeader(string text)
		{
			base.Text = text;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003916 File Offset: 0x00001B16
		public EXColumnHeader(string text, int width)
		{
			base.Text = text;
			base.Width = width;
		}
	}
}
