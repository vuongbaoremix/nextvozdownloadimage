using EXControls;
using NextVozDownloadImage.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage
{
    public static class DownloadProcess
    {
        private static EXListView _lv;

        private static List<ProgressBar> _process = new List<ProgressBar>();


        public static void Init(EXListView lv)
        {
            _lv = lv;
        }


        public static void Create(int row)
        {
            _lv.Items.Clear();
            _process.ForEach(item =>
            {
                _lv.Controls.Remove(item);
            });

            _process.Clear();

            for (int i = 0; i < row; i++)
            {
                EXListViewItem lvi = new EXListViewItem(); 
                EXControlListViewSubItem cs = new EXControlListViewSubItem();


                ProgressBar pb = new ProgressBar();

                lvi.SubItems[0].Text = "Đang lấy link.";
                //lvi.SubItems.Add("");
                //lvi.SubItems.Add("0");
                //lvi.SubItems.Add("");
                //lvi.SubItems.Add("");

                _lv.Items.Add(lvi);
                 
                //var s = lvi.Bounds;

                //Rectangle r = lvi.SubItems[1].Bounds;
                //pb.SetBounds(r.X, r.Y - 2, r.Width, r.Height - 4);


                pb.Minimum = 0;
                pb.Maximum = 100;
                pb.Value = 0;
                pb.Step = 1;
                lvi.SubItems.Add(cs);
                _lv.AddControlToSubItem(pb, cs);

                // _lv.Controls.Add(pb);
                _process.Add(pb);
            }

        }

        public static void Update(int index, ImageInfo info)
        {

            ControlHelper.ControlInvoke(_lv, () =>
            {
                var lvItem = _lv.Items[index];
                lvItem.SubItems[0].Text = info.Url;
                //lvItem.SubItems[1].Text = info.Name;
                //lvItem.SubItems[2].Text = HumanReadable.BytesToString(info.Size);
                //lvItem.SubItems[3].Text = info.Extension;
            });
        }

        public static void UpdateProcess(int index, int value)
        {
            var process = _process[index];

            ControlHelper.ControlInvoke(process, () =>
            {
                process.Value = value;
            });
        }
    }
}
