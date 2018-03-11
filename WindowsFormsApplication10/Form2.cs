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

namespace WindowsFormsApplication10
{

    public partial class Form2 : Form
    {
        public SqlConnection con;
        public SqlDataAdapter ad;
        public DataTable tb = new DataTable();
        bool DataDirty;
        public SqlCommandBuilder bld;

        //Displays data in the datagirdview
      
        public void disp_data()
        {
            con.Open();
            ad = new SqlDataAdapter("select * from groupDevices", con);
            tb = new DataTable();
            ad.Fill(tb);
            dataGridView1.DataSource = tb; 
            con.Close();  
           

        }
          
        
        public Form2(string constring)
        {
            InitializeComponent();

            con = new SqlConnection(constring);
            ad = new SqlDataAdapter("select * from groupDevices", con);

             DataDirty = false;     //if any changes is done and not saved this will turns into true
            disp_data();
        }


        private void button1_Click(object sender, EventArgs e) //Clear Button
        {
           clear_all();
        }
      
      
       //Returns all text boxes empty

        public void clear_all()
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        //Enter key in any text box calls the method of insert key to insert using enter

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter )
            {
                button3_Click(sender, e);
            }

        }

        //Turns the dirty datadirty into true if there any change occured

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

        //ask for saving changes if there is any changes

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {


            if (DataDirty)
            {
                const string message =
                    "Do you want to save changes before exit?";
                const string caption = "Group Devices Closing";
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




        //Update Button to save any changes after editing the table

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

        //Insert button to insert data

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand("Select * from [groupDevices] where deviceid='"+textBox1.Text+"'", con);
            SqlDataAdapter dd = new SqlDataAdapter(command);
            dd.Fill(ds);
            int i = ds.Tables[0].Rows.Count;

            //Checking if any textbox from the necessary text boxes is empty
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "")
                MessageBox.Show("(Device Id, Device Name, Group id, City code, Plate Number, Mode id) Must all have value before you insert");
            else if(i>0){
                MessageBox.Show("This Device id already exists");
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into groupDevices values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                disp_data();
                clear_all();
                MessageBox.Show("Insereted");
            }
  
        }

        //delete button

        private void button4_Click(object sender, EventArgs e)
        {
           
            const string message =
                  "Are you sure you want to delete?";
            const string caption = "Deleting";
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
                ad.Update(tb);
            }
            
        }

       
            //all text boxes acts as search when text in it is changed

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

            DataView data = new DataView(tb);

            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";
            dataGridView1.DataSource = data;

            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            DataView data = new DataView(tb);
            //data.RowFilter = string.Format("DeviceName LIKE '%{0}%'", textBox2.Text);
            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
            DataView data = new DataView(tb);
            //data.RowFilter = string.Format("Convert(Groupid,System.String) LIKE '%{0}%'AND Convert(platenumber,System.String) LIKE '%{0}%'", textBox3.Text,textBox4.Text);
            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";
            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
            DataView data = new DataView(tb);
          //  data.RowFilter = string.Format("Convert(Citycode,System.String) LIKE '%{0}%'", textBox5.Text);
            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";

            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

         
            DataView data = new DataView(tb);
            //data.RowFilter = string.Format("Convert(Groupid,System.String) LIKE '%{0}%'AND Convert(platenumber,System.String) LIKE '%{0}%'", textBox3.Text, textBox4.Text);

           // data.RowFilter = string.Format("Convert(Platenumber,System.String) LIKE '%{0}%'", textBox4.Text);
            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";

            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
          
            DataView data = new DataView(tb);
            //data.RowFilter = string.Format("Convert(Modemid,System.String) LIKE '%{0}%'", textBox6.Text);
            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";

            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        
            DataView data = new DataView(tb);
            //data.RowFilter = string.Format("Convert(Ip,System.String) LIKE '%{0}%'", textBox7.Text);
            data.RowFilter = "Convert(Deviceid,System.String) LIKE '%" + textBox1.Text + "%' AND " + "DeviceName LIKE '%" + textBox2.Text + "%'AND " + "Convert(Groupid,System.String) LIKE '%" + textBox3.Text + "%'AND " + "Convert(Citycode,System.String) LIKE '%" + textBox4.Text + "%'AND " + "Convert(PlateNumber,System.String) LIKE '%" + textBox5.Text + "%'AND " + "Convert(Modemid,System.String) LIKE '%" + textBox6.Text + "%'AND " + "Convert(ip,System.String) LIKE '%" + textBox7.Text + "%'";

            dataGridView1.DataSource = data;
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == "" && textBox6.Text == "" && textBox7.Text == "")
                disp_data();

        }

     

      

        
    } 
}
