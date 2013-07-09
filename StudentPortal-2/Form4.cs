using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentPortal_2
{
    public partial class Form4 : Form
    {
        string branch, sem;
        public Form4(string branch, string semester)
        {
            InitializeComponent();
            this.branch = branch;
            this.sem = semester;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (sem == "1" || sem == "2")
            {
                pictureBox1.ImageLocation = "C://Apps//Timetables//Common//" + branch + ".jpg";
            }
            else
            {
                pictureBox1.ImageLocation = "C://Apps//Timetables//" + branch + "//" + sem + ".jpg";
            }
            Image image = Image.FromFile(pictureBox1.ImageLocation);
            pictureBox1.Height = image.Height;
            pictureBox1.Width = image.Width;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}
