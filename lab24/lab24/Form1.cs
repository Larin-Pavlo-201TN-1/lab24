using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace lab24
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            thread1 = new Thread(new ThreadStart(Txt_RC2));
            thread2 = new Thread(new ThreadStart(Txt_MD5));
            thread3 = new Thread(new ThreadStart(Rnd_num));
        }
        Thread thread1;
        Thread thread2;
        Thread thread3;

        private void Txt_RC2()
        {
            try
            {
                RC2 rc2 = RC2.Create();
                ICryptoTransform encryptor = rc2.CreateEncryptor(rc2.Key, rc2.IV);
                string text = "sd asdasdasd";
                byte[] toEncrypt = Encoding.UTF8.GetBytes(text);
                ParallelLoopResult parallelLoopResult = Parallel.For(0, 100, i =>
                {
                    richTextBox2.Invoke((MethodInvoker)delegate ()
                    {
                        richTextBox2.Text = "";
                        for (int j = 0; j < toEncrypt.Length; j++)
                        {
                            richTextBox2.Text += toEncrypt[j].ToString();
                        }
                    });
                });
            }
            catch (Exception) { }
        }

        private void Txt_MD5()
        {
            try
            {
                MD5 md5 = MD5.Create();
                string text = "sd asdasdasd";
                byte[] toEncrypt = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = md5.ComputeHash(toEncrypt);
                ParallelLoopResult parallelLoopResult = Parallel.For(0, 100, i =>
                {
                    richTextBox2.Invoke((MethodInvoker)delegate ()
                    {
                        richTextBox1.Text = "";
                        for (int j = 0; j < hashBytes.Length; j++)
                        {
                            richTextBox1.Text += hashBytes[j].ToString();
                        }
                    });
                });
            }
            catch (Exception) { }
        }

        private void Rnd_num()
        {
            try
            {
                ASCIIEncoding ByteConverter = new ASCIIEncoding();
                string dataString = "sd asdasdasd";
                byte[] originalData = ByteConverter.GetBytes(dataString);
                byte[] signedData;
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
                RSAParameters Key = RSAalg.ExportParameters(true);
                signedData = RSAalg.SignData(originalData, SHA256.Create());
                ParallelLoopResult parallelLoopResult = Parallel.For(0, 100, i =>
                {
                    richTextBox2.Invoke((MethodInvoker)delegate ()
                    {
                        richTextBox3.Text = "";
                        for (int j = 0; j < signedData.Length; j++)
                        {
                            richTextBox3.Text += signedData[j].ToString();
                        }
                    });
                });
            }
            catch (Exception) {}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thread1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            thread2.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            thread3.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            thread1.Suspend();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            thread2.Suspend();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            thread3.Suspend();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            thread1.Suspend();
            thread2.Suspend();
            thread3.Suspend();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread1.Abort();
            thread2.Abort();
            thread3.Abort();
        }
    }
}
