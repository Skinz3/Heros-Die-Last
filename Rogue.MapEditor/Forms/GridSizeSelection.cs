using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rogue.MapEditor.Forms
{
    public partial class GridSizeSelection : Form
    {
        public bool Ok
        {
            get;
            private set;
        }
        public int MapWidth
        {
            get
            {
                return int.Parse(textBox1.Text);
            }
        }
        public int MapHeight
        {
            get
            {
                return int.Parse(textBox2.Text);
            }
        }
        public GridSizeSelection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ok = true;
            this.Close();
        }
    }
}
