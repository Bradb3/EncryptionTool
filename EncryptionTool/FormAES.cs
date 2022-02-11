// ©Bradley Bellinger USF U44306345

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;


namespace EncryptionTool
{
    public partial class FormAES : Form
    {
        Aes aes = Aes.Create();

        public FormAES()
        {
            InitializeComponent();
        }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
           
                if (plainText == null || plainText.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (Key == null || Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (IV == null || IV.Length <= 0)
                    throw new ArgumentNullException("IV");
               
                
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
               
                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
            {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    
private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GenerateKeyIv_Click(object sender, EventArgs e)
        {
            aes.GenerateKey();
            aes.GenerateIV();
            textBox1.Text = Convert.ToBase64String(aes.Key);
            textBox2.Text = Convert.ToBase64String(aes.IV);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void encrypt_click(object sender, EventArgs e)
        {
            if ((textBox1.Text != null && textBox1.Text.Length > 0) & (textBox2.Text != null && textBox2.Text.Length > 0) & (richTextBox1.Text != null && richTextBox1.TextLength > 0))
            {
                byte[] boxkey = Convert.FromBase64String(textBox1.Text);
                byte[] boxIV = Convert.FromBase64String(textBox2.Text);
                richTextBox2.Text = Convert.ToBase64String(EncryptStringToBytes_Aes(richTextBox1.Text, boxkey, boxIV)); 
            }
            else MessageBox.Show("You are missing something","Error");
        }

        private void decrypt_click(object sender, EventArgs e)
        {
            if ((textBox1.Text != null && textBox1.Text.Length > 0) & (textBox2.Text != null && textBox2.Text.Length > 0) & (richTextBox2.Text != null && richTextBox2.TextLength > 0))
            {
                byte[] boxkey = Convert.FromBase64String(textBox1.Text);
                byte[] boxIV = Convert.FromBase64String(textBox2.Text);
                byte[] cyphertxt = Convert.FromBase64String(richTextBox2.Text);
                richTextBox1.Text = DecryptStringFromBytes_Aes(cyphertxt, boxkey, boxIV);
            }
            else MessageBox.Show("You are missing something","Error");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormAES_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void RsaToolBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormRSA()).ShowDialog();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormHash()).ShowDialog();
            this.Close();
        }
    }
}
