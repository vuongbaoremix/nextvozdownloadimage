using System;
using System.Drawing;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000014 RID: 20
	public class EXBoolListViewSubItem : EXListViewSubItemAB
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003E2D File Offset: 0x0000202D
		public EXBoolListViewSubItem()
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003E38 File Offset: 0x00002038
		public EXBoolListViewSubItem(bool val)
		{
			this._value = val;
			base.MyValue = val.ToString();
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003E58 File Offset: 0x00002058
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003E70 File Offset: 0x00002070
		public bool BoolValue
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				base.MyValue = value.ToString();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003E88 File Offset: 0x00002088
		public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
		{
			EXBoolColumnHeader boolcol = (EXBoolColumnHeader)ch;
			Image boolimg;
			if (this.BoolValue)
			{
				boolimg = boolcol.TrueImage;
			}
			else
			{
				boolimg = boolcol.FalseImage;
			}
			int imgy = e.Bounds.Y + e.Bounds.Height / 2 - boolimg.Height / 2;
			e.Graphics.DrawImage(boolimg, x, imgy, boolimg.Width, boolimg.Height);
			x += boolimg.Width + 2;
			return x;
		}

		// Token: 0x04000027 RID: 39
		private bool _value;
	}
}
