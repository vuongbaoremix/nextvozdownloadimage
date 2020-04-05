using System;
using System.Drawing;

namespace EXControls
{
	// Token: 0x02000016 RID: 22
	public class EXImageListViewItem : EXListViewItem
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003F5A File Offset: 0x0000215A
		public EXImageListViewItem()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003F65 File Offset: 0x00002165
		public EXImageListViewItem(string text)
		{
			base.Text = text;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003F78 File Offset: 0x00002178
		public EXImageListViewItem(Image image)
		{
			this._image = image;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003F8A File Offset: 0x0000218A
		public EXImageListViewItem(string text, Image image)
		{
			this._image = image;
			base.Text = text;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003FA4 File Offset: 0x000021A4
		public EXImageListViewItem(string text, Image image, string value)
		{
			base.Text = text;
			this._image = image;
			base.MyValue = value;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003FC8 File Offset: 0x000021C8
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003FE0 File Offset: 0x000021E0
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

		// Token: 0x04000029 RID: 41
		private Image _image;
	}
}
