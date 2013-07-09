using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace StudentPortal_2
{
    public partial class Form2 : Form
    {
        string user, pass;
        public Form2(string user, string pass)
        {
            InitializeComponent();
            this.user = user;
            this.pass = pass;
        }

        private void Form2_Load(object sender, EventArgs e)
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
                if ((reader["Username"].ToString() == user) && (reader["Password"].ToString() == pass))
                {
                    int year = Convert.ToInt32(reader["Year"].ToString());
                    int y = (DateTime.Now.Year - year);
                    int sem;
                    if (DateTime.Now.Month >= 8)
                        sem = (y * 2) + 1;
                    else
                        sem = y * 2;
                    label4.Text = reader["Email"].ToString();
                    label13.Text = sem.ToString();
                    label14.Text = reader["RegNo"].ToString();
                    label15.Text = reader["RollNo"].ToString();
                    label17.Text = reader["Group"].ToString();
                    label18.Text = reader["Name"].ToString();
                    label12.Text = reader["Branch"].ToString();
                }
            }
            reader.Close();
            var a = label13.Text;
            var group = label17.Text;
            List<string> l = new List<string>();
            if(a=="1" || a=="2")
            {
                if((a=="1" && group=="A") || (a=="2" && group=="B"))
                {
                    query = "SELECT * FROM Common WHERE [Group]='A'";
                    cmd = new SqlCommand(query, cn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        l = reader["Subjects"].ToString().Split(',').ToList();
                        label10.Text = l[0];
                        label16.Text = l[1];
                        label19.Text = l[2];
                        label20.Text = l[3];
                        label21.Text = l[4];
                        label22.Text = l[5];
                        label23.Text = l[6];
                        label24.Text = l[7];
                        label25.Text = l[8];
                        label26.Text = l[9];
                    }
                    reader.Close();
                }
                else
                {
                    query = "SELECT * FROM Common WHERE [Group]='B'";
                    cmd = new SqlCommand(query, cn);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        l = reader["Subjects"].ToString().Split(',').ToList();
                        label10.Text = l[0];
                        label16.Text = l[1];
                        label19.Text = l[2];
                        label20.Text = l[3];
                        label21.Text = l[4];
                        label22.Text = l[5];
                        label23.Text = l[6];
                        label24.Text = l[7];
                        label25.Text = l[8];
                        label26.Text = l[9];
                    }
                    reader.Close();
                }
            }
            else
            {
                query = "SELECT * FROM " + label12.Text + " WHERE Semester=" + label13.Text + ";";
                cmd = new SqlCommand(query, cn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    l = reader["Subjects"].ToString().Split(',').ToList();
                    label10.Text = l[0];
                    label16.Text = l[1];
                    label19.Text = l[2];
                    label20.Text = l[3];
                    label21.Text = l[4];
                    label22.Text = l[5];
                    label23.Text = l[6];
                    label24.Text = l[7];
                    label25.Text = l[8];
                    label26.Text = l[9];
                }
                reader.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string branch;
            if (label13.Text == "1" || label13.Text == "2")
            {
                branch = label17.Text;
            }
            else
            {
                branch = label12.Text;
            }
            Form4 form = new Form4(branch, label13.Text);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form5 form = new Form5(user, pass);
            form.StartPosition = FormStartPosition.CenterParent;
            this.Hide();
            form.ShowDialog();
            this.Close();
        }

    }
}
