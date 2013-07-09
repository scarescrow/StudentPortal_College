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
    public partial class Form5 : Form
    {
        string user, pass, email, roll;
        public Form5(string user,string pass)
        {
            InitializeComponent();
            this.user = user;
            this.pass = pass;
        }

        private void Form5_Load(object sender, EventArgs e)
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
            string query = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Connection = cn;
            SqlDataReader reader = null;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader["Username"].ToString() == user && reader["Password"].ToString() == pass)
                {
                    textBox1.Text = reader["Email"].ToString();
                    label16.Text = reader["Branch"].ToString();
                    textBox3.Text = reader["RollNo"].ToString();
                    label11.Text = reader["Name"].ToString();
                    label12.Text = reader["Username"].ToString();
                    label13.Text = reader["Year"].ToString();
                    label14.Text = reader["RegNo"].ToString();
                    label15.Text = reader["Group"].ToString();
                }
            }
            reader.Close();
            email = textBox1.Text;
            roll = textBox3.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2(user, pass);
            form.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            form.ShowDialog();
            this.Close();
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
            string query;
            SqlCommand cmd;
            if (textBox1.Text != email && textBox1.Text!="")
            {
                string regex = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
                Match match = Regex.Match(textBox1.Text, regex);
                if (!match.Success)
                {
                    MessageBox.Show("Please Enter A Valid Email Address");
                    return;
                }
                else
                {
                    query = "UPDATE users SET Email='" + textBox1.Text + "' WHERE Username='" + user + "' AND Password='" + pass + "';";
                    cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                }
            }
            if (textBox3.Text != roll && textBox3.Text!="")
            {
                query = "UPDATE users SET RollNo='" + textBox3.Text + "' WHERE Username='" + user + "' AND Password='" + pass + "';";
                cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
            }
            if (textBox4.Text != "")
            {
                if (textBox4.Text != textBox5.Text)
                {
                    MessageBox.Show("Passwords Do Not Match");
                    return;
                }
                else
                {
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    int y = 0;
                    byte[] buffer = new byte[textBox4.Text.Length];
                    foreach (char c in textBox4.Text.ToCharArray())
                    {
                        buffer[y] = (byte)c;
                        y = y + 1;
                    }
                    string pwd = BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-", "");
                    query = "UPDATE users SET Password='" + pwd + "' WHERE Username='" + user + "' AND Password='" + pass + "';";
                    cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    pass = pwd;
                }
            }
            MessageBox.Show("Your Changes Have Been Successfully Saved");
            Form2 form = new Form2(user, pass);
            form.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            form.ShowDialog();
            this.Close();
        }
    }
}
