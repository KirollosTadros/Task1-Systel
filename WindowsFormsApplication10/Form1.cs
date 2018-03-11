using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication10
{
    public partial class Form1 : Form
    {

        public string constring = "Data Source=KIRO\\SQLEXPRESS;Initial Catalog=Connection;Integrated Security=True";


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message =
                "Are you sure that you would like to close?";
            const string caption = "Systel Closing";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {
                // cancel the closure of the form.
                e.Cancel = true;
            }
        }

        //Starts Group Devices form

        private void button1_Click(object sender, EventArgs e)  
        {
            new Form2(constring).Show();
        }

        //Starts Users Form

        private void button2_Click(object sender, EventArgs e)
        {
            new Form3(constring).Show();
        }

        //Starts Groups Form

        private void button3_Click(object sender, EventArgs e)
        {
            new Form4(constring).Show();
        }
    }
}
