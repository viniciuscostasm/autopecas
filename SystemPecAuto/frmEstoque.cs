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
    public partial class frmEstoque : Form
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr;
        ConnectionString cs = new ConnectionString();
        public frmEstoque()
        {
            InitializeComponent();
        }


        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "123456789".ToCharArray();
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
        private void auto()
        {
            txtEstoqueID.Text = "ST-" + GetUniqueKey(6);
        }

        private void frmEstoque_Load(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProdutosRegistro1 frm = new frmProdutosRegistro1();
            frm.lblTipoUsuario.Text = lblTipoUsuario.Text;
            frm.Show();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNomeProduto.Text == "")
            {
                MessageBox.Show("Informe o nome do produto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNomeProduto.Focus();
                return;
            }
            if (txtQtd.Text == "")
            {
                MessageBox.Show("Informe a quantidade", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQtd.Focus();
                return;
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string ct = "select ProductID from temp_Stock where ProductID='" + txtProdutoID.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb2 = "Update Temp_Stock set Quantity=Quantity + " + txtQtd.Text + " where ProductID='" + txtProdutoID.Text + "'";
                    cmd = new SqlCommand(cb2);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();

                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "insert into Temp_Stock(ProductID,Quantity) VALUES ('" + txtProdutoID.Text + "'," + txtQtd.Text + ")";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;

                    cmd.ExecuteReader();
                    con.Close();
                }
                auto();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into Stock(StockID,ProductID,Quantity,StockDate) VALUES ('" + txtEstoqueID.Text + "','" + txtProdutoID.Text + "'," + txtQtd.Text + ",@d1)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", dtpDataEstoque.Text);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Salvo com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSalvar.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Resetar()
        {
            txtCaracteristicas.Text = "";
            txtNomeProduto.Text = "";
            txtQtd.Text = "";
            txtEstoqueID.Text = "";
            dtpDataEstoque.Text = DateTime.Today.ToString();

            btnDeletar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnSalvar.Enabled = true;

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Resetar();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
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
                string cb2 = "Update Temp_Stock set Quantity=Quantity - " + txtQtd1.Text + " where ProductID='" + txtProdutoID.Text + "'";
                cmd = new SqlCommand(cb2);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cq = "delete from Stock where StockID='" + txtEstoqueID.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                if (registrosAfetados > 0)
                {
                    MessageBox.Show("Deletado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnObterDados_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmEstoqueRegistro frm = new frmEstoqueRegistro();
            frm.Show();
            frm.lblTipoUsuario.Text = lblTipoUsuario.Text;
            frm.ObterDados();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {

            if (txtNomeProduto.Text == "")
            {
                MessageBox.Show("Informe o nome do produto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNomeProduto.Focus();
                return;
            }
            if (txtQtd.Text == "")
            {
                MessageBox.Show("Informe a quantidade", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtQtd.Focus();
                return;
            }

            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();

                string ct = "select ProductID from temp_Stock where ProductID='" + txtProdutoID.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb2 = "Update Temp_Stock set Quantity=Quantity + " + txtQtd.Text + " - " + txtQtd1.Text + " where ProductID='" + txtProdutoID.Text + "'";
                    cmd = new SqlCommand(cb2);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();

                }
                else
                {
                    con = new SqlConnection(cs.DBConn);
                    con.Open();
                    string cb1 = "insert into Temp_Stock(ProductID,Quantity) VALUES ('" + txtProdutoID.Text + "'," + txtQtd.Text + ")";
                    cmd = new SqlCommand(cb1);
                    cmd.Connection = con;

                    cmd.ExecuteReader();
                    con.Close();
                }
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "Update Stock set ProductID='" + txtProdutoID.Text + "',Quantity=" + txtQtd.Text + ",StockDate= '" + dtpDataEstoque.Text + "' where StockID='" + txtEstoqueID.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Atualizado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAtualizar.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void txtQtd_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

