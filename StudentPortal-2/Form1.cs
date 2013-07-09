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
using System.IO;
using System.Security.Cryptography;

namespace StudentPortal_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void button1_Click(object sender, EventArgs e)
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
            string username = textBox1.Text;
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            int y = 0;
            byte[] buffer = new byte[textBox2.Text.Length];
            foreach (char c in textBox2.Text.ToCharArray())
            {
                buffer[y] = (byte)c;
                y = y + 1;
            }
            string password = BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-", "");
            string sqlquery = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(sqlquery,cn);
            cmd.Connection = cn;
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            int check = 0;
            while (reader.Read())
            {
                if (textBox1.Text == (reader["Username"].ToString()) && password == (reader["Password"].ToString()))
                {
                    Form2 form2 = new Form2(textBox1.Text, password);
                    form2.StartPosition = FormStartPosition.CenterParent;
                    this.Hide();
                    form2.ShowDialog();
                    this.Close();
                    check = 1;
                }
            }
            if (check==0)
                {
                    MessageBox.Show("Sorry! Incorrect Credentials");
                    return;
                }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
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
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.LinkVisited = true;
            Form3 form3 = new Form3();
            form3.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            form3.ShowDialog();
            this.Close();
        }
    }
}