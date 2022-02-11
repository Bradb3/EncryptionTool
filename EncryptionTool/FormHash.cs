// ©Bradley Bellinger USF U44306345

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EncryptionTool
{
    public partial class FormHash : Form
    {
        public FormHash()
        {
            InitializeComponent();
        }

        private void Hash_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] message = System.Text.Encoding.ASCII.GetBytes(richTextBox1.Text);
                byte[] md5message = md5.ComputeHash(message);
                textBox1.Text = Convert.ToBase64String(md5message);
            }
            if (radioButton2.Checked)
            {
                System.Security.Cryptography.SHA256 md5 = System.Security.Cryptography.SHA256.Create();
                byte[] message = System.Text.Encoding.ASCII.GetBytes(richTextBox1.Text);
                byte[] sha256message = md5.ComputeHash(message);
                textBox1.Text = Convert.ToBase64String(sha256message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormAES()).ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormRSA()).ShowDialog();
            this.Close();
        }

        private void FormHash_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                radioButton2.Checked = false;

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;

            }
        }
    }
}
