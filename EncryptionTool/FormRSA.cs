// ©Bradley Bellinger USF U44306345

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace EncryptionTool
{
    public partial class FormRSA : Form
    {

        
        public FormRSA()
        {
            InitializeComponent();
        }

        private void FormRSA_Load(object sender, EventArgs e)
        {
            string[] keysizes = new string[] { "1024", "2048", "4096", "8192" };
            comboBox1.Items.AddRange(keysizes);
            comboBox1.SelectedIndex = 0;
            label7.Text = "Max input Key Size supports: 117 Characters";
            label8.Text = "Encrypt Character Count: 0";
            label9.MaximumSize = new Size(100, 0);
            label9.AutoSize = true;
        }

        private void Generatekeys_click(object sender, EventArgs e)
        {
            int cbox = int.Parse(comboBox1.SelectedItem.ToString());
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(cbox);
            richTextBox1.Text = Convert.ToBase64String(RSA.ExportRSAPublicKey());
            richTextBox2.Text = Convert.ToBase64String(RSA.ExportRSAPrivateKey());
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Decrypt_Click(object sender, EventArgs e)
        {

            try
            {
                Convert.FromBase64String(richTextBox3.Text);
                var publicKey = Convert.FromBase64String(richTextBox1.Text);
                var privateKey = Convert.FromBase64String(richTextBox2.Text);
                RSA rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(privateKey, out int keyLength);
                var texttobdecrypt = Convert.FromBase64String(richTextBox3.Text);
                var temp = rsa.Decrypt(texttobdecrypt, RSAEncryptionPadding.Pkcs1);
                richTextBox4.Text = System.Text.Encoding.UTF8.GetString(temp);
            }
            catch (Exception)
            {
                MessageBox.Show("Input not base64 String","Error");
            }

        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            try
            {
                var publicKey = Convert.FromBase64String(richTextBox1.Text);
                var privateKey = Convert.FromBase64String(richTextBox2.Text);
                RSA rsa = RSA.Create();
                rsa.ImportRSAPublicKey(publicKey, out int keyLength);
                var texttobencoded = System.Text.Encoding.UTF8.GetBytes(richTextBox3.Text);
                var temp = rsa.Encrypt(texttobencoded, RSAEncryptionPadding.Pkcs1);
                richTextBox4.Text = Convert.ToBase64String(temp);
            }
            catch (Exception)
            {
                MessageBox.Show("Bad/No Input/ Bad/No Keys/ Wrong Size Keys","Error");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormAES()).ShowDialog();
            this.Close();
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {
            int len = richTextBox3.Text.Length;
            label8.Text = "Encrypt Character Count: " + len;
            int Selectedindex = comboBox1.SelectedIndex;
            if (Selectedindex == 0)  
            {
                if(len > 117) 
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;
            }
            if (Selectedindex == 1)
            {
                if (len > 245)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;

            }
            if (Selectedindex == 2)
            {
                if (len > 501)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;

            }
            if (Selectedindex == 3)
            {
                if (len > 1013)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = richTextBox4.Text;
            richTextBox4.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int len = richTextBox3.Text.Length;
            if (comboBox1.SelectedIndex == 0)
            {
                label7.Text = "Max input Key Size supports: 117 Characters";
                if (len > 117)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                
                label7.Text = "Max input Key Size supports: 245 Characters";
                if (len > 245)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;
            }
           
            if (comboBox1.SelectedIndex == 2)
            {
                label7.Text = "Max input Key Size supports: 501 Characters";
                if (len > 501)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                label7.Text = "Max input Key Size supports: 1013 Characters";
                if (len > 1013)
                {
                    label8.ForeColor = Color.Red;
                }
                else label8.ForeColor = Color.Green;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
            richTextBox4.Text = "";
        }      

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormHash()).ShowDialog();
            this.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                var rsa = new RSACryptoServiceProvider();
                var privateKey = Convert.FromBase64String(richTextBox2.Text);
                byte[] messageB4 = System.Text.Encoding.UTF8.GetBytes(richTextBox3.Text);
                rsa.ImportRSAPrivateKey(privateKey, out int keyLength);
                byte[] sgndmessage = rsa.SignData(messageB4, CryptoConfig.MapNameToOID("SHA256"));
                richTextBox4.Text = Convert.ToBase64String(sgndmessage);
                button11.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("No/Bad Key", "Error");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                var rsa = new RSACryptoServiceProvider();
                var publickey = Convert.FromBase64String(richTextBox1.Text);
                byte[] messageB4 = System.Text.Encoding.UTF8.GetBytes(richTextBox3.Text);
                byte[] messagesigned = Convert.FromBase64String(richTextBox4.Text);
                rsa.ImportRSAPublicKey(publickey, out int keyLength);
                bool verified = rsa.VerifyData(messageB4, CryptoConfig.MapNameToOID("SHA256"), messagesigned);
                if (verified == true)
                {
                    MessageBox.Show("Signiture & Message Verified! ", "Success");
                }
                if (verified == false)
                {
                    MessageBox.Show("Fail", "Error");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No/Bad Key", "Error");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }
    }
}
