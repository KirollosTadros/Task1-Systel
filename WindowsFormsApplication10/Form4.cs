using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Permissions;

namespace WindowsFormsApplication10
{
    public partial class Form4 : Form
    {

        public SqlConnection con;
        public SqlDataAdapter ad;
        public DataTable tb = new DataTable();
        bool DataDirty;
        public SqlCommandBuilder bld;
        public Form4(string constring)
        {
            InitializeComponent();
            con = new SqlConnection(constring);
            DataDirty = false;
            disp_data();
          
        }

        public void disp_data()
        {
            con.Open();
            ad = new SqlDataAdapter("select * from Groups", con);
            tb = new DataTable();
            ad.Fill(tb);
            dataGridView1.DataSource = tb;
            con.Close();
        }

        public void clear_all()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter )
            {
                button3_Click(sender, e);
            }

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow gridrow = dataGridView1.Rows[e.RowIndex];
            DataRowView rowview = (DataRowView)gridrow.DataBoundItem;
            DataRow row = rowview.Row;
            if (row.RowState != DataRowState.Unchanged || dataGridView1.IsCurrentRowDirty)
            {
                DataDirty = true;
            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {

           
            if (DataDirty)
            {
                const string message =
                    "Do you want to save changes before exit?";
                const string caption = "Groups Closing";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);
             

                // If the no button was pressed ...
                if (result == DialogResult.No)
                {
                    // cancel the closure of the form.
                    e.Cancel = false;
                }
                else
                {
                    try
                    {

                        con.Open();
                        bld = new SqlCommandBuilder(ad);
                        ad.Update(tb);
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("Something went wrong your edit won't be saved");
                        con.Close();
                        DataDirty = false;
                        disp_data();
                    }

                }
            }
        }

  


    
        private void button1_Click(object sender, EventArgs e)
        {
            clear_all();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                bld = new SqlCommandBuilder(ad);
                ad.Update(tb);
                con.Close();
                DataDirty = false;
                MessageBox.Show("Updated ");
            }

            catch (SqlException ex)
            {

                MessageBox.Show("Something went wrong your update won't be saved");
                con.Close();
                DataDirty = false;
                disp_data();
            }
        }

     
        private void button3_Click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand("Select * from [Groups] where ID='" + textBox1.Text + "'", con);
            SqlDataAdapter dd = new SqlDataAdapter(command);
            dd.Fill(ds);
            int i = ds.Tables[0].Rows.Count;

            if (textBox1.Text == "" )
                MessageBox.Show("ID Must has value before you insert");
            else if(i>0){
                MessageBox.Show("This ID alreayd exist");
            }
            
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Groups values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
                clear_all();
                MessageBox.Show("Insereted");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView data = new DataView(tb);
            data.RowFilter = "Convert(ID,System.String) LIKE '%" + textBox1.Text + "%' AND " + "groupname LIKE '%" + textBox2.Text + "%'AND " + "Convert(allowedlogin,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(login,System.String) LIKE '%" + textBox4.Text + "%'AND " + "name LIKE '%" + textBox5.Text + "%'AND " + "Description LIKE '%" + textBox6.Text + "%'";
            dataGridView1.DataSource = data;
             if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" )
                disp_data();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            DataView data = new DataView(tb);
            data.RowFilter = "Convert(ID,System.String) LIKE '%" + textBox1.Text + "%' AND " + "groupname LIKE '%" + textBox2.Text + "%'AND " + "Convert(allowedlogin,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(login,System.String) LIKE '%" + textBox4.Text + "%'AND " + "name LIKE '%" + textBox5.Text + "%'AND " + "Description LIKE '%" + textBox6.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
                disp_data();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            DataView data = new DataView(tb);
            data.RowFilter = "Convert(ID,System.String) LIKE '%" + textBox1.Text + "%' AND " + "groupname LIKE '%" + textBox2.Text + "%'AND " + "Convert(allowedlogin,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(login,System.String) LIKE '%" + textBox4.Text + "%'AND " + "name LIKE '%" + textBox5.Text + "%'AND " + "Description LIKE '%" + textBox6.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
                disp_data();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView data = new DataView(tb);
            data.RowFilter = "Convert(ID,System.String) LIKE '%" + textBox1.Text + "%' AND " + "groupname LIKE '%" + textBox2.Text + "%'AND " + "Convert(allowedlogin,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(login,System.String) LIKE '%" + textBox4.Text + "%'AND " + "name LIKE '%" + textBox5.Text + "%'AND " + "Description LIKE '%" + textBox6.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
                disp_data();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DataView data = new DataView(tb);
            data.RowFilter = "Convert(ID,System.String) LIKE '%" + textBox1.Text + "%' AND " + "groupname LIKE '%" + textBox2.Text + "%'AND " + "Convert(allowedlogin,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(login,System.String) LIKE '%" + textBox4.Text + "%'AND " + "name LIKE '%" + textBox5.Text + "%'AND " + "Description LIKE '%" + textBox6.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
                disp_data();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            DataView data = new DataView(tb);
            data.RowFilter = "Convert(ID,System.String) LIKE '%" + textBox1.Text + "%' AND " + "groupname LIKE '%" + textBox2.Text + "%'AND " + "Convert(allowedlogin,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(login,System.String) LIKE '%" + textBox4.Text + "%'AND " + "name LIKE '%" + textBox5.Text + "%'AND " + "Description LIKE '%" + textBox6.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "")
                disp_data();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            const string message =
                  "Are you sure you want to delete?";
            const string caption = "Delete";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            // If the no button was pressed ...
            if (result == DialogResult.No)
            {

            }
            else
            {
                int rowindex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(rowindex);
                bld = new SqlCommandBuilder(ad);
                try
                {
                    ad.Update(tb);
                }
                catch (SqlException ex)
                {

                    MessageBox.Show("Something went wrong your update won't be saved");
                    con.Close();
                    DataDirty = false;
                    disp_data();
                }
            }

        }

   

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            string search = tb.Rows[rowindex][0].ToString();
          //  MessageBox.Show(search);
            DataTable dt = new DataTable();
            DataView data = new DataView(dt);
            SqlCommand command = new SqlCommand("Select * from [Users] where groupid='" + search + "'", con);
            SqlDataAdapter dd = new SqlDataAdapter(command);
            dd.Fill(dt);
            //
            //data.RowFilter = "Convert(ID,System.String) LIKE '%" +search + "%'";
            //dataGridView1.DataSource = tb;

               new Displays(dt).Show();
        }

    }
}
