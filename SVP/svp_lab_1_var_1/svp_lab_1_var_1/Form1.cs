using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace svp_lab_1_var_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void save()
        {
           
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Name = saveFileDialog1.FileName;
                File.WriteAllText(Name, richTextBox1.Text);
            }
        }

        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e) // об авторе
        {
            Form5 f5 = new Form5();
            f5.Owner = this;
            f5.ShowDialog();
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e) // новый
        {
            анализТекстаToolStripMenuItem.Visible = true;
            richTextBox1.Visible = true;
            button1.Visible = true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e) // открыть
        {
            анализТекстаToolStripMenuItem.Visible = true;
            richTextBox1.Visible = true;
            button1.Visible = true;         
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Name = openFileDialog1.FileName;
                richTextBox1.Clear();
                richTextBox1.Text = File.ReadAllText(Name);
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e) // сохранить
        {
            save();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e) // выход
        {
            Close();
        }

        private void количествоСловToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text A = new Text(richTextBox1);
            Form2 f2 = new Form2(A);
            f2.Owner = this;
            f2.ShowDialog();
        }

        private void количествоПредложенийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text A = new Text(richTextBox1);
            Form3 f3 = new Form3(A);
            f3.Owner = this;
            f3.ShowDialog();
        }

        private void выборТелефоновToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text A = new Text(richTextBox1);
            Form4 f4 = new Form4(A);
            f4.Owner = this;
            f4.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save();
        }
    }
}