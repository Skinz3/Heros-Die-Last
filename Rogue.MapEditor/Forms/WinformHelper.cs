using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Rogue.MapEditor.Forms
{
    class WinformHelper
    {
        public static void EnableStyle()
        {
            Application.EnableVisualStyles();
        }

        public static void ShowMessage(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
