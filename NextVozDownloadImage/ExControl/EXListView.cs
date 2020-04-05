using System;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x02000006 RID: 6
	public class EXListView : ListView
	{
		// Token: 0x0600001C RID: 28
		[DllImport("user32.dll")]
		private static extern bool SendMessage(IntPtr hWnd, uint m, int wParam, int lParam);

		// Token: 0x0600001D RID: 29 RVA: 0x000024C0 File Offset: 0x000006C0
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 15)
			{
				foreach (object obj in this._controls)
				{
					EXListView.EmbeddedControl c = (EXListView.EmbeddedControl)obj;
					Rectangle r = c.MySubItem.Bounds;
					if (r.Y > 0 && r.Y < base.ClientRectangle.Height)
					{
						c.MyControl.Visible = true;
						c.MyControl.Bounds = new Rectangle(r.X + this._cpadding, r.Y + this._cpadding, r.Width - 2 * this._cpadding, r.Height - 2 * this._cpadding);
					}
					else
					{
						c.MyControl.Visible = false;
					}
				}
			}
			int msg = m.Msg;
			switch (msg)
			{
			case 276:
			case 277:
				break;
			default:
				if (msg != 522)
				{
					goto IL_13C;
				}
				break;
			}
			base.Focus();
			IL_13C:
			base.WndProc(ref m);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002624 File Offset: 0x00000824
		private void ScrollMe(int x, int y)
		{
			EXListView.SendMessage(base.Handle, 4116u, x, y);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000263C File Offset: 0x0000083C
		public EXListView()
		{
			this._cpadding = 4;
			this._controls = new ArrayList();
			this._sortcol = -1;
			this._sortcolbrush = SystemBrushes.ControlLight;
			this._highlightbrush = SystemBrushes.Window;
			base.OwnerDraw = true;
			base.FullRowSelect = true;
			base.View = View.Details;
			base.MouseDown += this.this_MouseDown;
			base.MouseDoubleClick += this.this_MouseDoubleClick;
			base.DrawColumnHeader += this.this_DrawColumnHeader;
			base.DrawSubItem += this.this_DrawSubItem;
			base.MouseMove += this.this_MouseMove;
			base.ColumnClick += this.this_ColumnClick;
			this.txtbx = new TextBox();
			this.txtbx.Visible = false;
			base.Controls.Add(this.txtbx);
			this.txtbx.Leave += this.c_Leave;
			this.txtbx.KeyPress += this.txtbx_KeyPress;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002768 File Offset: 0x00000968
		public void AddControlToSubItem(Control control, EXControlListViewSubItem subitem)
		{
			base.Controls.Add(control);
			subitem.MyControl = control;
			EXListView.EmbeddedControl ec;
			ec.MyControl = control;
			ec.MySubItem = subitem;
			this._controls.Add(ec);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027B0 File Offset: 0x000009B0
		public void RemoveControlFromSubItem(EXControlListViewSubItem subitem)
		{
			Control c = subitem.MyControl;
			for (int i = 0; i < this._controls.Count; i++)
			{
				if (((EXListView.EmbeddedControl)this._controls[i]).MySubItem == subitem)
				{
					this._controls.RemoveAt(i);
					subitem.MyControl = null;
					base.Controls.Remove(c);
					c.Dispose();
					break;
				}
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002830 File Offset: 0x00000A30
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002848 File Offset: 0x00000A48
		public int ControlPadding
		{
			get
			{
				return this._cpadding;
			}
			set
			{
				this._cpadding = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002854 File Offset: 0x00000A54
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000286C File Offset: 0x00000A6C
		public Brush MySortBrush
		{
			get
			{
				return this._sortcolbrush;
			}
			set
			{
				this._sortcolbrush = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002878 File Offset: 0x00000A78
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002890 File Offset: 0x00000A90
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

		// Token: 0x06000028 RID: 40 RVA: 0x0000289C File Offset: 0x00000A9C
		private void txtbx_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this._clickedsubitem.Text = this.txtbx.Text;
				this.txtbx.Visible = false;
				this._clickeditem.Tag = null;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028F0 File Offset: 0x00000AF0
		private void c_Leave(object sender, EventArgs e)
		{
			Control c = (Control)sender;
			this._clickedsubitem.Text = c.Text;
			c.Visible = false;
			this._clickeditem.Tag = null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000292C File Offset: 0x00000B2C
		private void this_MouseDown(object sender, MouseEventArgs e)
		{
			ListViewHitTestInfo lstvinfo = base.HitTest(e.X, e.Y);
			ListViewItem.ListViewSubItem subitem = lstvinfo.SubItem;
			if (subitem != null)
			{
				int subx = subitem.Bounds.Left;
				if (subx < 0)
				{
					this.ScrollMe(subx, 0);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002988 File Offset: 0x00000B88
		private void this_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			EXListViewItem lstvItem = base.GetItemAt(e.X, e.Y) as EXListViewItem;
			if (lstvItem != null)
			{
				this._clickeditem = lstvItem;
				int x = lstvItem.Bounds.Left;
				int i;
				for (i = 0; i < base.Columns.Count; i++)
				{
					x += base.Columns[i].Width;
					if (x > e.X)
					{
						x -= base.Columns[i].Width;
						this._clickedsubitem = lstvItem.SubItems[i];
						this._col = i;
						break;
					}
				}
				if (base.Columns[i] is EXColumnHeader)
				{
					EXColumnHeader col = (EXColumnHeader)base.Columns[i];
					if (col.GetType() == typeof(EXEditableColumnHeader))
					{
						EXEditableColumnHeader editcol = (EXEditableColumnHeader)col;
						if (editcol.MyControl != null)
						{
							Control c = editcol.MyControl;
							if (c.Tag != null)
							{
								base.Controls.Add(c);
								c.Tag = null;
								if (c is ComboBox)
								{
									((ComboBox)c).SelectedValueChanged += this.cmbx_SelectedValueChanged;
								}
								c.Leave += this.c_Leave;
							}
							c.Location = new Point(x, base.GetItemRect(base.Items.IndexOf(lstvItem)).Y);
							c.Width = base.Columns[i].Width;
							if (c.Width > base.Width)
							{
								c.Width = base.ClientRectangle.Width;
							}
							c.Text = this._clickedsubitem.Text;
							c.Visible = true;
							c.BringToFront();
							c.Focus();
						}
						else
						{
							this.txtbx.Location = new Point(x, base.GetItemRect(base.Items.IndexOf(lstvItem)).Y);
							this.txtbx.Width = base.Columns[i].Width;
							if (this.txtbx.Width > base.Width)
							{
								this.txtbx.Width = base.ClientRectangle.Width;
							}
							this.txtbx.Text = this._clickedsubitem.Text;
							this.txtbx.Visible = true;
							this.txtbx.BringToFront();
							this.txtbx.Focus();
						}
					}
					else if (col.GetType() == typeof(EXBoolColumnHeader))
					{
						EXBoolColumnHeader boolcol = (EXBoolColumnHeader)col;
						if (boolcol.Editable)
						{
							EXBoolListViewSubItem boolsubitem = (EXBoolListViewSubItem)this._clickedsubitem;
							if (boolsubitem.BoolValue)
							{
								boolsubitem.BoolValue = false;
							}
							else
							{
								boolsubitem.BoolValue = true;
							}
							base.Invalidate(boolsubitem.Bounds);
						}
					}
				}
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002D0C File Offset: 0x00000F0C
		private void cmbx_SelectedValueChanged(object sender, EventArgs e)
		{
			if (((Control)sender).Visible && this._clickedsubitem != null)
			{
				if (sender.GetType() == typeof(EXComboBox))
				{
					EXComboBox excmbx = (EXComboBox)sender;
					object item = excmbx.SelectedItem;
					if (item.GetType() == typeof(EXComboBox.EXImageItem))
					{
						EXComboBox.EXImageItem imgitem = (EXComboBox.EXImageItem)item;
						if (this._col == 0)
						{
							if (this._clickeditem.GetType() == typeof(EXImageListViewItem))
							{
								((EXImageListViewItem)this._clickeditem).MyImage = imgitem.MyImage;
							}
							else if (this._clickeditem.GetType() == typeof(EXMultipleImagesListViewItem))
							{
								EXMultipleImagesListViewItem imglstvitem = (EXMultipleImagesListViewItem)this._clickeditem;
								imglstvitem.MyImages.Clear();
								imglstvitem.MyImages.AddRange(new object[]
								{
									imgitem.MyImage
								});
							}
						}
						else if (this._clickedsubitem.GetType() == typeof(EXImageListViewSubItem))
						{
							EXImageListViewSubItem imgsub = (EXImageListViewSubItem)this._clickedsubitem;
							imgsub.MyImage = imgitem.MyImage;
						}
						else if (this._clickedsubitem.GetType() == typeof(EXMultipleImagesListViewSubItem))
						{
							EXMultipleImagesListViewSubItem imgsub2 = (EXMultipleImagesListViewSubItem)this._clickedsubitem;
							imgsub2.MyImages.Clear();
							imgsub2.MyImages.Add(imgitem.MyImage);
							imgsub2.MyValue = imgitem.MyValue;
						}
					}
					else if (item.GetType() == typeof(EXComboBox.EXMultipleImagesItem))
					{
						EXComboBox.EXMultipleImagesItem imgitem2 = (EXComboBox.EXMultipleImagesItem)item;
						if (this._col == 0)
						{
							if (this._clickeditem.GetType() == typeof(EXImageListViewItem))
							{
								((EXImageListViewItem)this._clickeditem).MyImage = (Image)imgitem2.MyImages[0];
							}
							else if (this._clickeditem.GetType() == typeof(EXMultipleImagesListViewItem))
							{
								EXMultipleImagesListViewItem imglstvitem = (EXMultipleImagesListViewItem)this._clickeditem;
								imglstvitem.MyImages.Clear();
								imglstvitem.MyImages.AddRange(imgitem2.MyImages);
							}
						}
						else if (this._clickedsubitem.GetType() == typeof(EXImageListViewSubItem))
						{
							EXImageListViewSubItem imgsub = (EXImageListViewSubItem)this._clickedsubitem;
							if (imgitem2.MyImages != null)
							{
								imgsub.MyImage = (Image)imgitem2.MyImages[0];
							}
						}
						else if (this._clickedsubitem.GetType() == typeof(EXMultipleImagesListViewSubItem))
						{
							EXMultipleImagesListViewSubItem imgsub2 = (EXMultipleImagesListViewSubItem)this._clickedsubitem;
							imgsub2.MyImages.Clear();
							imgsub2.MyImages.AddRange(imgitem2.MyImages);
							imgsub2.MyValue = imgitem2.MyValue;
						}
					}
				}
				ComboBox c = (ComboBox)sender;
				this._clickedsubitem.Text = c.Text;
				c.Visible = false;
				this._clickeditem.Tag = null;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000030B4 File Offset: 0x000012B4
		private void this_MouseMove(object sender, MouseEventArgs e)
		{
			ListViewItem item = base.GetItemAt(e.X, e.Y);
			if (item != null && item.Tag == null)
			{
				base.Invalidate(item.Bounds);
				item.Tag = "t";
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003107 File Offset: 0x00001307
		private void this_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003114 File Offset: 0x00001314
		private void this_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			e.DrawBackground();
			if (e.ColumnIndex == this._sortcol)
			{
				e.Graphics.FillRectangle(this._sortcolbrush, e.Bounds);
			}
			if ((e.ItemState & ListViewItemStates.Selected) != (ListViewItemStates)0)
			{
				e.Graphics.FillRectangle(this._highlightbrush, e.Bounds);
			}
			int fonty = e.Bounds.Y + e.Bounds.Height / 2 - e.SubItem.Font.Height / 2;
			int x = e.Bounds.X + 2;
			if (e.ColumnIndex == 0)
			{
				EXListViewItem item = (EXListViewItem)e.Item;
				if (item.GetType() == typeof(EXImageListViewItem))
				{
					EXImageListViewItem imageitem = (EXImageListViewItem)item;
					if (imageitem.MyImage != null)
					{
						Image img = imageitem.MyImage;
						int imgy = e.Bounds.Y + e.Bounds.Height / 2 - img.Height / 2;
						e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
						x += img.Width + 2;
					}
				}
				e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), (float)x, (float)fonty);
			}
			else
			{
				EXListViewSubItemAB subitem = e.SubItem as EXListViewSubItemAB;
				if (subitem == null)
				{
					e.DrawDefault = true;
				}
				else
				{
					x = subitem.DoDraw(e, x, base.Columns[e.ColumnIndex] as EXColumnHeader);
					e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), (float)x, (float)fonty);
				}
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000333C File Offset: 0x0000153C
		private void this_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (base.Items.Count != 0)
			{
				for (int i = 0; i < base.Columns.Count; i++)
				{
					base.Columns[i].ImageKey = null;
				}
				for (int i = 0; i < base.Items.Count; i++)
				{
					base.Items[i].Tag = null;
				}
				if (e.Column != this._sortcol)
				{
					this._sortcol = e.Column;
					base.Sorting = SortOrder.Ascending;
					base.Columns[e.Column].ImageKey = "up";
				}
				else if (base.Sorting == SortOrder.Ascending)
				{
					base.Sorting = SortOrder.Descending;
					base.Columns[e.Column].ImageKey = "down";
				}
				else
				{
					base.Sorting = SortOrder.Ascending;
					base.Columns[e.Column].ImageKey = "up";
				}
				if (this._sortcol == 0)
				{
					if (base.Items[0].GetType() == typeof(EXListViewItem))
					{
						base.ListViewItemSorter = new EXListView.ListViewItemComparerText(e.Column, base.Sorting);
					}
					else
					{
						base.ListViewItemSorter = new EXListView.ListViewItemComparerValue(e.Column, base.Sorting);
					}
				}
				else if (base.Items[0].SubItems[this._sortcol].GetType() == typeof(EXListViewSubItemAB))
				{
					base.ListViewItemSorter = new EXListView.ListViewSubItemComparerText(e.Column, base.Sorting);
				}
				else
				{
					base.ListViewItemSorter = new EXListView.ListViewSubItemComparerValue(e.Column, base.Sorting);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private const uint LVM_FIRST = 4096u;

		// Token: 0x04000007 RID: 7
		private const uint LVM_SCROLL = 4116u;

		// Token: 0x04000008 RID: 8
		private const int WM_HSCROLL = 276;

		// Token: 0x04000009 RID: 9
		private const int WM_VSCROLL = 277;

		// Token: 0x0400000A RID: 10
		private const int WM_MOUSEWHEEL = 522;

		// Token: 0x0400000B RID: 11
		private const int WM_PAINT = 15;

		// Token: 0x0400000C RID: 12
		private ListViewItem.ListViewSubItem _clickedsubitem;

		// Token: 0x0400000D RID: 13
		private ListViewItem _clickeditem;

		// Token: 0x0400000E RID: 14
		private int _col;

		// Token: 0x0400000F RID: 15
		private TextBox txtbx;

		// Token: 0x04000010 RID: 16
		private int _sortcol;

		// Token: 0x04000011 RID: 17
		private Brush _sortcolbrush;

		// Token: 0x04000012 RID: 18
		private Brush _highlightbrush;

		// Token: 0x04000013 RID: 19
		private int _cpadding;

		// Token: 0x04000014 RID: 20
		private ArrayList _controls;

		// Token: 0x02000007 RID: 7
		private struct EmbeddedControl
		{
			// Token: 0x04000015 RID: 21
			public Control MyControl;

			// Token: 0x04000016 RID: 22
			public EXControlListViewSubItem MySubItem;
		}

		// Token: 0x02000008 RID: 8
		private class ListViewSubItemComparerText : IComparer
		{
			// Token: 0x06000031 RID: 49 RVA: 0x00003542 File Offset: 0x00001742
			public ListViewSubItemComparerText()
			{
				this._col = 0;
				this._order = SortOrder.Ascending;
			}

			// Token: 0x06000032 RID: 50 RVA: 0x0000355B File Offset: 0x0000175B
			public ListViewSubItemComparerText(int col, SortOrder order)
			{
				this._col = col;
				this._order = order;
			}

			// Token: 0x06000033 RID: 51 RVA: 0x00003574 File Offset: 0x00001774
			public int Compare(object x, object y)
			{
				string xstr = ((ListViewItem)x).SubItems[this._col].Text;
				string ystr = ((ListViewItem)y).SubItems[this._col].Text;
				decimal dec_x;
				decimal dec_y;
				int returnVal;
				DateTime dat_x;
				DateTime dat_y;
				if (decimal.TryParse(xstr, out dec_x) && decimal.TryParse(ystr, out dec_y))
				{
					returnVal = decimal.Compare(dec_x, dec_y);
				}
				else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
				{
					returnVal = DateTime.Compare(dat_x, dat_y);
				}
				else
				{
					returnVal = string.Compare(xstr, ystr);
				}
				if (this._order == SortOrder.Descending)
				{
					returnVal *= -1;
				}
				return returnVal;
			}

			// Token: 0x04000017 RID: 23
			private int _col;

			// Token: 0x04000018 RID: 24
			private SortOrder _order;
		}

		// Token: 0x02000009 RID: 9
		private class ListViewSubItemComparerValue : IComparer
		{
			// Token: 0x06000034 RID: 52 RVA: 0x0000363C File Offset: 0x0000183C
			public ListViewSubItemComparerValue()
			{
				this._col = 0;
				this._order = SortOrder.Ascending;
			}

			// Token: 0x06000035 RID: 53 RVA: 0x00003655 File Offset: 0x00001855
			public ListViewSubItemComparerValue(int col, SortOrder order)
			{
				this._col = col;
				this._order = order;
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00003670 File Offset: 0x00001870
			public int Compare(object x, object y)
			{
				string xstr = ((EXListViewSubItemAB)((ListViewItem)x).SubItems[this._col]).MyValue;
				string ystr = ((EXListViewSubItemAB)((ListViewItem)y).SubItems[this._col]).MyValue;
				decimal dec_x;
				decimal dec_y;
				int returnVal;
				DateTime dat_x;
				DateTime dat_y;
				if (decimal.TryParse(xstr, out dec_x) && decimal.TryParse(ystr, out dec_y))
				{
					returnVal = decimal.Compare(dec_x, dec_y);
				}
				else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
				{
					returnVal = DateTime.Compare(dat_x, dat_y);
				}
				else
				{
					returnVal = string.Compare(xstr, ystr);
				}
				if (this._order == SortOrder.Descending)
				{
					returnVal *= -1;
				}
				return returnVal;
			}

			// Token: 0x04000019 RID: 25
			private int _col;

			// Token: 0x0400001A RID: 26
			private SortOrder _order;
		}

		// Token: 0x0200000A RID: 10
		private class ListViewItemComparerText : IComparer
		{
			// Token: 0x06000037 RID: 55 RVA: 0x00003742 File Offset: 0x00001942
			public ListViewItemComparerText()
			{
				this._col = 0;
				this._order = SortOrder.Ascending;
			}

			// Token: 0x06000038 RID: 56 RVA: 0x0000375B File Offset: 0x0000195B
			public ListViewItemComparerText(int col, SortOrder order)
			{
				this._col = col;
				this._order = order;
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00003774 File Offset: 0x00001974
			public int Compare(object x, object y)
			{
				string xstr = ((ListViewItem)x).Text;
				string ystr = ((ListViewItem)y).Text;
				decimal dec_x;
				decimal dec_y;
				int returnVal;
				DateTime dat_x;
				DateTime dat_y;
				if (decimal.TryParse(xstr, out dec_x) && decimal.TryParse(ystr, out dec_y))
				{
					returnVal = decimal.Compare(dec_x, dec_y);
				}
				else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
				{
					returnVal = DateTime.Compare(dat_x, dat_y);
				}
				else
				{
					returnVal = string.Compare(xstr, ystr);
				}
				if (this._order == SortOrder.Descending)
				{
					returnVal *= -1;
				}
				return returnVal;
			}

			// Token: 0x0400001B RID: 27
			private int _col;

			// Token: 0x0400001C RID: 28
			private SortOrder _order;
		}

		// Token: 0x0200000B RID: 11
		private class ListViewItemComparerValue : IComparer
		{
			// Token: 0x0600003A RID: 58 RVA: 0x0000381C File Offset: 0x00001A1C
			public ListViewItemComparerValue()
			{
				this._col = 0;
				this._order = SortOrder.Ascending;
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00003835 File Offset: 0x00001A35
			public ListViewItemComparerValue(int col, SortOrder order)
			{
				this._col = col;
				this._order = order;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00003850 File Offset: 0x00001A50
			public int Compare(object x, object y)
			{
				string xstr = ((EXListViewItem)x).MyValue;
				string ystr = ((EXListViewItem)y).MyValue;
				decimal dec_x;
				decimal dec_y;
				int returnVal;
				DateTime dat_x;
				DateTime dat_y;
				if (decimal.TryParse(xstr, out dec_x) && decimal.TryParse(ystr, out dec_y))
				{
					returnVal = decimal.Compare(dec_x, dec_y);
				}
				else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
				{
					returnVal = DateTime.Compare(dat_x, dat_y);
				}
				else
				{
					returnVal = string.Compare(xstr, ystr);
				}
				if (this._order == SortOrder.Descending)
				{
					returnVal *= -1;
				}
				return returnVal;
			}

			// Token: 0x0400001D RID: 29
			private int _col;

			// Token: 0x0400001E RID: 30
			private SortOrder _order;
		}
	}
}
