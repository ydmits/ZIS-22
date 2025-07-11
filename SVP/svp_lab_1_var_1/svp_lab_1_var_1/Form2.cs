using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace svp_lab_1_var_1
{
    public partial class Form2 : Form
    {
        Text A1;
        public Form2(Text x)
        {
            InitializeComponent();
            
            A1 = x;
        }
        
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            textBox2.Text = A1.counter_words(textBox1);
            textBox2.Show();
        }
    }
}
