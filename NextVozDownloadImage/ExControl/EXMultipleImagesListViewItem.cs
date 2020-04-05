using System;
using System.Collections;

namespace EXControls
{
	// Token: 0x02000017 RID: 23
	public class EXMultipleImagesListViewItem : EXListViewItem
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00003FEA File Offset: 0x000021EA
		public EXMultipleImagesListViewItem()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003FF5 File Offset: 0x000021F5
		public EXMultipleImagesListViewItem(string text)
		{
			base.Text = text;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004008 File Offset: 0x00002208
		public EXMultipleImagesListViewItem(ArrayList images)
		{
			this._images = images;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000401A File Offset: 0x0000221A
		public EXMultipleImagesListViewItem(string text, ArrayList images)
		{
			base.Text = text;
			this._images = images;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004034 File Offset: 0x00002234
		public EXMultipleImagesListViewItem(string text, ArrayList images, string value)
		{
			base.Text = text;
			this._images = images;
			base.MyValue = value;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004058 File Offset: 0x00002258
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004070 File Offset: 0x00002270
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

		// Token: 0x0400002A RID: 42
		private ArrayList _images;
	}
}
