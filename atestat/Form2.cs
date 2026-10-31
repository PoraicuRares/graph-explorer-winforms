using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace atestat
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form5 form = new Form5();
            form.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.ShowDialog();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Form7 form = new Form7();
            form.ShowDialog();
        }
    }

       
}
    

