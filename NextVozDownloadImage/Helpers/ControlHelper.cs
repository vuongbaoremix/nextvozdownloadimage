using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextVozDownloadImage.Helpers
{
    public class ControlHelper
    { 
        /// <summary>
        /// Call form control action from different thread
        /// </summary>
        public static void ControlInvoke(Control control, Action function)
        {
            if (control.IsDisposed || control.Disposing)
                return;

            if (control.InvokeRequired)
            {
                control.Invoke(new MethodInvoker(delegate { ControlInvoke(control, function); }));
                return;
            }
            function();
        }
    }
}
