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
using System.IO;
using System.Security.Cryptography;

namespace SystemPecAuto
{
    public partial class frmCadProduto : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        ConnectionString cs = new ConnectionString();
        public frmCadProduto()
        {
            InitializeComponent();
        }
        private void auto()
        {
            txtProdutoID.Text = "P-" + GetUniqueKey(6);
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

        private void frmCadProduto_Load(object sender, EventArgs e)
        {
            FillCombo();
            Autocomplete();
        }
        public void FillCombo()
        {
            try
            {

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select RTRIM(CategoryName) from Category order by CategoryName";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    comboBoxCategoria.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Resetar()
        {
            txtNomeProduto.Text = "";
            comboBoxSubCategoria.Text = "";
            comboBoxCategoria.Text = "";
            txtPreco.Text = "";
            txtDescricao.Text = "";
            pictureBox1.Image = Properties.Resources._12;
            comboBoxSubCategoria.Enabled = false;
            btnDeletar.Enabled = false;
            btnAtualizar.Enabled = false;
            btnSalvar.Enabled = true;
            txtNomeProduto.Focus();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Resetar();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtNomeProduto.Text == "")
            {
                MessageBox.Show("Informe o nome do produto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNomeProduto.Focus();
                return;
            }
            if (comboBoxCategoria.Text == "")
            {
                MessageBox.Show("Selecione a categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxCategoria.Focus();
                return;
            }
            if (comboBoxSubCategoria.Text == "")
            {
                MessageBox.Show("Selecione a sub categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxSubCategoria.Focus();
                return;
            }
            if (txtPreco.Text == "")
            {
                MessageBox.Show("Informe o preço", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPreco.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select ProductName from Product where ProductName='" + txtNomeProduto.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Nome do Produto já existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNomeProduto.Text = "";
                    txtNomeProduto.Focus();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }
                auto();
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "insert into Product(ProductID,ProductName,CategoryID,SubCategoryID,Features,Price,Image) VALUES ('" + txtProdutoID.Text + "','" + txtNomeProduto.Text + "'," + txtCategoriaID.Text + "," + txtSubCategoriaID.Text + ",@d1," + txtPreco.Text + ",@d2)";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtDescricao.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d2", SqlDbType.Image);
                p.Value = data;
                cmd.Parameters.Add(p);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Salvo com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnSalvar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string cq = "delete from product where productID='" + txtProdutoID.Text + "'";
                cmd = new SqlCommand(cq);
                cmd.Connection = con;
                registrosAfetados = cmd.ExecuteNonQuery();
                if (registrosAfetados > 0)
                {
                    MessageBox.Show("Deletado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Resetar();
                    Autocomplete();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Resetar();
                    Autocomplete();
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
        private void Autocomplete()
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT distinct ProductName FROM product", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Product");
                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["productname"].ToString());

                }
                txtNomeProduto.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtNomeProduto.AutoCompleteCustomSource = col;
                txtNomeProduto.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (txtNomeProduto.Text == "")
            {
                MessageBox.Show("Infome o nome do produto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNomeProduto.Focus();
                return;
            }
            if (comboBoxCategoria.Text == "")
            {
                MessageBox.Show("Selecione a categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxCategoria.Focus();
                return;
            }
            if (comboBoxSubCategoria.Text == "")
            {
                MessageBox.Show("Selecione a sub categoria", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxSubCategoria.Focus();
                return;
            }
            if (txtPreco.Text == "")
            {
                MessageBox.Show("Informe o preço", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPreco.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                string cb = "Update product set ProductName='" + txtNomeProduto.Text + "',CategoryID=" + txtCategoriaID.Text + ",SubCategoryID=" + txtSubCategoriaID.Text + ",Features=@d1,price=" + txtPreco.Text + ",Image=@d2 Where ProductID='" + txtProdutoID.Text + "'";
                cmd = new SqlCommand(cb);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@d1", txtDescricao.Text);
                MemoryStream ms = new MemoryStream();
                Bitmap bmpImage = new Bitmap(pictureBox1.Image);
                bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.GetBuffer();
                SqlParameter p = new SqlParameter("@d2", SqlDbType.Image);
                p.Value = data;
                cmd.Parameters.Add(p);
                cmd.ExecuteReader();
                con.Close();
                MessageBox.Show("Atualizado com sucesso", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Autocomplete();
                btnAtualizar.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnObterDados_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmProdutosRegistro2 frm = new frmProdutosRegistro2();
            frm.Show();
            frm.lblTipoUsuario.Text = lblTipoUsuario.Text;
            frm.ObterDados();
        }

        private void btnProcurar_Click(object sender, EventArgs e)
        {
            try
            {
                var _with1 = openFileDialog1;

                _with1.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                _with1.FilterIndex = 4;
                //Resetar o arquivo nome
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            // permite 0-9, 9-0 , e decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }
        
        }

        private void comboBoxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);
                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT ID from Category WHERE CategoryName = '" + comboBoxCategoria.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtCategoriaID.Text = rdr.GetInt32(0).ToString().Trim();
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                comboBoxCategoria.Text = comboBoxCategoria.Text.Trim();
                comboBoxSubCategoria.Items.Clear();
                comboBoxSubCategoria.Text = "";
                comboBoxSubCategoria.Enabled = true;
                comboBoxSubCategoria.Focus();

                con = new SqlConnection(cs.DBConn);
                con.Open();
                string ct = "select distinct RTRIM(SubCategoryName) from Category,SubCategory where Category.ID=SubCategory.CategoryID and CategoryName= '" + comboBoxCategoria.Text + "'";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    comboBoxSubCategoria.Items.Add(rdr[0]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxSubCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs.DBConn);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT ID from SubCategory WHERE SubCategoryName = '" + comboBoxSubCategoria.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtSubCategoriaID.Text = rdr.GetInt32(0).ToString().Trim();
                }
                if ((rdr != null))
                {
                    rdr.Close();
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

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
