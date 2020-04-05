using System;
using System.Windows.Forms;

namespace EXControls
{
	// Token: 0x0200000D RID: 13
	public class EXEditableColumnHeader : EXColumnHeader
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003931 File Offset: 0x00001B31
		public EXEditableColumnHeader()
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000393C File Offset: 0x00001B3C
		public EXEditableColumnHeader(string text)
		{
			base.Text = text;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000394F File Offset: 0x00001B4F
		public EXEditableColumnHeader(string text, int width)
		{
			base.Text = text;
			base.Width = width;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000396A File Offset: 0x00001B6A
		public EXEditableColumnHeader(string text, Control control)
		{
			base.Text = text;
			this.MyControl = control;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003985 File Offset: 0x00001B85
		public EXEditableColumnHeader(string text, Control control, int width)
		{
			base.Text = text;
			this.MyControl = control;
			base.Width = width;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000039A8 File Offset: 0x00001BA8
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000039C0 File Offset: 0x00001BC0
		public Control MyControl
		{
			get
			{
				return this._control;
			}
			set
			{
				this._control = value;
				this._control.Visible = false;
				this._control.Tag = "not_init";
			}
		}

		// Token: 0x0400001F RID: 31
		private Control _control;
	}
}
