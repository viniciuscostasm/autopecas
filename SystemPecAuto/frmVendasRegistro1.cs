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
using Excel = Microsoft.Office.Interop.Excel;

namespace SystemPecAuto
{
    public partial class frmVendasRegistro1 : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public frmVendasRegistro1()
        {
            InitializeComponent();
        }

        private void frmVendasRegistro1_Load(object sender, EventArgs e)
        {

        }

        private void btnObterDados_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT RTRIM(invoiceNo) as [No_Fatura],CONVERT(DateTime,InvoiceDate,105) as [Data Fatura],RTRIM(SubTotal) as [SubTotal],RTRIM(VATPer) as [Impostos],RTRIM(VATAmount) as [Valor Impostos],RTRIM(DiscountPer) as [Desconto %],RTRIM(DiscountAmount) as [Valor Desconto],RTRIM(GrandTotal) as [Total],RTRIM(Cash) as [Dinheiro],RTRIM(Change) as [Troco] from Invoice_Info where InvoiceDate between @d1 and @d2 order by InvoiceDate", con);
                cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpDataFaturaDe.Value.Date;
                cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "InvoiceDate").Value = dtpDataFaturaAte.Value.Date;
                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                DataSet myDataSet = new DataSet();
                myDA.Fill(myDataSet, "Invoice_Info");
                dataGridView1.DataSource = myDataSet.Tables["Invoice_Info"].DefaultView;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dataGridView1.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
                dataGridView1.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                MessageBox.Show("Desculpe, não importado para oexcel..", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int rowsTotal = 0;
            int colsTotal = 0;
            int I = 0;
            int j = 0;
            int iC = 0;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            Excel.Application xlApp = new Excel.Application();

            try
            {
                Excel.Workbook excelBook = xlApp.Workbooks.Add();
                Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelBook.Worksheets[1];
                xlApp.Visible = true;

                rowsTotal = dataGridView1.RowCount - 1;
                colsTotal = dataGridView1.Columns.Count - 1;
                var _with1 = excelWorksheet;
                _with1.Cells.Select();
                _with1.Cells.Delete();
                for (iC = 0; iC <= colsTotal; iC++)
                {
                    _with1.Cells[1, iC + 1].Value = dataGridView1.Columns[iC].HeaderText;
                }
                for (I = 0; I <= rowsTotal - 1; I++)
                {
                    for (j = 0; j <= colsTotal; j++)
                    {
                        _with1.Cells[I + 2, j + 1].value = dataGridView1.Rows[I].Cells[j].Value;
                    }
                }
                _with1.Rows["1:1"].Font.FontStyle = "Bold";
                _with1.Rows["1:1"].Font.Size = 12;

                _with1.Cells.Columns.AutoFit();
                _with1.Cells.Select();
                _with1.Cells.EntireColumn.AutoFit();
                _with1.Cells[1, 1].Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                xlApp = null;
            }

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                this.Hide();
                frmVendas frmVendas = new frmVendas();
                // or simply use column name instead of index
                // dr.Cells["id"].Value.ToString();
                frmVendas.Show();
                frmVendas.txtContaFat.Text = dr.Cells[0].Value.ToString();
                frmVendas.dtpDataFatura.Text = dr.Cells[1].Value.ToString();
                frmVendas.txtSubTotal.Text = dr.Cells[2].Value.ToString();
                frmVendas.txtTaxPer.Text = dr.Cells[3].Value.ToString();
                frmVendas.txtTaxAmt.Text = dr.Cells[4].Value.ToString();
                frmVendas.txtDescontoPer.Text = dr.Cells[5].Value.ToString();
                frmVendas.txtDescontoAmt.Text = dr.Cells[6].Value.ToString();
                frmVendas.txtTotal.Text = dr.Cells[7].Value.ToString();
                frmVendas.txtDinheiro.Text = dr.Cells[8].Value.ToString();
                frmVendas.txtTroco.Text = dr.Cells[9].Value.ToString();
                frmVendas.btnAtualizar.Enabled = true;
                frmVendas.btnDelete.Enabled = true;
                frmVendas.btnImprimir.Enabled = true;
                frmVendas.btnSalvar.Enabled = false;

                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = new SqlCommand("SELECT Product.ProductID,ProductSold.Productname,ProductSold.Price,ProductSold.Quantity,ProductSold.TotalAmount from Invoice_Info,ProductSold,Product where Invoice_info.InvoiceNo=ProductSold.InvoiceNo and Product.ProductID=ProductSold.ProductID and invoice_Info.InvoiceNo='" + dr.Cells[0].Value.ToString() + "'", con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read() == true)
                {
                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(rdr[0].ToString().Trim());
                    lst.SubItems.Add(rdr[1].ToString().Trim());
                    lst.SubItems.Add(rdr[2].ToString().Trim());
                    lst.SubItems.Add(rdr[3].ToString().Trim());
                    lst.SubItems.Add(rdr[4].ToString().Trim());
                    frmVendas.listView1.Items.Add(lst);
                }
                frmVendas.listView1.Enabled = false;
                frmVendas.btnIncluirCarrinho.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmVendasRegistro1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmVendas frm = new frmVendas();
            frm.lblTipoUsuario.Text = this.lblTipoUsuario.Text;
            frm.Show();
        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            dtpDataFaturaDe.Text = System.DateTime.Today.ToString();
            dtpDataFaturaAte.Text = System.DateTime.Today.ToString();
            dataGridView1.DataSource = null;
        }
    }
}
