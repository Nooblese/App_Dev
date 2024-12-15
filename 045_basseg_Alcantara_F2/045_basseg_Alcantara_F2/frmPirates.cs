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
using System.Reflection;

namespace _045_basseg_Alcantara_F2
{
    public partial class frmPirates : Form
    {
        string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/basse/Downloads/dpPirates.accdb";
        OleDbConnection conn;
        public frmPirates()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select ID, piratename as ALIAS, givenname as Name, age as Age, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates";
            //string query = "select * from pirates";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            grdView.DataSource = dt;
            conn.Close();
            grdView.Columns["Age"].Visible = false;
            grdView.Columns["ID"].Visible = false;

            txtAlias.Enabled = false;
            txtName.Enabled = false;
            txtAge.Enabled = false;
            cmbPirateGroup.Enabled = false;
            txtBounty.Enabled = false;

            btnSave.Enabled = false;


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //string query = "select piratename as ALIAS, givenname as Name, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates where piratename like '%" + txtSearch.Text + "%' or givenname like '%" + txtSearch.Text + "%' and pirategroupt like '%" + cmbSearch.Text + "%' ";
            ////string query = "select * from pirates";
            //conn = new OleDbConnection(connStr);
            //conn.Open();
            //OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            //adapter.Fill(dt);
            //grdView.DataSource = dt;
            //conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || string.IsNullOrWhiteSpace(cmbSearch.Text))
            {
                MessageBox.Show("Missing search entry", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else 
            {
                DataTable dt = new DataTable();
                string query = "select piratename as ALIAS, givenname as Name, age as Age, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates where (piratename like '%" + txtSearch.Text + "%' or givenname like '%" + txtSearch.Text + "%') and pirategroup like '" + cmbSearch.Text + "' ";
                conn = new OleDbConnection(connStr);
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                adapter.Fill(dt);
                grdView.DataSource = dt;
                conn.Close();
                grdView.Columns["Age"].Visible = false;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (grdView.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = grdView.SelectedRows[0];

                txtAlias.Text = selectedRow.Cells["ALIAS"].Value.ToString();
                txtName.Text = selectedRow.Cells["Name"].Value.ToString();
                txtAge.Text = selectedRow.Cells["Age"].Value.ToString();
                cmbPirateGroup.SelectedItem = selectedRow.Cells["PIRATEGROUP"].Value.ToString();
                txtBounty.Text = selectedRow.Cells["BOUNTY"].Value.ToString();

                txtAlias.Enabled = true;
                txtName.Enabled = true;
                txtAge.Enabled = true;
                cmbPirateGroup.Enabled = true;
                txtBounty.Enabled = true;

                btnRecord.Enabled = false;
                btnSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please select a row to view details.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (int.Parse(txtBounty.Text) <= 0)
            {
                MessageBox.Show("Bounty cannot be Zero");
                return;
            }
            else 
            {
                string query = "UPDATE pirates set piratename =@alias, givenname =@name, age =@age, pirategroup = @group, bounty =@bounty " +
                   "where ID =" + grdView.SelectedCells[0].Value.ToString() + "";
                //DataTable dt = new DataTable();
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

                txtAlias.Clear();
                txtName.Clear();
                txtAge.Clear();
                cmbPirateGroup.SelectedIndex = -1;
                txtBounty.Clear();

                grdView.Refresh();
            }

            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtAlias.Enabled = false;
            txtName.Enabled = false;
            txtAge.Enabled = false;
            cmbPirateGroup.Enabled = false;
            txtBounty.Enabled = false;

            btnSave.Enabled = false;
            btnRecord.Enabled = true;

            txtAlias.Clear();
            txtName.Clear();
            txtAge.Clear();
            cmbPirateGroup.SelectedIndex = -1;
            txtBounty.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (grdView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = grdView.SelectedRows[0];

                if (MessageBox.Show("Are you sure you want to delete this row?", "Confirm Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    string query = "delete from pirates where ID = "+grdView.SelectedCells[0].Value.ToString()+" ";
                    conn = new OleDbConnection(connStr);
                    conn.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
                    adapter.Fill(dt);
                    grdView.DataSource = dt;
                    conn.Close();

                }

                
            }
            else
            {
                MessageBox.Show("Please select a row to delete.");
            }

            
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtAlias.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || cmbPirateGroup.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtBounty.Text))
            {
                txtAlias.Enabled = true;
                txtName.Enabled = true;
                txtAge.Enabled = true;
                txtBounty.Enabled = true;
                cmbPirateGroup.Enabled = true;
            }
            else 
            {
                DataTable dt = new DataTable();
                string query = "insert into pirates(piratename,givenname,age,bounty,pirategroup) values(@alias,@name,@age,@bounty,@pirategroup)";
                conn = new OleDbConnection(connStr);
                OleDbCommand cmd = new OleDbCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@alias", txtAlias.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@age", txtAge.Text);
                cmd.Parameters.AddWithValue("@bounty", txtBounty.Text);
                cmd.Parameters.AddWithValue("@pirategroup", cmbPirateGroup.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                txtAlias.Clear();
                txtName.Clear();
                txtAge.Clear();
                cmbPirateGroup.SelectedIndex = -1;
                txtBounty.Clear();


                btnRecord.Enabled = false;
            }




        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string query = "select ID, piratename as ALIAS, givenname as Name, age as Age, pirategroup as PIRATEGROUP, bounty as BOUNTY from pirates";
            //string query = "select * from pirates";
            conn = new OleDbConnection(connStr);
            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn);
            adapter.Fill(dt);
            grdView.DataSource = dt;
            conn.Close();
            grdView.Columns["Age"].Visible = false;
            grdView.Columns["ID"].Visible = false;

            txtAlias.Enabled = false;
            txtName.Enabled = false;
            txtAge.Enabled = false;
            cmbPirateGroup.Enabled = false;
            txtBounty.Enabled = false;

            btnSave.Enabled = false;

            txtAlias.Clear();
            txtName.Clear();
            txtAge.Clear();
            cmbPirateGroup.SelectedIndex = -1;
            txtBounty.Clear() ;
            btnRecord.Enabled = true;

            txtSearch.Clear();
            cmbSearch.SelectedIndex = -1;
        }
    }
}
