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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            if (!checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked && !checkBox4.Checked) cnt++;
            if (radioButton2.Checked) cnt++;
            if (radioButton7.Checked) cnt++;
            if (radioButton10.Checked) cnt++;
            if (radioButton16.Checked) cnt++;
            MessageBox.Show("Ai raspuns corect la " + cnt + " intrebari!");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
