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
    public partial class Form4 : Form
    {
        Text A1;
        public Form4(Text x)
        {
            InitializeComponent();

            A1 = x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list = A1.counter_phones();
            DataTable table = new DataTable();
            table.Columns.Add("Номер телефона", typeof(string));
            for(int i = 0; i < list.Count; i++)
            {
                table.Rows.Add(list[i]);
            }
            dataGridView1.DataSource = table;
        }
    }
}
