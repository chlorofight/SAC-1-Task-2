using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unit_3_sac_1_Task_2
{

    //Unit 3 sac 1 Task 2, Leonardo Bini, 23/03/2022
    public partial class Form1 : Form
    {
        public Form1()
        {
            //Add colomuns to the data grid view (OLD CODE)
            InitializeComponent();
            /*
            dataGridView1.ColumnCount = 7;
            dataGridView1.Columns[0].Name = "Textbook";
            dataGridView1.Columns[1].Name = "Subject";
            dataGridView1.Columns[2].Name = "Seller";
            dataGridView1.Columns[3].Name = "Purchaser";
            dataGridView1.Columns[4].Name = "Purchased Price";
            dataGridView1.Columns[5].Name = "Sale Price";
            dataGridView1.Columns[6].Name = "Profit";
            */
        }
        float fTotalProfit = 0f;

        private void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    main(ofd.FileName);
                }
                else if (ofd.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBox.Show("No file selected");
                }
            }
            catch
            {
                MessageBox.Show("Not valid File");
            }
        }

        private void main(string filePath)
        {   
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(filePath).ToList();
            //float totalProfit = 0;
            foreach (string line in lines)
            {
                List<string> items = line.Split(',').ToList();
                items.Add(Calc(items[4], items[5]).ToString());
                dataGridView1.Rows.Add(items.ToArray());
            }
        }

        //Function : Calc the profit
        //Input: String, purchedPriceIn , string, salePriceIn
        //Output: float, salePriceIn - PurchedpriceIn , float, 0 , float Purchedprice * -1

        private float Calc(string purchedPriceIn, string salePriceIn)
        {
            float purchedPrice;
            float salePrice;
            if (float.TryParse(purchedPriceIn, out purchedPrice)
                & float.TryParse(salePriceIn, out salePrice))
            {
                fTotalProfit = fTotalProfit + (salePrice - purchedPrice);
                lblTotalProfit.Text = $"The total profit is {fTotalProfit}";
                return salePrice - purchedPrice;
            }
            else
            {
                if(float.TryParse(purchedPriceIn, out purchedPrice))
                {
                    fTotalProfit = fTotalProfit - purchedPrice;
                    lblTotalProfit.Text = $"The total profit is {fTotalProfit}";
                    return purchedPrice * -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            openFile();
        }
    }
}
