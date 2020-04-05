using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000002 RID: 2
	internal class EXComboBox : ComboBox
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public EXComboBox()
		{
			this._highlightbrush = SystemBrushes.Highlight;
			base.DrawMode = DrawMode.OwnerDrawFixed;
			base.DrawItem += this.this_DrawItem;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
		public Brush MyHighlightBrush
		{
			get
			{
				return this._highlightbrush;
			}
			set
			{
				this._highlightbrush = value;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A8 File Offset: 0x000002A8
		private void this_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index != -1)
			{
				e.DrawBackground();
				if ((e.State & DrawItemState.Selected) != DrawItemState.None)
				{
					e.Graphics.FillRectangle(this._highlightbrush, e.Bounds);
				}
				EXComboBox.EXItem item = (EXComboBox.EXItem)base.Items[e.Index];
				Rectangle bounds = e.Bounds;
				int x = bounds.X + 2;
				if (item.GetType() == typeof(EXComboBox.EXImageItem))
				{
					EXComboBox.EXImageItem imgitem = (EXComboBox.EXImageItem)item;
					if (imgitem.MyImage != null)
					{
						Image img = imgitem.MyImage;
						int y = bounds.Y + bounds.Height / 2 - img.Height / 2 + 1;
						e.Graphics.DrawImage(img, x, y, img.Width, img.Height);
						x += img.Width + 2;
					}
				}
				else if (item.GetType() == typeof(EXComboBox.EXMultipleImagesItem))
				{
					EXComboBox.EXMultipleImagesItem imgitem2 = (EXComboBox.EXMultipleImagesItem)item;
					if (imgitem2.MyImages != null)
					{
						for (int i = 0; i < imgitem2.MyImages.Count; i++)
						{
							Image img = (Image)imgitem2.MyImages[i];
							int y = bounds.Y + bounds.Height / 2 - img.Height / 2 + 1;
							e.Graphics.DrawImage(img, x, y, img.Width, img.Height);
							x += img.Width + 2;
						}
					}
				}
				int fonty = bounds.Y + bounds.Height / 2 - e.Font.Height / 2;
				e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), (float)x, (float)fonty);
				e.DrawFocusRectangle();
			}
		}

		// Token: 0x04000001 RID: 1
		private Brush _highlightbrush;

		// Token: 0x02000003 RID: 3
		public class EXItem
		{
			// Token: 0x06000005 RID: 5 RVA: 0x000022C2 File Offset: 0x000004C2
			public EXItem()
			{
			}

			// Token: 0x06000006 RID: 6 RVA: 0x000022E3 File Offset: 0x000004E3
			public EXItem(string text)
			{
				this._text = text;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000007 RID: 7 RVA: 0x0000230C File Offset: 0x0000050C
			// (set) Token: 0x06000008 RID: 8 RVA: 0x00002324 File Offset: 0x00000524
			public string Text
			{
				get
				{
					return this._text;
				}
				set
				{
					this._text = value;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000009 RID: 9 RVA: 0x00002330 File Offset: 0x00000530
			// (set) Token: 0x0600000A RID: 10 RVA: 0x00002348 File Offset: 0x00000548
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

			// Token: 0x0600000B RID: 11 RVA: 0x00002354 File Offset: 0x00000554
			public override string ToString()
			{
				return this._text;
			}

			// Token: 0x04000002 RID: 2
			private string _text = "";

			// Token: 0x04000003 RID: 3
			private string _value = "";
		}

		// Token: 0x02000004 RID: 4
		public class EXImageItem : EXComboBox.EXItem
		{
			// Token: 0x0600000C RID: 12 RVA: 0x0000236C File Offset: 0x0000056C
			public EXImageItem()
			{
			}

			// Token: 0x0600000D RID: 13 RVA: 0x00002377 File Offset: 0x00000577
			public EXImageItem(string text)
			{
				base.Text = text;
			}

			// Token: 0x0600000E RID: 14 RVA: 0x0000238A File Offset: 0x0000058A
			public EXImageItem(Image image)
			{
				this._image = image;
			}

			// Token: 0x0600000F RID: 15 RVA: 0x0000239C File Offset: 0x0000059C
			public EXImageItem(string text, Image image)
			{
				base.Text = text;
				this._image = image;
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000023B6 File Offset: 0x000005B6
			public EXImageItem(Image image, string value)
			{
				this._image = image;
				base.MyValue = value;
			}

			// Token: 0x06000011 RID: 17 RVA: 0x000023D0 File Offset: 0x000005D0
			public EXImageItem(string text, Image image, string value)
			{
				base.Text = text;
				this._image = image;
				base.MyValue = value;
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000012 RID: 18 RVA: 0x000023F4 File Offset: 0x000005F4
			// (set) Token: 0x06000013 RID: 19 RVA: 0x0000240C File Offset: 0x0000060C
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

			// Token: 0x04000004 RID: 4
			private Image _image;
		}

		// Token: 0x02000005 RID: 5
		public class EXMultipleImagesItem : EXComboBox.EXItem
		{
			// Token: 0x06000014 RID: 20 RVA: 0x00002416 File Offset: 0x00000616
			public EXMultipleImagesItem()
			{
			}

			// Token: 0x06000015 RID: 21 RVA: 0x00002421 File Offset: 0x00000621
			public EXMultipleImagesItem(string text)
			{
				base.Text = text;
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002434 File Offset: 0x00000634
			public EXMultipleImagesItem(ArrayList images)
			{
				this._images = images;
			}

			// Token: 0x06000017 RID: 23 RVA: 0x00002446 File Offset: 0x00000646
			public EXMultipleImagesItem(string text, ArrayList images)
			{
				base.Text = text;
				this._images = images;
			}

			// Token: 0x06000018 RID: 24 RVA: 0x00002460 File Offset: 0x00000660
			public EXMultipleImagesItem(ArrayList images, string value)
			{
				this._images = images;
				base.MyValue = value;
			}

			// Token: 0x06000019 RID: 25 RVA: 0x0000247A File Offset: 0x0000067A
			public EXMultipleImagesItem(string text, ArrayList images, string value)
			{
				base.Text = text;
				this._images = images;
				base.MyValue = value;
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001A RID: 26 RVA: 0x0000249C File Offset: 0x0000069C
			// (set) Token: 0x0600001B RID: 27 RVA: 0x000024B4 File Offset: 0x000006B4
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

			// Token: 0x04000005 RID: 5
			private ArrayList _images;
		}
	}
}
