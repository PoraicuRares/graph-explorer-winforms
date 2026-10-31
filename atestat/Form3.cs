using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atestat
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Form9 form = new Form9();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            form.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form10 form = new Form10();
            form.ShowDialog();
        }
    }
}
