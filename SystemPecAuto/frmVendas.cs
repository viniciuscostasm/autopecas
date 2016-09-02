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
using System.Security.Cryptography;


namespace SystemPecAuto
{
    public partial class frmVendas : Form
    {
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public frmVendas()
        {
            InitializeComponent();
        }

        private void auto()
        {
            txtContaFat.Text = "INV-" + GetUniqueKey(8);

        }
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }




        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaxPer.Text == "")
                {
                    MessageBox.Show("Informe o percentual do Imposto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTaxPer.Focus();
                    return;
                }
                if (txtDescontoPer.Text == "")
                {
                    MessageBox.Show("Informe o percentual do Desconto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescontoPer.Focus();
                    return;
                }
                if (txtDinheiro.Text == "")
                {
                    MessageBox.Show("Informe o valor Total", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDinheiro.Focus();
                    return;
                }

                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("Nenhum produto foi adicionado", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                auto();

                con = new SqlConnection(cs.DBConn);
                con.Open();

                string cb = "insert Into Invoice_Info(InvoiceNo,InvoiceDate,SubTotal,VATPer,VATAmount,DiscountPer,DiscountAmount,GrandTotal,Cash,Change) VALUES ('" + txtContaFat.Text + "','" + dtpDataFatura.Text + "'," + txtSubTotal.Text + "," + txtTaxPer.Text + "," + txtTaxAmt.Text + "," + txtDescontoPer.Text + "," + txtDescontoAmt.Text + "," + txtTotal.Text + "," + txtDinheiro.Text + "," + txtTroco.Text + ")";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Close();


                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);

                    string cd = "insert Into ProductSold(InvoiceNo,ProductID,ProductName,Quantity,Price,TotalAmount) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(cd);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("d1", txtContaFat.Text);
                    cmd.Parameters.AddWithValue("d2", listView1.Items[i].SubItems[1].Text);
                    cmd.Parameters.AddWithValue("d3", listView1.Items[i].SubItems[2].Text);
                    cmd.Parameters.AddWithValue("d4", listView1.Items[i].SubItems[4].Text);
                    cmd.Parameters.AddWithValue("d5", listView1.Items[i].SubItems[3].Text);
                    cmd.Parameters.AddWithValue("d6", listView1.Items[i].SubItems[5].Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "update temp_stock set Quantity = Quantity - " + listView1.Items[i].SubItems[4].Text + " where ProductID= '" + listView1.Items[i].SubItems[1].Text + "'";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

                btnSalvar.Enabled = false;
                btnImprimir.Enabled = true;
                ObterDados();
                MessageBox.Show("Operação relizada com sucesso", "Faturamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void frmVendas_Load(object sender, EventArgs e)
        {
            ObterDados();
        }
        public void Calculo()
        {
            try
            {
                double val1 = 0;
                double val2 = 0;
                double val3 = 0;
                double val4 = 0;
                double val5 = 0;
                if (txtTaxPer.Text != "")
                {
                    val1 = Convert.ToDouble((Convert.ToDouble(txtSubTotal.Text) * Convert.ToDouble(txtTaxPer.Text) / 100));
                    val1 = Math.Round(val1, 2);
                    txtTaxAmt.Text = val1.ToString();

                }
                if (txtDescontoPer.Text != "")
                {
                    val3 = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtTaxAmt.Text)) * Convert.ToDouble(txtDescontoPer.Text) / 100));
                    val3 = Math.Round(val3, 2);
                    txtDescontoAmt.Text = val3.ToString();
                }

                double.TryParse(txtTaxAmt.Text, out val1);
                double.TryParse(txtSubTotal.Text, out val2);
                double.TryParse(txtDescontoAmt.Text, out val3);
                double.TryParse(txtTotal.Text, out val4);
                double.TryParse(txtDinheiro.Text, out val5);
                val1 = Math.Round(val1, 2);
                val2 = Math.Round(val2, 2);
                val3 = Math.Round(val3, 2);
                val4 = val1 + val2 - val3;
                val4 = Math.Round(val4, 2);
                txtTotal.Text = val4.ToString();
                double I = (val5 - val4);
                I = Math.Round(I, 2);
                txtTroco.Text = I.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQtdVendida_TextChanged(object sender, EventArgs e)
        {
            double val1 = 0;
            int val2 = 0;
            double.TryParse(txtPreco.Text, out val1);
            int.TryParse(txtQtdVendida.Text, out val2);
            double I = (val1 * val2);
            txtValorTotal.Text = I.ToString();
        }

        public double subtotal()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            i = 0;
            j = 0;
            k = 0;
            try
            {
                j = listView1.Items.Count;
                for (i = 0; i <= j - 1; i++)
                {
                    k = k + Convert.ToInt32(listView1.Items[i].SubItems[5].Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return k;

        }

        private void btnIncluirCarrinho_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNomeProduto.Text == "")
                {
                    MessageBox.Show("Informe o nome do produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (txtQtdVendida.Text == "")
                {
                    MessageBox.Show("Informe a quantidade vendida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQtdVendida.Focus();
                    return;
                }
                int SaleQty = Convert.ToInt32(txtQtdVendida.Text);
                if (SaleQty == 0)
                {
                    MessageBox.Show("O valor não pode ser igual a zero", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQtdVendida.Focus();
                    return;
                }

                if (listView1.Items.Count == 0)
                {

                    ListViewItem lst = new ListViewItem();
                    lst.SubItems.Add(txtProdutoID.Text);
                    lst.SubItems.Add(txtNomeProduto.Text);
                    lst.SubItems.Add(txtPreco.Text);
                    lst.SubItems.Add(txtQtdVendida.Text);
                    lst.SubItems.Add(txtValorTotal.Text);
                    listView1.Items.Add(lst);
                    txtSubTotal.Text = subtotal().ToString();

                    Calculo();
                    txtNomeProduto.Text = "";
                    txtProdutoID.Text = "";
                    txtPreco.Text = "";
                    txtQtdDisponivel.Text = "";
                    txtQtdVendida.Text = "";
                    txtValorTotal.Text = "";
                    txtProduto.Text = "";
                    return;
                }

                for (int j = 0; j <= listView1.Items.Count - 1; j++)
                {
                    if (listView1.Items[j].SubItems[1].Text == txtProdutoID.Text)
                    {
                        listView1.Items[j].SubItems[1].Text = txtProdutoID.Text;
                        listView1.Items[j].SubItems[2].Text = txtNomeProduto.Text;
                        listView1.Items[j].SubItems[3].Text = txtPreco.Text;
                        listView1.Items[j].SubItems[4].Text = (Convert.ToInt32(listView1.Items[j].SubItems[4].Text) + Convert.ToInt32(txtQtdVendida.Text)).ToString();
                        listView1.Items[j].SubItems[5].Text = (Convert.ToInt32(listView1.Items[j].SubItems[5].Text) + Convert.ToInt32(txtValorTotal.Text)).ToString();
                        txtSubTotal.Text = subtotal().ToString();
                        Calculo();
                        txtNomeProduto.Text = "";
                        txtProdutoID.Text = "";
                        txtPreco.Text = "";
                        txtQtdDisponivel.Text = "";
                        txtQtdVendida.Text = "";
                        txtValorTotal.Text = "";
                        return;
                    }
                }

                ListViewItem lst1 = new ListViewItem();

                lst1.SubItems.Add(txtProdutoID.Text);
                lst1.SubItems.Add(txtNomeProduto.Text);
                lst1.SubItems.Add(txtPreco.Text);
                lst1.SubItems.Add(txtQtdVendida.Text);
                lst1.SubItems.Add(txtValorTotal.Text);
                listView1.Items.Add(lst1);
                txtSubTotal.Text = subtotal().ToString();
                Calculo();
                txtNomeProduto.Text = "";
                txtProdutoID.Text = "";
                txtPreco.Text = "";
                txtQtdDisponivel.Text = "";
                txtQtdVendida.Text = "";
                txtValorTotal.Text = "";
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("Nenhum item para remover", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    int itmCnt = 0;
                    int i = 0;
                    int t = 0;

                    listView1.FocusedItem.Remove();
                    itmCnt = listView1.Items.Count;
                    t = 1;

                    for (i = 1; i <= itmCnt + 1; i++)
                    {
                        //Dim lst1 As New ListViewItem(i)
                        //ListView1.Items(i).SubItems(0).Text = t
                        t = t + 1;

                    }
                    txtSubTotal.Text = subtotal().ToString();
                    Calculo();
                }

                btnRemover.Enabled = false;
                if (listView1.Items.Count == 0)
                {
                    txtSubTotal.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtTaxPer_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTaxPer.Text))
                {
                    txtTaxAmt.Text = "";
                    txtTotal.Text = "";
                    return;
                }
                Calculo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemover.Enabled = true;
        }

        private void txtProduto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT Product.ProductID,ProductName,Features,Price,sum(Quantity) from Temp_Stock,Product where Temp_Stock.ProductID=Product.ProductID and ProductName like '" + txtProduto.Text + "%' group by product.ProductID,productname,Price,Features,Quantity having(quantity>0) order by ProductName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
                }
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

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                DataGridViewRow dr = dataGridView1.SelectedRows[0];
                txtProdutoID.Text = dr.Cells[0].Value.ToString();
                txtNomeProduto.Text = dr.Cells[1].Value.ToString();
                txtPreco.Text = dr.Cells[3].Value.ToString();
                txtQtdDisponivel.Text = dr.Cells[4].Value.ToString();
                txtQtdVendida.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ObterDados()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String sql = "SELECT Product.ProductID,ProductName,Features,Price,sum(Quantity) from Temp_Stock,Product where Temp_Stock.ProductID=Product.ProductID group by Product.productID,productname,Price,Features,Quantity having(Quantity>0)  order by ProductName";
                cmd = new SqlCommand(sql, con);
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dataGridView1.Rows.Clear();
                while (rdr.Read() == true)
                {
                    dataGridView1.Rows.Add(rdr[0], rdr[1], rdr[2], rdr[3], rdr[4]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Resetar()
        {
            txtContaFat.Text = "";
            dtpDataFatura.Text = DateTime.Today.ToString();
            txtNomeProduto.Text = "";
            txtProdutoID.Text = "";
            txtPreco.Text = "";
            txtQtdDisponivel.Text = "";
            txtQtdVendida.Text = "";
            txtValorTotal.Text = "";
            listView1.Items.Clear();
            txtDescontoAmt.Text = "";
            txtDescontoPer.Text = "";

            txtSubTotal.Text = "";
            txtTaxPer.Text = "";
            txtTaxAmt.Text = "";
            txtTotal.Text = "";
            txtDinheiro.Text = "";
            txtTroco.Text = "";
            txtProduto.Text = "";
            btnSalvar.Enabled = true;
            btnDelete.Enabled = false;
            btnAtualizar.Enabled = false;
            btnRemover.Enabled = false;
            btnImprimir.Enabled = false;
            listView1.Enabled = true;
            btnIncluirCarrinho.Enabled = true;

        }

        private void btnNovoRegistro_Click(object sender, EventArgs e)
        {
            Resetar();
            Resetar();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja deletar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                deletar_registros();
            }
        }
        private void deletar_registros()
        {

            try
            {

                int registrosAfetados = 0;
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq1 = "delete from productSold where InvoiceNo='" + txtContaFat.Text + "'";
                cmd = new SqlCommand(cq1);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                con.Close();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Invoice_Info where InvoiceNo='" + txtContaFat.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                if (registrosAfetados > 0)
                {
                    MessageBox.Show("Deletado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Resetar();
                    Resetar();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Resetar();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtQtdVendida_Validating(object sender, CancelEventArgs e)
        {
            int val1 = 0;
            int val2 = 0;
            int.TryParse(txtQtdDisponivel.Text, out val1);
            int.TryParse(txtQtdVendida.Text, out val2);
            if (val2 > val1)
            {
                MessageBox.Show("A quantidade vendida é maior que a quantidade disponível em estoque", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQtdVendida.Text = "";
                txtValorTotal.Text = "";
                txtQtdVendida.Focus();
                return;
            }

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptFatura rpt = new rptFatura();
                //The report you created.
                cmd = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                POS_DBDataSet myDS = new POS_DBDataSet();
                //The DataSet you created.
                con = new SqlConnection(cs.DBConn);
                cmd.Connection = con;
                cmd.CommandText = "SELECT * from invoice_info,productsold where invoice_info.invoiceno=productsold.invoiceno  and Invoice_info.invoiceNo='" + txtContaFat.Text + "'";
                cmd.CommandType = CommandType.Text;
                myDA.SelectCommand = cmd;
                myDA.Fill(myDS, "Invoice_Info");
                myDA.Fill(myDS, "ProductSold");
                rpt.SetDataSource(myDS);
                frmFaturaRelatorio frm = new frmFaturaRelatorio();
                frm.crystalReportViewer1.ReportSource = rpt;
                frm.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                String cb = "update Invoice_info set  VATPer=" + txtTaxPer.Text + ",VATAmount=" + txtTaxAmt.Text + ",DiscountPer=" + txtDescontoPer.Text + ",DiscountAmount=" + txtDescontoAmt.Text + ",GrandTotal= " + txtTotal.Text + ",TotalPayment= " + txtDinheiro.Text + ",PaymentDue= " + txtTroco.Text + " where Invoiceno= '" + txtContaFat.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();

                ObterDados();
                btnAtualizar.Enabled = false;
                MessageBox.Show("Atualizado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtQtdVendida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
     
        }

        private void txtTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allows 0-9, backspace, and decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
   
        }

        private void btnObterDados_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmVendasRegistro1 frm = new frmVendasRegistro1();
            frm.lblTipoUsuario.Text = this.lblTipoUsuario.Text;
            frm.Show();
        }

        private void txtDescontoPer_TextChanged(object sender, EventArgs e)
        {
            Calculo();
        }

        private void txtDinheiro_TextChanged(object sender, EventArgs e)
        {
            Calculo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("OSK.exe");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {

        }

    }

}







