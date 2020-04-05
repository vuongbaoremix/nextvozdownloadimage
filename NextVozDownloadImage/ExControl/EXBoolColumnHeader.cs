using System;
using System.Drawing;

namespace EXControls
{
	// Token: 0x0200000E RID: 14
	public class EXBoolColumnHeader : EXColumnHeader
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000039E8 File Offset: 0x00001BE8
		public EXBoolColumnHeader()
		{
			this.init();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000039FA File Offset: 0x00001BFA
		public EXBoolColumnHeader(string text)
		{
			this.init();
			base.Text = text;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003A14 File Offset: 0x00001C14
		public EXBoolColumnHeader(string text, int width)
		{
			this.init();
			base.Text = text;
			base.Width = width;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003A36 File Offset: 0x00001C36
		public EXBoolColumnHeader(string text, Image trueimage, Image falseimage)
		{
			this.init();
			base.Text = text;
			this._trueimage = trueimage;
			this._falseimage = falseimage;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003A5E File Offset: 0x00001C5E
		public EXBoolColumnHeader(string text, Image trueimage, Image falseimage, int width)
		{
			this.init();
			base.Text = text;
			this._trueimage = trueimage;
			this._falseimage = falseimage;
			base.Width = width;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003A8F File Offset: 0x00001C8F
		private void init()
		{
			this._editable = false;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003A9C File Offset: 0x00001C9C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public Image TrueImage
		{
			get
			{
				return this._trueimage;
			}
			set
			{
				this._trueimage = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003AC0 File Offset: 0x00001CC0
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public Image FalseImage
		{
			get
			{
				return this._falseimage;
			}
			set
			{
				this._falseimage = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003AE4 File Offset: 0x00001CE4
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003AFC File Offset: 0x00001CFC
		public bool Editable
		{
			get
			{
				return this._editable;
			}
			set
			{
				this._editable = value;
			}
		}

		// Token: 0x04000020 RID: 32
		private Image _trueimage;

		// Token: 0x04000021 RID: 33
		private Image _falseimage;

		// Token: 0x04000022 RID: 34
		private bool _editable;
	}
}
