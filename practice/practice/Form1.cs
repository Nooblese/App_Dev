using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace practice
{
    public partial class Form1 : Form
    {
        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\basse\\Downloads\\dpPirates.accdb";
        OleDbConnection conn;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select ID as ID, piratename as ALIAS, givenname as NAME, age as AGE, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates ";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            grdView.DataSource = dt;
            conn.Close();

            grdView.Columns["ID"].Visible = false;
            grdView.Columns["age"].Visible = false;

            txtAlias.Enabled = false;
            txtName.Enabled = false;
            txtAge.Enabled = false;
            cmbPirateGroup.Enabled = false;
            txtBounty.Enabled = false;

            btnSave.Enabled = false;
            btnUpdate.Enabled = false;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || string.IsNullOrWhiteSpace(cmbSearch.Text))
            {
                MessageBox.Show("Missing Search Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                DataTable dt = new DataTable();
                string query = "select piratename as ALIAS, givenname as NAME, age as AGE, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates where (piratename like '%"+txtSearch.Text+"%' or givenname like '%"+txtSearch.Text+"%') and pirategroup like '"+cmbSearch.Text+"' ";
                conn = new OleDbConnection(connStr);
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                adapter.Fill(dt);
                grdView.DataSource= dt;
                conn.Close();
                grdView.Columns["age"].Visible = false;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedrow = grdView.SelectedRows[0];

            txtAlias.Text = selectedrow.Cells["ALIAS"].Value.ToString();
            txtName.Text = selectedrow.Cells["NAME"].Value.ToString();
            txtAge.Text = selectedrow.Cells["AGE"].Value.ToString();
            cmbPirateGroup.Text = selectedrow.Cells["PIRATEGROUP"].Value.ToString();
            txtBounty.Text = selectedrow.Cells["BOUNTY"].Value.ToString();

            txtAlias.Enabled = true;
            txtName.Enabled = true;
            txtAge.Enabled = true;
            cmbPirateGroup.Enabled = true;
            txtBounty.Enabled = true;

            btnRecord.Enabled = false;
            btnUpdate.Enabled = true;
        }

        private void load() 
        {
            DataTable dt = new DataTable();
            string query = "select ID as ID, piratename as ALIAS, givenname as NAME, age as AGE, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates ";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            grdView.DataSource = dt;
            conn.Close();

            grdView.Columns["ID"].Visible = false;
            grdView.Columns["age"].Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtBounty.Text) <= 0)
            {
                MessageBox.Show("Bounty Cannot Be ZERO!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataTable dt = new DataTable();
                string query = "UPDATE pirates set piratename = @alias, givenname = @name, age =@age, pirategroup =@group, bounty = @bounty where ID = " + grdView.SelectedCells[0].Value.ToString() +"";
                conn = new OleDbConnection(connStr);
                OleDbCommand cmd = new OleDbCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@alias", txtAlias.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@group", cmbPirateGroup.Text);
                cmd.Parameters.AddWithValue("@bounty", txtBounty.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Successfully Updated!","Updated",MessageBoxButtons.OK,MessageBoxIcon.Information);


                load();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            string query = "delete from pirates where ID = " + grdView.SelectedCells[0].Value.ToString() +"";
            conn = new OleDbConnection( connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dataTable);
            conn.Close();

            MessageBox.Show("Successfully Deleted!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);

            load();
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            txtAlias.Enabled = true;
            txtName.Enabled = true;
            txtAge.Enabled = true;
            cmbPirateGroup.Enabled = true;
            txtBounty.Enabled = true;

            txtAlias.Clear();
            txtName.Clear();
            txtAge.Clear();
            cmbPirateGroup.SelectedIndex = -1;
            txtBounty.Clear();

            btnSave.Enabled = true;
            btnRecord.Enabled = false;
            btnUpdate.Enabled = false;




        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtAlias.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || string.IsNullOrWhiteSpace(cmbPirateGroup.Text) || string.IsNullOrWhiteSpace(txtBounty.Text))
            {
                MessageBox.Show("Mising input record!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                DataTable dataTable = new DataTable();
                string query = "insert into pirates(piratename,givenname,age,pirategroup,bounty) values(@alias,@name,@age,@group,@bounty) ";
                conn = new OleDbConnection(connStr);
                OleDbCommand cmd = new OleDbCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@alias", txtAlias.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@group", cmbPirateGroup.Text);
                cmd.Parameters.AddWithValue("@bounty", txtBounty.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                load();

                MessageBox.Show("Record Saved!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtAlias.Clear();
                txtName.Clear();
                txtAge.Clear();
                cmbPirateGroup.SelectedIndex = -1;
                txtBounty.Clear();
            }




        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            

            txtAlias.Clear();
            txtName.Clear();
            txtAge.Clear();
            cmbPirateGroup.SelectedIndex = -1;
            txtBounty.Clear();

         
        }
    }

}
