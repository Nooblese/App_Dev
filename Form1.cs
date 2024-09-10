using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppDev
{
    public partial class Form1 : Form
    {
        double Iadv = 274840.00;
        double Ipcx = 250400.00;
        double Iclick = 153800.00;
        double adv = 168800.00;
        double pcx = 151800.00;
        double click = 82500.00;
        double ans;
        double ins = 4500.00;
        double reg = 3100.00;
        int months = 36;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbModel.Items.Add("Choose Model");
            cmbModel.Items.Add("Honda ADV");
            cmbModel.Items.Add("Honda PCX");
            cmbModel.Items.Add("Honda Click");
        }

        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            lblInsure.Text = ins.ToString();
            lblRegis.Text = reg.ToString();

            if (chkInstall.Checked)
            {
                if (string.IsNullOrEmpty(txtPay.Text))
                {
                    MessageBox.Show("Missing Input");
                }
                else 
                {
                
                    double tax = 0.11;
                    double mpay;
                    double pay = Convert.ToDouble(txtPay.Text);
                    lblStatus.Text = "Not Paid";

                    switch (cmbModel.SelectedIndex)
                    {
                        case 0:
                            ans = tax * Iadv;
                            mpay = (Iadv - pay) / months;
                            lblMPay.Text = mpay.ToString("0.##");
                            break;

                        case 1:
                            ans = tax * Ipcx;
                            mpay = (Ipcx - pay) / months;
                            lblMPay.Text = mpay.ToString("0.##");
                            break;
                        case 2:
                            ans = tax * Iclick;
                            mpay = (Iclick - pay) / months;
                            lblMPay.Text = mpay.ToString("0.##");
                            break;
                    }

                    lblTax.Text = ans.ToString();



                }

            }
            else 
            {
                lblStatus.Text = "Fully Paid";
                lblMPay.Text = "0.00";
                double tax = 0.03;

                switch (cmbModel.SelectedIndex)
                {
                    case 0:
                        ans = tax * adv;
                        lblTax.Text = ans.ToString();
                        break;

                    case 1:
                        ans = tax * pcx;
                        lblTax.Text = ans.ToString();
                        break;
                    case 2:
                        ans = tax * click;
                        lblTax.Text = ans.ToString();
                        break;

                }   

            }
                


            
            
                
            
        }
    }
}
