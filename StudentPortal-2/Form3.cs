using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

namespace StudentPortal_2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        //public int checker = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string regex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Match match = Regex.Match(textBox2.Text,regex);
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || comboBox1.Text == "" || textBox8.Text == "" || textBox9.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Please Fill All Fields");
            }
            else if (textBox4.Text.Length<6)
            {
                MessageBox.Show("Your Password Should Be At Least 6 Characters Long");
            }
            else if (textBox4.Text != textBox5.Text)
            {
                MessageBox.Show("Passwords Do Not Match!");
            }
            else if (!match.Success)
            {
                MessageBox.Show("Please Enter A Valid Email ID");
            }
            else
            {
                string connection = @"Data Source=.\SQLEXPRESS;AttachDbFilename=""C:\Users\Sagnik\Documents\Visual Studio 2010\Projects\StudentPortal-2\StudentPortal-2\Database1.mdf"";Integrated Security=True;Connect Timeout=30;User Instance=True";
                SqlConnection cn = new SqlConnection(connection);
                try
                {
                    cn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Did Not Connect");
                }
                var name = textBox1.Text;
                var email = textBox2.Text;
                var user = textBox3.Text;
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                int y = 0;
                byte[] buffer = new byte[textBox4.Text.Length];
                foreach (char c in textBox4.Text.ToCharArray())
                {
                    buffer[y] = (byte)c;
                    y = y + 1;
                }
                string pass = BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-","");
                var branch = comboBox1.Text;
                var year = comboBox2.Text;
                var reg = textBox8.Text;
                var roll = textBox9.Text;
                var group = checkBox1.Checked?"A":"B";
                string query = "INSERT INTO Users (Username, Password, Name, Email, Branch, Year, RegNo, RollNo, [Group]) VALUES ('" + user + "', '" + pass + "', '" + name + "', '" + email + "', '" + branch + "', '" + year.ToString() + "', '" + reg + "', '" + roll + "', '" + group + "');";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                comboBox1.ResetText();
                textBox8.Text = "";
                textBox9.Text = "";
                checkBox1.CheckState = 0;
                checkBox2.CheckState = 0;
                MessageBox.Show("Thank You, Your Details Have Been Registered");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            form1.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            form1.ShowDialog();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            int y = Convert.ToInt32(DateTime.Now.Year.ToString());
            comboBox2.Items.Add((y-3).ToString());
            comboBox2.Items.Add((y-2).ToString());
            comboBox2.Items.Add((y-1).ToString());
            comboBox2.Items.Add(y.ToString());
        }
    }
}
