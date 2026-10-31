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
    public partial class Form1 : Form
    {
        private Color originalBorderColor = Color.Transparent;
        public Form1()
        {
            InitializeComponent();
            efect(pictureBox1);
            efect(pictureBox2);
         
        }
      
        
        private void efect(PictureBox pb)
        {
            pb.BorderStyle = BorderStyle.None;
            pb.BackColor = Color.Transparent;

            pb.MouseEnter += PictureBox_HoverEnter;
            pb.MouseLeave += PictureBox_HoverLeave;
        }
        private void PictureBox_HoverEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender; 
            
            pb.BorderStyle = BorderStyle.Fixed3D;
            pb.BackColor = Color.FromArgb(240, 240, 240);

            var shadow = new Panel
            {
                Size = new Size(pb.Width + 6, pb.Height + 6),
                Location = new Point(pb.Left - 3, pb.Top - 3),
                BackColor = Color.FromArgb(100, 0, 0, 0)
            };

            this.Controls.Add(shadow);
            shadow.SendToBack();
            pb.BringToFront();
            pb.Tag = shadow;

        }

        private void PictureBox_HoverLeave(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            pb.BorderStyle = BorderStyle.None;
            pb.BackColor = originalBorderColor;

            if (pb.Tag is Panel shadow)
            {
                this.Controls.Remove(shadow);
                shadow.Dispose();
                pb.Tag = null;
            }
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Form3 form = new Form3();
            form.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
} 
