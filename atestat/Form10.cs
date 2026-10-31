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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cnt = 0; 
            if (radioButton2.Checked) cnt++;
            if (radioButton5.Checked) cnt++;
            if (radioButton10.Checked) cnt++;
            if (radioButton15.Checked) cnt++;
            if (radioButton19.Checked) cnt++;
            MessageBox.Show("Ai raspuns corect la " + cnt + " intrebari!");
        }


    }
}
