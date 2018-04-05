using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagentoConnector
{
    public partial class Form1 : Form
    {
        
        private Magento m2 { get; set; }
        public Form1()
        {
            InitializeComponent();
        }


        private void btnGO_Click(object sender, EventArgs e)
        {
            string token = this.GetToken(txtUserName.Text, txtPassword.Text);
            MessageBox.Show(token);   
        }


       private string GetToken(string username, string password)
        {
            m2 = new Magento("http://focus.roadmaster.com.co/");
            string token = m2.getAdminToken(username, password);
            return token;
        }

        private void btnGetProduct_Click(object sender, EventArgs e)
        {
            string token = GetToken(txtUserName.Text, txtPassword.Text);

            if (token != string.Empty || token != null)
            {
                //the returned token will be enclosed in " (quotes) -- you need to remove the quotes
                token = token.Replace("\"", string.Empty); 

                string productData = m2.GetProduct(txtSku.Text, token);
                MessageBox.Show(productData);
            }
            else
            {
                MessageBox.Show("You don't have proper credentials to access the resources!");
            }
        }

        
        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
           
            double price_in_dollars = Convert.ToDouble(txtPriceValue.Text.ToString()) / Convert.ToDouble(txtConversionRate.Text.ToString());

            string token = GetToken(txtUserName.Text, txtPassword.Text);

            if (token != string.Empty || token != null)
            {
                //the returned token will be enclosed in " (quotes) -- you need to remove the quotes
                token = token.Replace("\"", string.Empty);

                string productData = m2.UpdatePrice(txtSku2.Text, Convert.ToString(price_in_dollars), token);
                MessageBox.Show(productData);
            }
            else
            {
                MessageBox.Show("You don't have proper credentials to access the resources!");
            }
        }

        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            string token = GetToken(txtUserName.Text, txtPassword.Text);

            if (token != string.Empty || token != null)
            {
                //the returned token will be enclosed in " (quotes) -- you need to remove the quotes
                token = token.Replace("\"", string.Empty);

                string productData = m2.UpdateStock(txtSku3.Text, txtQty.Text, token);
                MessageBox.Show(productData);
            }
            else
            {
                MessageBox.Show("You don't have proper credentials to access the resources!");
            }
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            double price_in_dollars = Convert.ToDouble(txtPrice5.Text.ToString()) / Convert.ToDouble(txtConvRate5.Text.ToString());

            string token = GetToken(txtUserName.Text, txtPassword.Text);

            if (token != string.Empty || token != null)
            {
                //the returned token will be enclosed in " (quotes) -- you need to remove the quotes
                token = token.Replace("\"", string.Empty);

                string productData = m2.UpdateStockAndPrice(txtSku5.Text,txtQty5.Text, Convert.ToString(price_in_dollars), token);
                MessageBox.Show(productData);
            }
            else
            {
                MessageBox.Show("You don't have proper credentials to access the resources!");
            }
        }

        private void btnCreateProduct_Click(object sender, EventArgs e)
        {
            double price_in_dollars = Convert.ToDouble(txtPrice6.Text.ToString()) / 3.75;


            string token = GetToken(txtUserName.Text, txtPassword.Text);

            if (token != string.Empty || token != null)
            {
                //the returned token will be enclosed in " (quotes) -- you need to remove the quotes
                token = token.Replace("\"", string.Empty);

                string productData = m2.CreateProduct(txtSku6.Text, txtName6.Text, txtWeight6.Text, Convert.ToString(price_in_dollars), txtQty6.Text, txtSuitableCar6.Text, txtSuitableYear6.Text, token);
                MessageBox.Show(productData);
            }
            else
            {
                MessageBox.Show("You don't have proper credentials to access the resources!");
            }

        }
    }
}
