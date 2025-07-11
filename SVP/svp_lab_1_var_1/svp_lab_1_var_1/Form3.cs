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
    public partial class Form3 : Form
    {
        Text A1;
        public Form3(Text x)
        {
            InitializeComponent();
            A1 = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = A1.counter_quote();
            textBox1.Show();
        }
    }
}
