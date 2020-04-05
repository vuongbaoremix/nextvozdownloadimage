using System;
using System.Drawing;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000012 RID: 18
	public class EXImageListViewSubItem : EXListViewSubItemAB
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003BD3 File Offset: 0x00001DD3
		public EXImageListViewSubItem()
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003BDE File Offset: 0x00001DDE
		public EXImageListViewSubItem(string text)
		{
			base.Text = text;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003BF1 File Offset: 0x00001DF1
		public EXImageListViewSubItem(Image image)
		{
			this._image = image;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003C03 File Offset: 0x00001E03
		public EXImageListViewSubItem(Image image, string value)
		{
			this._image = image;
			base.MyValue = value;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003C1D File Offset: 0x00001E1D
		public EXImageListViewSubItem(string text, Image image, string value)
		{
			base.Text = text;
			this._image = image;
			base.MyValue = value;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003C40 File Offset: 0x00001E40
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003C58 File Offset: 0x00001E58
		public Image MyImage
		{
			get
			{
				return this._image;
			}
			set
			{
				this._image = value;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003C64 File Offset: 0x00001E64
		public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
		{
			if (this.MyImage != null)
			{
				Image img = this.MyImage;
				int imgy = e.Bounds.Y + e.Bounds.Height / 2 - img.Height / 2;
				e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
				x += img.Width + 2;
			}
			return x;
		}

		// Token: 0x04000025 RID: 37
		private Image _image;
	}
}
