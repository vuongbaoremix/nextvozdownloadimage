using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000013 RID: 19
	public class EXMultipleImagesListViewSubItem : EXListViewSubItemAB
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003CDF File Offset: 0x00001EDF
		public EXMultipleImagesListViewSubItem()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003CEA File Offset: 0x00001EEA
		public EXMultipleImagesListViewSubItem(string text)
		{
			base.Text = text;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003CFD File Offset: 0x00001EFD
		public EXMultipleImagesListViewSubItem(ArrayList images)
		{
			this._images = images;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003D0F File Offset: 0x00001F0F
		public EXMultipleImagesListViewSubItem(ArrayList images, string value)
		{
			this._images = images;
			base.MyValue = value;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003D29 File Offset: 0x00001F29
		public EXMultipleImagesListViewSubItem(string text, ArrayList images, string value)
		{
			base.Text = text;
			this._images = images;
			base.MyValue = value;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003D4C File Offset: 0x00001F4C
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003D64 File Offset: 0x00001F64
		public ArrayList MyImages
		{
			get
			{
				return this._images;
			}
			set
			{
				this._images = value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003D70 File Offset: 0x00001F70
		public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
		{
			if (this.MyImages != null && this.MyImages.Count > 0)
			{
				for (int i = 0; i < this.MyImages.Count; i++)
				{
					Image img = (Image)this.MyImages[i];
					int imgy = e.Bounds.Y + e.Bounds.Height / 2 - img.Height / 2;
					e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
					x += img.Width + 2;
				}
			}
			return x;
		}

		// Token: 0x04000026 RID: 38
		private ArrayList _images;
	}
}
