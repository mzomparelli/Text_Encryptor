using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;


namespace Text_Encryptor
{
    public partial class frmMain : Form
    {

        StringBuilder TextData;
        public string FileName = "";


        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FileName = "";
            txtText.Text = "";
            txtText.Enabled = true;
            this.Text = "Text Encryptor - New File";

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Encrypted Text File (.enc)|*.enc";
            DialogResult result = dlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel || dlg.FileName == "")
            {
                FileName = "";
                dlg.Dispose();
                return;
            }

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                if(dlg.FileName != "")
                {
                    FileName = dlg.FileName;
                    LoadFile();
                    txtText.Enabled = true;
                }
            }
            
        }



        public void LoadFile()
        {
            if (File.Exists(FileName))
            {
                TextData = new StringBuilder();
                StreamReader oRead;
                oRead = File.OpenText(FileName);

                while (oRead.Peek() != -1)
                {
                    TextData.AppendLine(EncryptStrings.DecryptString(oRead.ReadLine()));
                }

                oRead.Close();
                oRead.Dispose();

                txtText.Text = TextData.ToString();
                txtText.Enabled = true;
                txtText.Select(0, 0);
                this.Text = "Text Encryptor - " + Path.GetFileNameWithoutExtension(FileName);
            }

        }

        private void WriteFile()
        {
            string[] delimiter = { Environment.NewLine };
            string[] text = txtText.Text.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

            StreamWriter oWrite;
            oWrite = File.CreateText(FileName);

            foreach (string s in text)
            {
                oWrite.WriteLine(EncryptStrings.EncryptString(s));
            }

            oWrite.Close();
            oWrite.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FileName == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.DefaultExt = ".enc";
                DialogResult result = dlg.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel || dlg.FileName == "")
                {
                    dlg.Dispose();
                    return;
                }

                FileName = dlg.FileName;
                WriteFile();
                this.Text = "Text Encryptor - " + Path.GetFileNameWithoutExtension(FileName);
            }
            else
            {

            tryAgain:
                try
                {
                    File.Delete(FileName);
                }
                catch (Exception ex)
                {
                    DialogResult result = MessageBox.Show("Cannot write to file." + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + "Try Again?", "Error", MessageBoxButtons.YesNo);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        goto tryAgain;
                    }
                                        
                }

            tryWriteAgain:
            try
            {
                WriteFile();
                this.Text = "Text Encryptor - " + Path.GetFileNameWithoutExtension(FileName);
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show("Cannot write to file." + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + "Try Again?", "Error", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    goto tryWriteAgain;
                }
               
            }


            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Text Encryptor v1.0.0" + Environment.NewLine + "© 2014 Michael Zomparelli" + Environment.NewLine + Environment.NewLine + "This app encrypts/decrypts text stored in a text file. Never is unencrypted text written to disk. Encryption/decryption takes place in memory and only encrypted values are written to disk.", "About Text Encryptor");
        }


    }


}
