using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestUI.Common
{
    public static class CommonVoids
    {
        public static void ErrorMsg(string msg)
        {
            MessageBox.Show(msg, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
